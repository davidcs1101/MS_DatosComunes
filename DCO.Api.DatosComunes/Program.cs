using Microsoft.EntityFrameworkCore;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using DCO.Api.DatosComunes.Middlewares;
using DCO.Infraestructura.Dominio.Repositorio;
using DCO.Infraestructura.Aplicacion.ServiciosExternos;
using DCO.Infraestructura.Servicios.Interfaces;
using DCO.Infraestructura.Servicios.Implementaciones;
using DCO.Aplicacion.CasosUso.Implementaciones;
using DCO.Aplicacion.CasosUso.Interfaces;
using DCO.Aplicacion.ServiciosExternos;
using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Aplicacion.Servicios.Implementaciones;
using DCO.DataAccess;
using DCO.Dominio.Repositorio;
using DCO.Dominio.Servicios.Interfaces;
using DCO.Dominio.Servicios.Implementaciones;
using DCO.Dominio.Repositorio.UnidadTrabajo;
using DCO.Intraestructura.Dominio.Repositorio.UnidadTrabajo;
using DCO.Intraestructura.Dominio.Repositorio;
using DCO.Aplicacion.ServiciosExternos.config;
using DCO.Dtos.AppSettings;
using DCO.Infraestructura.Aplicacion.ServiciosExternos.Config;
using SEG.Infraestructura.Aplicacion.ServiciosExternos.Config;
using Hangfire;
using Hangfire.MySql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Configuramos Swagger para que permita envío de Bearer Token
// Agregar esto después de 'builder.Services.AddSwaggerGen();'
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DCO.Api.DatosComunes", Version = "1.0" });

    // Configuración de Bearer Token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor ingrese el token Bearer en el siguiente formato: Bearer su_token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


//Configuramos AutoMapper para el mapeo de DTOS a las entidades y le decimos que se hará a nivel de Ensamblado
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configuración de log4net
var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
builder.Services.AddLogging(loggingBuilder => { loggingBuilder.AddLog4Net(); });

// Configuracion de JWT
var configuracionJWT = builder.Configuration.GetSection("JWT");
var issuer = configuracionJWT["Emisor"];
var audience = configuracionJWT["Audiencia"];
var key = configuracionJWT["Llave"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer
    (opcion =>
    {
        opcion.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ClockSkew = TimeSpan.Zero //No se permite tolerancia de tiempo una vez el token caduca (por defecto es 5 minutos si no se establece)
        };
    });
builder.Services.AddAuthorization(options => options.AddPolicy("ListasPermiso",
permiso => permiso.RequireClaim("Programa", "LISTAS")));
builder.Services.AddAuthorization(options => options.AddPolicy("DatosConstantesPermiso",
permiso => permiso.RequireClaim("Programa", "DATOSCONSTANTES")));

builder.Services.AddScoped<IListaRepositorio, ListaRepositorio>();
builder.Services.AddScoped<IListaServicio, ListaServicio>();
builder.Services.AddScoped<IListaDetalleRepositorio, ListaDetalleRepositorio>();
builder.Services.AddScoped<IListaDetalleServicio, ListaDetalleServicio>();
builder.Services.AddScoped<IDatoConstanteRepositorio, DatoConstanteRepositorio>();
builder.Services.AddScoped<IDatoConstanteServicio, DatoConstanteServicio>();
builder.Services.AddScoped<IColaSolicitudRepositorio, ColaSolicitudRepositorio>();

builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajoEF>();

builder.Services.AddScoped(typeof(IEntidadValidador<>), typeof(EntidadValidador<>));

builder.Services.AddScoped<IApiResponse, ApiResponse>();
builder.Services.AddScoped<IMSSeguridad, MSSeguridad>();
builder.Services.AddScoped<IRespuestaHttpValidador, RespuestaHttpValidador>();
builder.Services.AddScoped<IColaSolicitudServicio, ColaSolicitudServicio>();
builder.Services.AddScoped<IJobEncoladorServicio, JobEncoladorServicio>();
builder.Services.AddScoped<IUsuarioContextoServicio, UsuarioContextoServicio>();

builder.Services.AddScoped<ISerializadorJsonServicio, SerializadorJsonServicio>();

#region REG_Servicios de configuraciones Appsettings
builder.Services.Configure<TrabajosColasSettings>(builder.Configuration.GetSection("TrabajosColas"));
builder.Services.AddSingleton<IConfiguracionesTrabajosColas, ConfiguracionesTrabajosColas>();

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.AddSingleton<IConfiguracionesJwt, ConfiguracionesJwt>();
#endregion

builder.Services.AddDbContext<AppDbContext>
    (opciones => opciones
    .UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    //ServerVersion.Parse("8.0.39-mysql")
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddHangfire(opciones =>
{
    opciones.UseStorage(
        new MySqlStorage(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            new MySqlStorageOptions { TablesPrefix = "XHAF_DCO_" }));
});

//Necesario para correr el background job server
builder.Services.AddHangfireServer(opciones => { opciones.ServerName = "MSDatosComunesServer"; });

//Servicio para obtener el usuarioId de los Tokens de la solicitud
builder.Services.AddHttpContextAccessor();

//Configuracion para llamado de otros MicroServicios
builder.Services.AddTransient<MiddlewareManejadorTokens>();
var urlGateway = builder.Configuration["UrlGateway"];
builder.Services.AddHttpClient<IMSSeguridadContextoWebServicio, MSSeguridadContextoWebServicio>
    (cliente =>
    {
        cliente.BaseAddress = new Uri(urlGateway);
        cliente.DefaultRequestHeaders.Add("Accept", "application/json");
    })
    .AddHttpMessageHandler<MiddlewareManejadorTokens>();

var app = builder.Build();

//Dashboard para ver los jobs en el navegador
app.UseHangfireDashboard("/hangfire");

//Configuracion para la tarea Job en segundo plano que rastrea las solicitudes pendientes de procesar.
var configuracionTrabajosColas = app.Services.GetRequiredService<IConfiguracionesTrabajosColas>();
RecurringJob.AddOrUpdate<IColaSolicitudServicio>("procesador_solicitudes", x => x.ProcesarColaSolicitudesAsync(),
    configuracionTrabajosColas.ObtenerProcesarColaSolicitudesCron());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<MiddlewareExcepcionesGlobales>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

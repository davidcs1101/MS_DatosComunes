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
using Refit;

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
var emisor = configuracionJWT["Emisor"];
var audiencia = configuracionJWT["Audiencia"];
var llave = configuracionJWT["Llave"];
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
            ValidIssuer = emisor,
            ValidAudience = audiencia,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(llave)),
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
builder.Services.AddScoped<IDatoConstanteDetalleRepositorio, DatoConstanteDetalleRepositorio>();
builder.Services.AddScoped<IDatoConstanteDetalleServicio, DatoConstanteDetalleServicio>();


builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajoEF>();

builder.Services.AddScoped(typeof(IEntidadValidador<>), typeof(EntidadValidador<>));

builder.Services.AddSingleton<IApisResponse, ApisResponse>();
builder.Services.AddSingleton<IMSSeguridad, MSSeguridad>();
builder.Services.AddSingleton<IRespuestaHttpValidador, RespuestaHttpValidador>();
builder.Services.AddScoped<IColaSolicitudServicio, ColaSolicitudServicio>();
builder.Services.AddScoped<IJobEncoladorServicio, JobEncoladorServicio>();
builder.Services.AddScoped<IUsuarioContextoServicio, UsuarioContextoServicio>();

builder.Services.AddSingleton<ISerializadorJsonServicio, SerializadorJsonServicio>();

builder.Services.AddScoped<IProcesadorTransacciones, ProcesadorTransacciones>();
builder.Services.AddSingleton<IServicioComun, ServicioComun>();

#region REG_Servicios de configuraciones Appsettings
builder.Services.Configure<TrabajosColasSettings>(builder.Configuration.GetSection("TrabajosColas"));
builder.Services.AddSingleton<IConfiguracionesTrabajosColas, ConfiguracionesTrabajosColas>();

builder.Services.Configure<EventosNotificarSettings>(builder.Configuration.GetSection("EventosNotificar"));
builder.Services.AddSingleton<IConfiguracionesEventosNotificar, ConfiguracionesEventosNotificar>();
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

//Servicio para obtener el usuarioId de los Tokens de la solicitud Web
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<MiddlewareManejadorTokens>();
builder.Services.AddTransient<MiddlewareManejadorTokensBackGround>();

//Configuracion para llamado de otros MicroServicios atraves de la Url Gateway
var urlMsSeguridad = builder.Configuration["UrlMSSeguridad"];

builder.Services
    .AddRefitClient<IMSSeguridadContextoWebServicio>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(urlMsSeguridad);
        c.DefaultRequestHeaders.Add("Accept", "application/json");
    })
    .AddHttpMessageHandler<MiddlewareManejadorTokens>();

builder.Services
    .AddRefitClient<IMSSeguridadBackgroundServicio>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(urlMsSeguridad);
        c.DefaultRequestHeaders.Add("Accept", "application/json");
    });

builder.Services
    .AddHttpClient<IPublicadorEventosBackgroundServicio, PublicadorEventosBackgroundServicio>
    (cliente =>
    {
        cliente.DefaultRequestHeaders.Add("Accept", "application/json");
    })
    .AddHttpMessageHandler<MiddlewareManejadorTokensBackGround>();

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

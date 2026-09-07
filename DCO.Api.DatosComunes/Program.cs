using DCO.Api.DatosComunes.Middlewares;
using DCO.Api.DatosComunes.Middlewares.Permisos;
using DCO.Aplicacion.CasosUso.Implementaciones;
using DCO.Aplicacion.CasosUso.Interfaces;
using DCO.Aplicacion.Servicios.Implementaciones;
using DCO.Aplicacion.Servicios.Implementaciones.Cache;
using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Aplicacion.Servicios.Interfaces.Cache;
using DCO.Aplicacion.ServiciosExternos;
using DCO.Aplicacion.ServiciosExternos.config;
using DCO.Aplicacion.ServiciosExternos.Mapeo;
using DCO.DataAccess;
using DCO.Dominio.Repositorio;
using DCO.Dominio.Repositorio.UnidadTrabajo;
using DCO.Dominio.Servicios.Implementaciones;
using DCO.Dominio.Servicios.Interfaces;
using DCO.Dtos.AppSettings;
using DCO.Infraestructura.Aplicacion.ServiciosExternos.Config;
using DCO.Infraestructura.Dominio.Repositorio;
using DCO.Infraestructura.Mapeo;
using DCO.Intraestructura.Dominio.Repositorio;
using DCO.Intraestructura.Dominio.Repositorio.UnidadTrabajo;
using Hangfire;
using Hangfire.MySql;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Refit;
using System.Text;
using Utilidades.Seguridad;
using Utilidades.Servicios.Http.Implementaciones;
using Utilidades.Servicios.Http.Interfaces;
using Utilidades.Servicios.Http.Interfaces.Contextos;
using Utilidades.Servicios.Responses.Implementaciones;
using Utilidades.Servicios.Serializacion.Implementaciones;
using Utilidades.Servicios.Serializacion.Interfaces;

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

//Mapperly
builder.Services.AddSingleton<IMapperPerfiles, MapperPerfiles>();

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


#region REG_Politicas de Autorizacion

builder.Services.AddAuthorization(options =>
{
    //Para política basada en permisos.
    options.AddPolicy(Politicas.PERMISO, policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new PermisoRequirement());
    });

    //Para política basada en los "CodigoGrupo" con valor específicos.
    options.AddPolicy(Politicas.GRUPOSFUNCIONESSISTEMA, policy =>
    {
        policy.RequireAssertion(context =>
        {
            var grupo = context.User.FindFirst(Claims.CodigoGrupo)?.Value;
            return
            grupo == CodigosGrupos.ADMINISTRADORSISTEMA ||
            grupo == CodigosGrupos.MSINTEGRACION;
        });
    });

});

#endregion

builder.Services.AddScoped<IListaRepositorio, ListaRepositorio>();
builder.Services.AddScoped<IListaServicio, ListaServicio>();
builder.Services.AddScoped<IListaDetalleRepositorio, ListaDetalleRepositorio>();
builder.Services.AddScoped<IListaDetalleServicio, ListaDetalleServicio>();
builder.Services.AddScoped<IDatoConstanteRepositorio, DatoConstanteRepositorio>();
builder.Services.AddScoped<IDatoConstanteServicio, DatoConstanteServicio>();
builder.Services.AddScoped<IColaSolicitudRepositorio, ColaSolicitudRepositorio>();
builder.Services.AddScoped<IDatoConstanteDetalleRepositorio, DatoConstanteDetalleRepositorio>();
builder.Services.AddScoped<IDatoConstanteDetalleServicio, DatoConstanteDetalleServicio>();

builder.Services.AddScoped<IMunicipioRepositorio, MunicipioRepositorio>();
builder.Services.AddScoped<IGeografiaServicio, GeografiaServicio>();

builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajoEF>();

builder.Services.AddScoped(typeof(IEntidadValidador<>), typeof(EntidadValidador<>));

builder.Services.AddSingleton<Utilidades.Servicios.Responses.Interfaces.IApiResponse, ApiResponse>();
builder.Services.AddSingleton<IMSSeguridad, MSSeguridad>();
builder.Services.AddScoped<IMSSeguridadAutenticacion, MSSeguridadAutenticacion>();

//Para cachear datos de otros microservicios
builder.Services.AddSingleton<ISeguridadPermisosCache, SeguridadPermisosCache>();

//Para cachear tokens de seguridad de acceso de usuarios
builder.Services.AddMemoryCache();

//IMPORTANTE: esta clase es la que permite que se evalue todo el tema de permisos a nivel de cada EndPoint
builder.Services.AddSingleton<IAuthorizationHandler, PermisoManejadorAutorizacion>();

builder.Services.AddSingleton<IRespuestaHttpValidador, RespuestaHttpValidador>();
builder.Services.AddScoped<IColaSolicitudServicio, ColaSolicitudServicio>();
builder.Services.AddScoped<IJobEncoladorServicio, JobEncoladorServicio>();
builder.Services.AddScoped<IUsuarioContextoServicio, UsuarioContextoServicio>();

builder.Services.AddSingleton<ISerializadorJsonServicio, SerializadorJsonServicio>();

builder.Services.AddScoped<IProcesadorTransacciones, ProcesadorTransacciones>();
builder.Services.AddSingleton<IServicioEjecutorHttp, ServicioEjecutorHttp>();

#region REG_Servicios de configuraciones Appsettings
builder.Services.Configure<TrabajosColasSettings>(builder.Configuration.GetSection("TrabajosColas"));
builder.Services.Configure<EventosNotificarSettings>(builder.Configuration.GetSection("EventosNotificar"));
builder.Services.AddSingleton<IAppSettings, AppSettings>();
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
builder.Services.AddTransient<MiddlewareManejadorTokensBackground>();

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
    })
    .AddHttpMessageHandler<MiddlewareManejadorTokensBackground>();

builder.Services
    .AddRefitClient<IMSSeguridadAutenticacionServicio>()
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
    .AddHttpMessageHandler<MiddlewareManejadorTokensBackground>();

var app = builder.Build();

//Dashboard para ver los jobs en el navegador
app.UseHangfireDashboard("/hangfire");

//Configuracion para la tarea Job en segundo plano que rastrea las solicitudes pendientes de procesar.
var configuracionTrabajosColas = app.Services.GetRequiredService<IAppSettings>();
RecurringJob.AddOrUpdate<IColaSolicitudServicio>("procesador_solicitudes", x => x.ProcesarColaSolicitudesAsync(),
    configuracionTrabajosColas.ObtenerTrabajosColasSettings().ProcesarColaSolicitudesCron);

// Se configura un job para inicializar la caché de permisos desde la base de datos al iniciar el microservicio y luego se programa para que se ejecute periódicamente.
BackgroundJob.Enqueue<ISeguridadPermisosCache>(x => x.InicializarAsync());
RecurringJob.AddOrUpdate<ISeguridadPermisosCache>("inicializar_permisos", x => x.InicializarAsync(),
    configuracionTrabajosColas.ObtenerTrabajosColasSettings().ProcesarColaSolicitudesCron);

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

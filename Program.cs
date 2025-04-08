using Microsoft.Extensions.FileProviders;
using System.IO;
using TFG_BACK.Controllers;
using TFG_BACK.Repositories;
using TFG_BACK.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("bbddAcademIQ");
var connectionStringEnvironmentVar = Environment.GetEnvironmentVariable("ConnectionStrings__bbddAcademIQ");

// Configurar una cadena de conexión con límites muy altos
var connectionStringWithHighLimits = connectionString;// + 
   // ";Max Pool Size=1000;Min Pool Size=10;Connection Lifetime=0;Connection Timeout=120;Default Command Timeout=120;";

// Configurar límites de tamaño para subida de archivos grandes
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 500 * 1024 * 1024; // 500 MB
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 500 * 1024 * 1024; // 500 MB
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(5);
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 500 * 1024 * 1024; // 500 MB
    options.ValueLengthLimit = 500 * 1024 * 1024;
    options.MultipartHeadersLengthLimit = 8192;
});

// // Añadir monitoreo de salud para la base de datos
// builder.Services.AddHealthChecks()
//     .AddMySql(connectionStringWithHighLimits, name: "database", failureStatus: HealthStatus.Degraded);

// // Registrar servicio de logs
// builder.Services.AddLogging(logging =>
// {
//     logging.AddConsole();
//     logging.AddDebug();
// });

// REPOSITORY con conexión con límites altos
builder.Services.AddScoped<IAsignaturaRepository, AsignaturaRepository>(provider =>
    new AsignaturaRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<IDetalleQuizzRepository, DetalleQuizzRepository>(provider =>
    new DetalleQuizzRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<IPreferenciasRepository, PreferenciasRepository>(provider =>
    new PreferenciasRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<IQuizRepository, QuizRepository>(provider =>
    new QuizRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<IRolRepository, RolRepository>(provider =>
    new RolRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<ISeguimientoRepository, SeguimientoRepository>(provider =>
    new SeguimientoRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(provider =>
    new UsuarioRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<IUsuarioAsignaturaRepository, UsuarioAsignaturaRepository>(provider =>
    new UsuarioAsignaturaRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<IVideoRepository, VideoRepository>(provider =>
    new VideoRepository(connectionStringWithHighLimits));






// SERVICE
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioAsignaturaService, UsuarioAsignaturaService>();
builder.Services.AddScoped<IAsignaturaService, AsignaturaService>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IDetalleQuizzService, DetalleQuizzService>();
builder.Services.AddScoped<IPreferenciasService, PreferenciasService>();
builder.Services.AddScoped<ISeguimientoService, SeguimientoService>();



// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.MaxIAsyncEnumerableBufferLimit = 500 * 1024 * 1024; // 500 MB
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Añadir endpoint de health check para monitorear la salud de la aplicación
// app.UseHealthChecks("/health", new HealthCheckOptions
// {
//     ResponseWriter = async (context, report) =>
//     {
//         context.Response.ContentType = "application/json";
//         var result = System.Text.Json.JsonSerializer.Serialize(
//             new
//             {
//                 status = report.Status.ToString(),
//                 checks = report.Entries.Select(e => new
//                 {
//                     name = e.Key,
//                     status = e.Value.Status.ToString(),
//                     description = e.Value.Description
//                 })
//             });
//         await context.Response.WriteAsync(result);
//     }
// });

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();
app.MapControllers();
app.Run();
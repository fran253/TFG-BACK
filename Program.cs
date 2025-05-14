using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Http.Features;
// Elimina o comenta esta línea:
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// Elimina o comenta esta línea:
// using Microsoft.IdentityModel.Tokens;
using TFG_BACK.Services;
using System.Text;

// Crear el builder
var builder = WebApplication.CreateBuilder(args);

// ---------------------------- Conexión a la base de datos ----------------------------
var connectionString = builder.Configuration.GetConnectionString("bbddAcademIQ");

builder.Services.AddDbContext<AcademIQDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));



// ---------------------------- Configuración de archivos grandes ----------------------------
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 500 * 1024 * 1024;
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 500 * 1024 * 1024;
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(5);
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 500 * 1024 * 1024;
    options.ValueLengthLimit = 500 * 1024 * 1024;
    options.MultipartHeadersLengthLimit = 8192;
});

// ---------------------------- CORS ----------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// ---------------------------- Comentamos la Configuración de autenticación JWT ----------------------------
// Comenta o elimina todo este bloque
/*
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
*/

// Opcionalmente, podemos agregar una autenticación básica si la necesitas
// Sin usar paquetes externos
builder.Services.AddAuthentication();

// ---------------------------- Servicios (AddScoped) ----------------------------

// CORE
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IPreferenciasService, PreferenciasService>();

// CURSOS / ASIGNATURAS
builder.Services.AddScoped<IAsignaturaService, AsignaturaService>();
builder.Services.AddScoped<IUsuarioCursoService, UsuarioCursoService>();
builder.Services.AddScoped<IUsuarioAsignaturaService, UsuarioAsignaturaService>();
builder.Services.AddScoped<ICursoService, CursoService>();

// VIDEOS
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IMarcadorVideoService, MarcadorVideoService>();
builder.Services.AddScoped<IComentarioVideoService, ComentarioVideoService>();
builder.Services.AddScoped<IFavoritoService, FavoritoService>();

// QUIZZES
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IDetalleQuizService, DetalleQuizService>();
builder.Services.AddScoped<IResultadoQuizService, ResultadoQuizService>();

// RELACIONES
builder.Services.AddScoped<ISeguimientoService, SeguimientoService>();

//S3
builder.Services.AddScoped<IS3UploaderService, S3UploaderService>();


// ---------------------------- Controladores y Swagger ----------------------------
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------------------------- Construcción del app ----------------------------
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

// Debe ir antes de MapControllers para que funcione la autenticación/autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
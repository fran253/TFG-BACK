using Microsoft.EntityFrameworkCore;
using TuProyecto.Models;

public class AcademIQDbContext : DbContext
{
    public AcademIQDbContext(DbContextOptions<AcademIQDbContext> options) : base(options) { }

    // Entidades existentes QUE FUNCIONAN
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Asignatura> Asignaturas { get; set; }
    public DbSet<UsuarioCurso> UsuarioCursos { get; set; }
    public DbSet<UsuarioAsignatura> UsuarioAsignaturas { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<MarcadorVideo> MarcadoresVideo { get; set; }
    public DbSet<ComentarioVideo> ComentariosVideo { get; set; }
    public DbSet<Favorito> Favoritos { get; set; }
    public DbSet<Seguimiento> Seguimientos { get; set; }
    public DbSet<ReporteVideo> ReportesVideo { get; set; }

    // QUIZ SYSTEM - TODOS LOS DbSets NECESARIOS
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Pregunta> Preguntas { get; set; }
    public DbSet<Respuesta> Respuestas { get; set; }
    public DbSet<ResultadoQuiz> ResultadosQuiz { get; set; }
    public DbSet<ValoracionQuiz> ValoracionesQuiz { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // SOLO las configuraciones que YA FUNCIONAN
        modelBuilder.Entity<UsuarioCurso>()
            .HasKey(uc => new { uc.IdUsuario, uc.IdCurso });

        modelBuilder.Entity<UsuarioAsignatura>()
            .HasKey(ua => new { ua.IdUsuario, ua.IdAsignatura });

        modelBuilder.Entity<Favorito>()
            .HasKey(f => new { f.IdUsuario, f.IdVideo });

        modelBuilder.Entity<Seguimiento>()
            .HasKey(s => new { s.IdAlumno, s.IdProfesor });

        // Configurar las relaciones entre Seguimiento y Usuario (ESTAS FUNCIONAN)
        modelBuilder.Entity<Seguimiento>()
            .HasOne(s => s.Alumno)
            .WithMany(u => u.Seguidores)
            .HasForeignKey(s => s.IdAlumno)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Seguimiento>()
            .HasOne(s => s.Profesor)
            .WithMany(u => u.Seguidos)
            .HasForeignKey(s => s.IdProfesor)
            .OnDelete(DeleteBehavior.Restrict);

        // NO CONFIGURAR RELACIONES DE QUIZ POR AHORA
        // Dejamos que Entity Framework use las convenciones por defecto

        // Configurar nombres de tablas
        modelBuilder.Entity<Asignatura>().ToTable("Asignatura");
        modelBuilder.Entity<ComentarioVideo>().ToTable("ComentarioVideo");
        modelBuilder.Entity<Curso>().ToTable("Curso");
        modelBuilder.Entity<Favorito>().ToTable("Favorito");
        modelBuilder.Entity<MarcadorVideo>().ToTable("MarcadorVideo");
        modelBuilder.Entity<Rol>().ToTable("Rol");
        modelBuilder.Entity<Seguimiento>().ToTable("Seguimiento");
        modelBuilder.Entity<Usuario>().ToTable("Usuario");
        modelBuilder.Entity<UsuarioAsignatura>().ToTable("Usuario_Asignatura");
        modelBuilder.Entity<UsuarioCurso>().ToTable("Usuario_Curso");
        modelBuilder.Entity<Video>().ToTable("Video");
        
        // Nombres de tablas para Quiz system
        modelBuilder.Entity<Quiz>().ToTable("Quiz");
        modelBuilder.Entity<Pregunta>().ToTable("Pregunta");
        modelBuilder.Entity<Respuesta>().ToTable("Respuesta");
        modelBuilder.Entity<ResultadoQuiz>().ToTable("ResultadoQuiz");
        modelBuilder.Entity<ValoracionQuiz>().ToTable("ValoracionQuiz");
    }
}
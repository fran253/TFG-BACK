// Data/AcademIQDbContext.cs
using Microsoft.EntityFrameworkCore;
using TuProyecto.Models;

public class AcademIQDbContext : DbContext
{
    public AcademIQDbContext(DbContextOptions<AcademIQDbContext> options) : base(options) { }

    // Entidades existentes
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

    // NUEVO DISEÑO DE QUIZ
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Pregunta> Preguntas { get; set; }
    public DbSet<Respuesta> Respuestas { get; set; }
    public DbSet<ResultadoQuiz> ResultadosQuiz { get; set; }
    public DbSet<ValoracionQuiz> ValoracionesQuiz { get; set; }

    public DbSet<Seguimiento> Seguimientos { get; set; }
    public DbSet<ReporteVideo> ReportesVideo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuraciones de claves compuestas existentes
        modelBuilder.Entity<UsuarioCurso>()
            .HasKey(uc => new { uc.IdUsuario, uc.IdCurso });

        modelBuilder.Entity<UsuarioAsignatura>()
            .HasKey(ua => new { ua.IdUsuario, ua.IdAsignatura });

        modelBuilder.Entity<Favorito>()
            .HasKey(f => new { f.IdUsuario, f.IdVideo });

        modelBuilder.Entity<Seguimiento>()
            .HasKey(s => new { s.IdAlumno, s.IdProfesor });

        // Configuración para ValoracionQuiz (clave única)
        modelBuilder.Entity<ValoracionQuiz>()
            .HasIndex(v => new { v.IdUsuario, v.IdQuiz })
            .IsUnique();

        // Configurar las relaciones entre Seguimiento y Usuario
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

        // Configuraciones para las nuevas entidades de Quiz
        modelBuilder.Entity<Quiz>()
            .HasOne(q => q.Usuario)
            .WithMany() // Sin navegación inversa
            .HasForeignKey(q => q.IdUsuario)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Pregunta>()
            .HasOne(p => p.Quiz)
            .WithMany(q => q.Preguntas)
            .HasForeignKey(p => p.IdQuiz)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Respuesta>()
            .HasOne(r => r.Pregunta)
            .WithMany(p => p.Respuestas)
            .HasForeignKey(r => r.IdPregunta)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ResultadoQuiz>()
            .HasOne(r => r.Usuario)
            .WithMany(u => u.Resultados)
            .HasForeignKey(r => r.IdUsuario)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ResultadoQuiz>()
            .HasOne(r => r.Quiz)
            .WithMany(q => q.Resultados)
            .HasForeignKey(r => r.IdQuiz)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ValoracionQuiz>()
            .HasOne(v => v.Usuario)
            .WithMany()
            .HasForeignKey(v => v.IdUsuario)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ValoracionQuiz>()
            .HasOne(v => v.Quiz)
            .WithMany(q => q.Valoraciones)
            .HasForeignKey(v => v.IdQuiz)
            .OnDelete(DeleteBehavior.Cascade);

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
        
        // Nuevas tablas
        modelBuilder.Entity<Quiz>().ToTable("Quiz");
        modelBuilder.Entity<Pregunta>().ToTable("Pregunta");
        modelBuilder.Entity<Respuesta>().ToTable("Respuesta");
        modelBuilder.Entity<ResultadoQuiz>().ToTable("ResultadoQuiz");
        modelBuilder.Entity<ValoracionQuiz>().ToTable("ValoracionQuiz");
    }
}
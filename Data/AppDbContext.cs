using Microsoft.EntityFrameworkCore;
using TuProyecto.Models;

public class AcademIQDbContext : DbContext
{
    public AcademIQDbContext(DbContextOptions<AcademIQDbContext> options) : base(options) { }

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

    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<DetalleQuiz> DetallesQuiz { get; set; }
    public DbSet<ResultadoQuiz> ResultadosQuiz { get; set; }

    public DbSet<Seguimiento> Seguimientos { get; set; }
    public DbSet<ReporteVideo> ReportesVideo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ---------------- Claves compuestas ----------------
        modelBuilder.Entity<UsuarioCurso>()
            .HasKey(uc => new { uc.IdUsuario, uc.IdCurso });

        modelBuilder.Entity<UsuarioAsignatura>()
            .HasKey(ua => new { ua.IdUsuario, ua.IdAsignatura });

        modelBuilder.Entity<Favorito>()
            .HasKey(f => new { f.IdUsuario, f.IdVideo });

        modelBuilder.Entity<Seguimiento>()
            .HasKey(s => new { s.IdAlumno, s.IdProfesor });

        // ---------------- Relaciones de UsuarioCurso ----------------
        modelBuilder.Entity<UsuarioCurso>()
            .HasOne(uc => uc.Usuario)
            .WithMany(u => u.UsuarioCursos)
            .HasForeignKey(uc => uc.IdUsuario)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UsuarioCurso>()
            .HasOne(uc => uc.Curso)
            .WithMany(c => c.UsuarioCursos)
            .HasForeignKey(uc => uc.IdCurso)
            .OnDelete(DeleteBehavior.Cascade);

        // ---------------- Relaciones de UsuarioAsignatura ----------------
        modelBuilder.Entity<UsuarioAsignatura>()
            .HasOne(ua => ua.Usuario)
            .WithMany(u => u.UsuarioAsignaturas)
            .HasForeignKey(ua => ua.IdUsuario)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UsuarioAsignatura>()
            .HasOne(ua => ua.Asignatura)
            .WithMany(a => a.UsuarioAsignaturas)
            .HasForeignKey(ua => ua.IdAsignatura)
            .OnDelete(DeleteBehavior.Cascade);

        // ---------------- Relaciones de Seguimiento ----------------
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

        // ---------------- Nombres de tablas ----------------
        modelBuilder.Entity<Asignatura>().ToTable("Asignatura");
        modelBuilder.Entity<ComentarioVideo>().ToTable("ComentarioVideo");
        modelBuilder.Entity<Curso>().ToTable("Curso");
        modelBuilder.Entity<DetalleQuiz>().ToTable("DetalleQuiz");
        modelBuilder.Entity<Favorito>().ToTable("Favorito");
        modelBuilder.Entity<MarcadorVideo>().ToTable("MarcadorVideo");
        modelBuilder.Entity<Quiz>().ToTable("Quiz");
        modelBuilder.Entity<ResultadoQuiz>().ToTable("ResultadoQuiz");
        modelBuilder.Entity<Rol>().ToTable("Rol");
        modelBuilder.Entity<Seguimiento>().ToTable("Seguimiento");
        modelBuilder.Entity<Usuario>().ToTable("Usuario");
        modelBuilder.Entity<UsuarioAsignatura>().ToTable("Usuario_Asignatura");
        modelBuilder.Entity<UsuarioCurso>().ToTable("Usuario_Curso");
        modelBuilder.Entity<Video>().ToTable("Video");
        modelBuilder.Entity<ReporteVideo>().ToTable("ReporteVideo");
    }
}

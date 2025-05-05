using Microsoft.EntityFrameworkCore;

public class AcademIQDbContext : DbContext
{
    public AcademIQDbContext(DbContextOptions<AcademIQDbContext> options) : base(options) { }

    public DbSet<Rol> Roles { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Preferencias> Preferencias { get; set; }

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Clave compuesta UsuarioCurso
        modelBuilder.Entity<UsuarioCurso>()
            .HasKey(uc => new { uc.IdUsuario, uc.IdCurso });

        // Clave compuesta UsuarioAsignatura
        modelBuilder.Entity<UsuarioAsignatura>()
            .HasKey(ua => new { ua.IdUsuario, ua.IdAsignatura });

        // Clave compuesta Favorito
        modelBuilder.Entity<Favorito>()
            .HasKey(f => new { f.IdUsuario, f.IdVideo });

        // Clave compuesta Seguimiento
        modelBuilder.Entity<Seguimiento>()
            .HasKey(s => new { s.IdAlumno, s.IdProfesor });
    }
}

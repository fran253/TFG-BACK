using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Usuario
{
    [Key]
    public int IdUsuario { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; }

    public string? Apellidos { get; set; }

    [Required]
    [EmailAddress]
    public string Gmail { get; set; }

    public string? Telefono { get; set; }

    [Required]
    public string Contraseña { get; set; }

    [ForeignKey("Rol")]
    public int IdRol { get; set; }
    public Rol Rol { get; set; }

    [ForeignKey("Preferencias")]
    public int? IdPreferencia { get; set; }
    public Preferencias? Preferencias { get; set; }

    public ICollection<Seguimiento> Seguidores { get; set; }
    public ICollection<Seguimiento> Seguidos { get; set; }
    public ICollection<UsuarioCurso> UsuarioCursos { get; set; }
    public ICollection<UsuarioAsignatura> UsuarioAsignaturas { get; set; }
    public ICollection<Video> Videos { get; set; }
    public ICollection<ComentarioVideo> Comentarios { get; set; }
    public ICollection<Favorito> Favoritos { get; set; }
    public ICollection<Quiz> Quizzes { get; set; }
    public ICollection<ResultadoQuiz> Resultados { get; set; }
}

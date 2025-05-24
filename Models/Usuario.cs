// Models/Usuario.cs (sin navegación a Quizzes)
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public Rol? Rol { get; set; } 

    [JsonIgnore]
    public ICollection<Seguimiento> Seguidores { get; set; }

    [JsonIgnore]
    public ICollection<Seguimiento> Seguidos { get; set; }

    [JsonIgnore]
    public ICollection<UsuarioCurso> UsuarioCursos { get; set; }

    [JsonIgnore]
    public ICollection<UsuarioAsignatura> UsuarioAsignaturas { get; set; }

    [JsonIgnore]
    public ICollection<Video> Videos { get; set; }

    [JsonIgnore]
    public ICollection<ComentarioVideo> Comentarios { get; set; }

    [JsonIgnore]
    public ICollection<Favorito> Favoritos { get; set; }

    // SIN QUIZZES - se eliminó la línea
    [JsonIgnore]
    public ICollection<ResultadoQuiz> Resultados { get; set; }

    public Usuario()
    {
        Seguidores = new List<Seguimiento>();
        Seguidos = new List<Seguimiento>();
        UsuarioCursos = new List<UsuarioCurso>();
        UsuarioAsignaturas = new List<UsuarioAsignatura>();
        Videos = new List<Video>();
        Comentarios = new List<ComentarioVideo>();
        Favoritos = new List<Favorito>();
        // Quizzes = new List<Quiz>(); // ELIMINADO
        Resultados = new List<ResultadoQuiz>();
    }
}
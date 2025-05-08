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
    
    [JsonIgnore] // Añade esta línea
    public Rol Rol { get; set; }

    [ForeignKey("Preferencias")]
    public int? IdPreferencia { get; set; }
    
    [JsonIgnore] // También agrega esto para evitar posibles ciclos con Preferencias
    public Preferencias? Preferencias { get; set; }

    // También recomendaría agregar [JsonIgnore] a todas estas colecciones
    // para evitar problemas similares con otras entidades
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
    
    [JsonIgnore]
    public ICollection<Quiz> Quizzes { get; set; }
    
    [JsonIgnore]
    public ICollection<ResultadoQuiz> Resultados { get; set; }
}

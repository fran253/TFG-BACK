using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; // Añade esto

public class Asignatura
{
    [Key]
    public int IdAsignatura { get; set; }

    [Required]
    [MaxLength(255)]
    public string Nombre { get; set; }

    public string? Imagen { get; set; }

    public string? Descripcion { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [ForeignKey("Curso")]
    public int IdCurso { get; set; }
    
    [JsonIgnore] // Añade esta línea
    public Curso Curso { get; set; }

    public ICollection<UsuarioAsignatura> UsuarioAsignaturas { get; set; }
    public ICollection<Video> Videos { get; set; }
    public ICollection<Quiz> Quizzes { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Quiz
{
    [Key]
    public int IdQuiz { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; }

    public string? Descripcion { get; set; }

    [Required]
    public int IdUsuario { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public int IdCurso { get; set; }
    public int IdAsignatura { get; set; }

    public ICollection<Pregunta> Preguntas { get; set; }

    public ICollection<ResultadoQuiz> Resultados { get; set; }

    public ICollection<ValoracionQuiz> Valoraciones { get; set; }
    // NO PROPIEDADES DE NAVEGACIÓN POR AHORA
    // Entity Framework las está interpretando mal
}
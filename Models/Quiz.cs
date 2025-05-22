using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Quiz
{
    [Key]
    public int IdQuiz { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; }

    public string? Descripcion { get; set; }

    [ForeignKey("Usuario")]
    public int IdUsuario { get; set; }
    
    [JsonIgnore]
    public Usuario Usuario { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public ICollection<Pregunta> Preguntas { get; set; } = new List<Pregunta>();
    
    [JsonIgnore]
    public ICollection<ResultadoQuiz> Resultados { get; set; } = new List<ResultadoQuiz>();
    
    [JsonIgnore]
    public ICollection<ValoracionQuiz> Valoraciones { get; set; } = new List<ValoracionQuiz>();
}
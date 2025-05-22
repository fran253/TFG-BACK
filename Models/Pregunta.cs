using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Pregunta
{
    [Key]
    public int IdPregunta { get; set; }

    [ForeignKey("Quiz")]
    public int IdQuiz { get; set; }
    
    [JsonIgnore]
    public Quiz Quiz { get; set; }

    [Required]
    public string Descripcion { get; set; }

    public int Orden { get; set; } = 1;

    public ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
}
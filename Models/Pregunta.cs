using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Pregunta
{
    [Key]
    public int IdPregunta { get; set; }

    [Required]
    public int IdQuiz { get; set; }
        
    [JsonIgnore]
    public Quiz Quiz { get; set; }

    [Required]
    public string Descripcion { get; set; }

    public int Orden { get; set; } = 1;

    public ICollection<Respuesta> Respuestas { get; set; }

    // NO propiedades de navegación por ahora
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Respuesta
{
    [Key]
    public int IdRespuesta { get; set; }

    [ForeignKey("Pregunta")]
    public int IdPregunta { get; set; }
    
    [JsonIgnore]
    public Pregunta Pregunta { get; set; }

    [Required]
    [MaxLength(255)]
    public string Texto { get; set; }

    public bool EsCorrecta { get; set; } = false;

    public int Orden { get; set; } = 1;
}
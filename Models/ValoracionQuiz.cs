using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class ValoracionQuiz
{
    [Key]
    public int IdValoracion { get; set; }

    [Required]
    public int IdUsuario { get; set; }

    [Required]
    public int IdQuiz { get; set; }
    [JsonIgnore]
    public Quiz Quiz { get; set; }

    [Required]
    [Range(1, 5)]
    public int Puntuacion { get; set; }

    [MaxLength(500)]
    public string? Comentario { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

}
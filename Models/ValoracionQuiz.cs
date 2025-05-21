using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class ValoracionQuiz
{
    [Key]
    public int IdValoracion { get; set; }

    [ForeignKey("Usuario")]
    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; }

    [ForeignKey("Quiz")]
    public int IdQuiz { get; set; }
    public Quiz Quiz { get; set; }

    [Required]
    [Range(1, 5)]
    public int Puntuacion { get; set; }

    [MaxLength(500)]
    public string? Comentario { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;
}
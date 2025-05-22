using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class ResultadoQuiz
{
    [Key]
    public int IdResultado { get; set; }

    [ForeignKey("Usuario")]
    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; }

    [ForeignKey("Quiz")]
    public int IdQuiz { get; set; }
    public Quiz Quiz { get; set; }

    public decimal Puntuacion { get; set; }

    public string? RespuestasSeleccionadas { get; set; } // JSON

    public DateTime Fecha { get; set; } = DateTime.Now;
}

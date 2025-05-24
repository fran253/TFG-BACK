using System.ComponentModel.DataAnnotations;

public class ResultadoQuiz
{
    [Key]
    public int IdResultado { get; set; }

    [Required]
    public int IdUsuario { get; set; }

    [Required]
    public int IdQuiz { get; set; }

    public decimal Puntuacion { get; set; }

    public string? RespuestasSeleccionadas { get; set; } 

    public DateTime Fecha { get; set; } = DateTime.Now;

}
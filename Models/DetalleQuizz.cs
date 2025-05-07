using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DetalleQuiz
{
    [Key]
    public int IdDetalleQuiz { get; set; }

    [ForeignKey("Quiz")]
    public int IdQuiz { get; set; }
    public Quiz Quiz { get; set; }

    [Required]
    public string Pregunta { get; set; }

    [Required]
    public string Opciones { get; set; } // Se recomienda JSON si quieres múltiples opciones marcables
}

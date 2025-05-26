using System.ComponentModel.DataAnnotations;

public class Pregunta
{
    [Key]
    public int IdPregunta { get; set; }

    [Required]
    public int IdQuiz { get; set; }

    [Required]
    public string Descripcion { get; set; }

    public int Orden { get; set; } = 1;

    // NO propiedades de navegación por ahora
}
using System.ComponentModel.DataAnnotations;

public class Quiz
{
    [Key]
    public int IdQuiz { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; }

    public string? Descripcion { get; set; }

    [Required]
    public int IdUsuario { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    // NO PROPIEDADES DE NAVEGACIÓN POR AHORA
    // Entity Framework las está interpretando mal
}
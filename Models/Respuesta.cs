using System.ComponentModel.DataAnnotations;

public class Respuesta
{
    [Key]
    public int IdRespuesta { get; set; }

    [Required]
    public int IdPregunta { get; set; }

    [Required]
    [MaxLength(255)]
    public string Texto { get; set; }

    public bool EsCorrecta { get; set; } = false;

    public int Orden { get; set; } = 1;

    // NO propiedades de navegación por ahora
}
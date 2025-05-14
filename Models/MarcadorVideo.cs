using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MarcadorVideo
{
    [Key]
    public int IdMarcador { get; set; }

    [ForeignKey("Video")]
    public int IdVideo { get; set; }
    public Video Video { get; set; }

    [Required]
    public decimal MinutoImportante { get; set; }  // Nueva propiedad

    public string? Titulo { get; set; }
}

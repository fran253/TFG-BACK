using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class MarcadorVideo
{
    [Key]
    public int IdMarcador { get; set; }

    [ForeignKey("Video")]
    public int IdVideo { get; set; }
    [JsonIgnore]
    public Video Video { get; set; }

    [Required]
    public decimal MinutoImportante { get; set; } 

    public string? Titulo { get; set; }
}

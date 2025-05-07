using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ComentarioVideo
{
    [Key]
    public int IdComentario { get; set; }

    [ForeignKey("Video")]
    public int IdVideo { get; set; }
    public Video Video { get; set; }

    [ForeignKey("Usuario")]
    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; }

    [Required]
    public string Texto { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Video
{
    [Key]
    public int IdVideo { get; set; }

    [Required]
    [MaxLength(150)]
    public string Titulo { get; set; }

    public string? Descripcion { get; set; }

    [Required]
    public string Url { get; set; }

    public string? Miniatura { get; set; }

    public DateTime FechaSubida { get; set; } = DateTime.Now;

    [ForeignKey("Asignatura")]
    public int IdAsignatura { get; set; }
    public Asignatura Asignatura { get; set; }

    [ForeignKey("Usuario")]
    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; }

    public ICollection<MarcadorVideo> Marcadores { get; set; }
    public ICollection<ComentarioVideo> Comentarios { get; set; }
    public ICollection<Favorito> Favoritos { get; set; }
}

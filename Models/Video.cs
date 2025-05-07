using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
<<<<<<< HEAD
using System.Text.Json.Serialization;
=======
>>>>>>> 80ab0711d3b5cb08e81a76ee11431b31e2e0bac9

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
    
    [JsonIgnore]
    public Asignatura Asignatura { get; set; }

    [ForeignKey("Usuario")]
    public int IdUsuario { get; set; }
    
    [JsonIgnore]
    public Usuario Usuario { get; set; }
    
    // Nueva propiedad para la relación con Curso
    [ForeignKey("Curso")]
    public int? IdCurso { get; set; }
    
    [JsonIgnore]
    public Curso? Curso { get; set; }

    [JsonIgnore]
    public ICollection<MarcadorVideo> Marcadores { get; set; }
    
    [JsonIgnore]
    public ICollection<ComentarioVideo> Comentarios { get; set; }
    
    [JsonIgnore]
    public ICollection<Favorito> Favoritos { get; set; }
}

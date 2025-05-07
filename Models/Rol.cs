using System.ComponentModel.DataAnnotations;

public class Rol
{
    [Key]
    public int IdRol { get; set; }

    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; }

    public ICollection<Usuario> Usuarios { get; set; }
}

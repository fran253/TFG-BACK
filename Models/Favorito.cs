using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Favorito
{
    [Key, Column(Order = 0)]
    public int IdUsuario { get; set; }

    [Key, Column(Order = 1)]
    public int IdVideo { get; set; }

    [ForeignKey("IdUsuario")]
    public Usuario Usuario { get; set; }

    [ForeignKey("IdVideo")]
    public Video Video { get; set; }
}

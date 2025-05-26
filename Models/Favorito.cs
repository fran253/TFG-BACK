using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Favorito
{
    [Key, Column(Order = 0)]
    public int IdUsuario { get; set; }

    [Key, Column(Order = 1)]
    public int IdVideo { get; set; }

    [ForeignKey(nameof(IdUsuario))]
    public virtual Usuario Usuario { get; set; }

    [ForeignKey(nameof(IdVideo))]
    public virtual Video Video { get; set; }
}

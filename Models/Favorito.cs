using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Favorito
{
    [Key, Column(Order = 0)]
    public int IdUsuario { get; set; }

    [Key, Column(Order = 1)]
    public int IdVideo { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    public Usuario Usuario { get; set; }
    public Video Video { get; set; }
}

namespace TFG_BACK.Models;
public class Preferencias
{
    public int IdPreferencia { get; set; }
    public string ColorFondo { get; set; }
    public string ColorBordes { get; set; }
    public string ImagenFondo { get; set; }

    public ICollection<Usuario> Usuarios { get; set; }
}

namespace TFG_BACK.Models;

public class Usuario
{
    public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; }
    public string Gmail { get; set; }
    public string Contraseña { get; set; }
    public string NumeroTelefono { get; set; }
    
    public int IdRol { get; set; }
    public Rol Rol { get; set; }

    public int IdPreferencia { get; set; }
    public Preferencias Preferencias { get; set; }

    public int IdSeguimiento { get; set; }
    public Seguimiento Seguimiento { get; set; }

}

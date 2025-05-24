using Microsoft.AspNetCore.Http;

public class CursoCrearDTO
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public IFormFile Imagen { get; set; }
    public int IdUsuario { get; set; } 
}
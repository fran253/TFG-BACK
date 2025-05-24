using Microsoft.AspNetCore.Http;

public class CrearPeticionProfesorRequest
{
    public int IdUsuario { get; set; }
    public string Texto { get; set; }
    public IFormFile Documentacion { get; set; }
}
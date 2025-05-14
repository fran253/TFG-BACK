using TuProyecto.Api.DTOs.Marcador;

public class RegistrarVideoRequest
{
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public int IdAsignatura { get; set; }
    public int IdUsuario { get; set; }
    public int? IdCurso { get; set; }  // <-- Aquí debe estar
    public IFormFile Video { get; set; }
    public IFormFile Miniatura { get; set; }
    public List<MarcadorRequest> Marcadores { get; set; }
}

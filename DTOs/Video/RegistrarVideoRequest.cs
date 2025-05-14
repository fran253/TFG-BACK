namespace TuProyecto.Api.DTOs.Video
{
    using TuProyecto.Api.DTOs.Marcador;
    using System.Collections.Generic;

    public class RegistrarVideoRequest
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string UrlVideo { get; set; }
        public string UrlMiniatura { get; set; }
        public int IdAsignatura { get; set; }
        public int IdUsuario { get; set; }
        public List<MarcadorDto> Marcadores { get; set; }
    }
}

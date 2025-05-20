public class ReporteVideoRespuestaDTO
{
    public int IdReporte { get; set; }
    public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public int IdVideo { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
}

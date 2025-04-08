namespace TFG_BACK.Models;
public class DetalleQuizz
{
    public int IdDetalleQuizz { get; set; } 
    public int IdQuizz { get; set; }
    public Quiz Quiz { get; set; }

    public string Pregunta { get; set; }
    public string Opciones { get; set; }
}

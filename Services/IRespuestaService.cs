using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRespuestaService
{
    Task<List<Respuesta>> GetByPreguntaIdAsync(int idPregunta);
    Task<Respuesta?> GetByIdAsync(int id);
    Task<int> AddAsync(Respuesta respuesta);
    Task UpdateAsync(Respuesta respuesta);
    Task DeleteAsync(int id);
    
    // Validaciones
    Task<bool> ValidarLimiteRespuestasAsync(int idPregunta);
    Task<int> ContarRespuestasDeLaPreguntaAsync(int idPregunta);
    Task<bool> ValidarAlMenosUnaCorrectaAsync(int idPregunta);
}
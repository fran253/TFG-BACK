using Microsoft.EntityFrameworkCore;

public class RespuestaService : IRespuestaService
{
    private readonly AcademIQDbContext _context;
    private const int MAX_RESPUESTAS_POR_PREGUNTA = 4;

    public RespuestaService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<Respuesta>> GetByPreguntaIdAsync(int idPregunta)
    {
        return await _context.Respuestas
            .Where(r => r.IdPregunta == idPregunta)
            .OrderBy(r => r.Orden)
            .ToListAsync();
    }

    public async Task<Respuesta?> GetByIdAsync(int id)
    {
        return await _context.Respuestas
            .FirstOrDefaultAsync(r => r.IdRespuesta == id);
    }

    public async Task<int> AddAsync(Respuesta respuesta)
    {
        // Validar límite de respuestas por pregunta
        if (!await ValidarLimiteRespuestasAsync(respuesta.IdPregunta))
        {
            throw new InvalidOperationException($"La pregunta ha alcanzado el límite máximo de {MAX_RESPUESTAS_POR_PREGUNTA} respuestas.");
        }

        // Asignar orden automáticamente
        var ultimoOrden = await _context.Respuestas
            .Where(r => r.IdPregunta == respuesta.IdPregunta)
            .MaxAsync(r => (int?)r.Orden) ?? 0;
        
        respuesta.Orden = ultimoOrden + 1;

        _context.Respuestas.Add(respuesta);
        await _context.SaveChangesAsync();
        return respuesta.IdRespuesta;
    }

    public async Task UpdateAsync(Respuesta respuesta)
    {
        _context.Respuestas.Update(respuesta);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var respuesta = await _context.Respuestas.FindAsync(id);
        if (respuesta != null)
        {
            _context.Respuestas.Remove(respuesta);
            await _context.SaveChangesAsync();
            
            // Validar que quede al menos una respuesta correcta
            if (!await ValidarAlMenosUnaCorrectaAsync(respuesta.IdPregunta))
            {
                throw new InvalidOperationException("No se puede eliminar esta respuesta porque debe quedar al menos una respuesta correcta en la pregunta.");
            }
        }
    }

    public async Task<bool> ValidarLimiteRespuestasAsync(int idPregunta)
    {
        var count = await ContarRespuestasDeLaPreguntaAsync(idPregunta);
        return count < MAX_RESPUESTAS_POR_PREGUNTA;
    }

    public async Task<int> ContarRespuestasDeLaPreguntaAsync(int idPregunta)
    {
        return await _context.Respuestas.CountAsync(r => r.IdPregunta == idPregunta);
    }

    public async Task<bool> ValidarAlMenosUnaCorrectaAsync(int idPregunta)
    {
        var correctas = await _context.Respuestas
            .CountAsync(r => r.IdPregunta == idPregunta && r.EsCorrecta);
        return correctas >= 1;
    }
}
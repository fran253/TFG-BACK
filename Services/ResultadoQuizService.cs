using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ResultadoQuizService : IResultadoQuizService
{
    private readonly AcademIQDbContext _context;

    public ResultadoQuizService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<ResultadoQuiz>> GetAllAsync()
    {
        return await _context.ResultadosQuiz
            .Include(r => r.Usuario)
            .Include(r => r.Quiz)
            .ToListAsync();
    }

    public async Task<ResultadoQuiz?> GetByIdAsync(int id)
    {
        return await _context.ResultadosQuiz
            .Include(r => r.Usuario)
            .Include(r => r.Quiz)
            .FirstOrDefaultAsync(r => r.IdResultado == id);
    }

    public async Task<List<ResultadoQuiz>> GetByUsuarioAsync(int idUsuario)
    {
        return await _context.ResultadosQuiz
            .Where(r => r.IdUsuario == idUsuario)
            .Include(r => r.Quiz)
            .ToListAsync();
    }

    public async Task<List<ResultadoQuiz>> GetByQuizAsync(int idQuiz)
    {
        return await _context.ResultadosQuiz
            .Where(r => r.IdQuiz == idQuiz)
            .Include(r => r.Usuario)
            .ToListAsync();
    }

    public async Task AddAsync(ResultadoQuiz resultado)
    {
        _context.ResultadosQuiz.Add(resultado);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var resultado = await _context.ResultadosQuiz.FindAsync(id);
        if (resultado != null)
        {
            _context.ResultadosQuiz.Remove(resultado);
            await _context.SaveChangesAsync();
        }
    }
}

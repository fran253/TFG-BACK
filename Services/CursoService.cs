using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CursoService : ICursoService
{
    private readonly AcademIQDbContext _context;

    public CursoService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<Curso>> GetAllAsync()
    {
        return await _context.Cursos.ToListAsync();
    }

    public async Task<Curso?> GetByIdAsync(int id)
    {
        return await _context.Cursos.FindAsync(id);
    }

    public async Task AddAsync(Curso curso)
    {
        _context.Cursos.Add(curso);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Curso curso)
    {
        _context.Cursos.Update(curso);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var curso = await _context.Cursos.FindAsync(id);
        if (curso != null)
        {
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Curso?> AddCursoConUsuarioAsync(CursoCrearDTO dto, int idUsuario)
    {
        var nombreExiste = await _context.Cursos
            .AnyAsync(c => c.Nombre.ToLower() == dto.Nombre.ToLower());

        if (nombreExiste)
            return null;

        var nuevoCurso = new Curso
        {
            Nombre = dto.Nombre,
            Imagen = dto.Imagen,
            Descripcion = dto.Descripcion,
            FechaCreacion = DateTime.UtcNow,
            IdUsuario = idUsuario
        };

        _context.Cursos.Add(nuevoCurso);
        await _context.SaveChangesAsync();

        return nuevoCurso;
    }
    public async Task<List<CursoVideosDTO>> GetTopCursosConMasVideosAsync(int cantidad)
    {
        return await _context.Videos
            .Where(v => v.IdCurso != null)
            .GroupBy(v => new { v.Curso.IdCurso, v.Curso.Nombre })
            .Select(g => new CursoVideosDTO
            {
                NombreCurso = g.Key.Nombre,
                TotalVideos = g.Count()
            })
            .OrderByDescending(c => c.TotalVideos)
            .Take(cantidad)
            .ToListAsync();
    }
}

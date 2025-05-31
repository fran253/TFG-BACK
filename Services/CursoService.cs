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

    public async Task DeleteAsync(int id)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        
        try
        {
            // Verificar que el curso existe
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                await transaction.RollbackAsync();
                return;
            }

            // 1. Obtener las asignaturas del curso
            var asignaturasIds = await _context.Asignaturas
                .Where(a => a.IdCurso == id)
                .Select(a => a.IdAsignatura)
                .ToListAsync();

            foreach (var idAsignatura in asignaturasIds)
            {
                // 1.1. Eliminar todas las dependencias de quizzes de esta asignatura
                var quizzesIds = await _context.Quizzes
                    .Where(q => q.IdAsignatura == idAsignatura)
                    .Select(q => q.IdQuiz)
                    .ToListAsync();

                foreach (var idQuiz in quizzesIds)
                {
                    // Eliminar respuestas de preguntas de este quiz
                    var preguntasIds = await _context.Preguntas
                        .Where(p => p.IdQuiz == idQuiz)
                        .Select(p => p.IdPregunta)
                        .ToListAsync();

                    foreach (var idPregunta in preguntasIds)
                    {
                        // Eliminar respuestas de esta pregunta
                        var respuestasToDelete = await _context.Respuestas
                            .Where(r => r.IdPregunta == idPregunta)
                            .ToListAsync();
                        _context.Respuestas.RemoveRange(respuestasToDelete);
                    }

                    // Eliminar preguntas de este quiz
                    var preguntasToDelete = await _context.Preguntas
                        .Where(p => p.IdQuiz == idQuiz)
                        .ToListAsync();
                    _context.Preguntas.RemoveRange(preguntasToDelete);
                }

                // Eliminar quizzes de esta asignatura
                var quizzesToDelete = await _context.Quizzes
                    .Where(q => q.IdAsignatura == idAsignatura)
                    .ToListAsync();
                _context.Quizzes.RemoveRange(quizzesToDelete);

                // 1.2. Eliminar todas las dependencias de videos de esta asignatura
                var videosIds = await _context.Videos
                    .Where(v => v.IdAsignatura == idAsignatura)
                    .Select(v => v.IdVideo)
                    .ToListAsync();

                foreach (var idVideo in videosIds)
                {
                    // Eliminar dependencias de este video
                    var reportesToDelete = await _context.ReportesVideo
                        .Where(r => r.IdVideo == idVideo)
                        .ToListAsync();
                    _context.ReportesVideo.RemoveRange(reportesToDelete);

                    var marcadoresToDelete = await _context.MarcadoresVideo
                        .Where(m => m.IdVideo == idVideo)
                        .ToListAsync();
                    _context.MarcadoresVideo.RemoveRange(marcadoresToDelete);

                    var comentariosToDelete = await _context.ComentariosVideo
                        .Where(c => c.IdVideo == idVideo)
                        .ToListAsync();
                    _context.ComentariosVideo.RemoveRange(comentariosToDelete);

                    var favoritosToDelete = await _context.Favoritos
                        .Where(f => f.IdVideo == idVideo)
                        .ToListAsync();
                    _context.Favoritos.RemoveRange(favoritosToDelete);
                }

                // Eliminar videos de esta asignatura
                var videosToDelete = await _context.Videos
                    .Where(v => v.IdAsignatura == idAsignatura)
                    .ToListAsync();
                _context.Videos.RemoveRange(videosToDelete);

                // 1.3. Eliminar relaciones usuario-asignatura
                var usuarioAsignaturasToDelete = await _context.UsuarioAsignaturas
                    .Where(ua => ua.IdAsignatura == idAsignatura)
                    .ToListAsync();
                _context.UsuarioAsignaturas.RemoveRange(usuarioAsignaturasToDelete);
            }

            // Guardar cambios hasta ahora
            await _context.SaveChangesAsync();

            // 2. Eliminar las asignaturas del curso
            var asignaturasToDelete = await _context.Asignaturas
                .Where(a => a.IdCurso == id)
                .ToListAsync();
            _context.Asignaturas.RemoveRange(asignaturasToDelete);

            // 3. Eliminar videos directos del curso (que tienen IdCurso)
            var videosDirectosIds = await _context.Videos
                .Where(v => v.IdCurso == id)
                .Select(v => v.IdVideo)
                .ToListAsync();

            foreach (var idVideo in videosDirectosIds)
            {
                // Eliminar dependencias de videos directos
                var reportesToDelete = await _context.ReportesVideo
                    .Where(r => r.IdVideo == idVideo)
                    .ToListAsync();
                _context.ReportesVideo.RemoveRange(reportesToDelete);

                var marcadoresToDelete = await _context.MarcadoresVideo
                    .Where(m => m.IdVideo == idVideo)
                    .ToListAsync();
                _context.MarcadoresVideo.RemoveRange(marcadoresToDelete);

                var comentariosToDelete = await _context.ComentariosVideo
                    .Where(c => c.IdVideo == idVideo)
                    .ToListAsync();
                _context.ComentariosVideo.RemoveRange(comentariosToDelete);

                var favoritosToDelete = await _context.Favoritos
                    .Where(f => f.IdVideo == idVideo)
                    .ToListAsync();
                _context.Favoritos.RemoveRange(favoritosToDelete);
            }

            // Eliminar videos directos del curso
            var videosDirectosToDelete = await _context.Videos
                .Where(v => v.IdCurso == id)
                .ToListAsync();
            _context.Videos.RemoveRange(videosDirectosToDelete);

            // 4. Eliminar relaciones usuario-curso
            var usuarioCursosToDelete = await _context.UsuarioCursos
                .Where(uc => uc.IdCurso == id)
                .ToListAsync();
            _context.UsuarioCursos.RemoveRange(usuarioCursosToDelete);

            // 5. Finalmente, eliminar el curso
            _context.Cursos.Remove(curso);

            // Guardar todos los cambios restantes
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new InvalidOperationException($"Error al eliminar el curso: {ex.Message}", ex);
        }
    }

    // Resto de métodos existentes...
    public async Task<List<Curso>> GetAllAsync()
    {
        return await _context.Cursos.ToListAsync();
    }

    public async Task<Curso?> GetByIdAsync(int id)
    {
        return await _context.Cursos.FindAsync(id);
    }
    
    public async Task<List<Curso>> GetCursosPorUsuarioAsync(int idUsuario)
    {
        return await _context.UsuarioCursos
            .Where(uc => uc.IdUsuario == idUsuario)
            .Select(uc => uc.Curso)
            .Include(c => c.Asignaturas)
            .ToListAsync();
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
 
    public async Task<Curso?> AddCursoConUsuarioAsync(CursoCrearDTO dto, int idUsuario, string? urlImagen = null)
    {
        var nombreExiste = await _context.Cursos
            .AnyAsync(c => c.Nombre.ToLower() == dto.Nombre.ToLower());

        if (nombreExiste)
            return null;

        var nuevoCurso = new Curso
        {
            Nombre = dto.Nombre,
            Imagen = urlImagen,
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

    public async Task<int> ContarCursosPorUsuarioAsync(int idUsuario)
    {
        return await _context.Cursos.CountAsync(c => c.IdUsuario == idUsuario);
    }

    public async Task<Curso> ObtenerUltimoCursoPorUsuarioAsync(int idUsuario)
    {
        return await _context.Cursos
            .Where(c => c.IdUsuario == idUsuario)
            .OrderByDescending(c => c.FechaCreacion)
            .FirstOrDefaultAsync();
    }
}
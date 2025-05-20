// Modificar Services/UsuarioService.cs
using Microsoft.EntityFrameworkCore;

public class UsuarioService : IUsuarioService
{
    private readonly AcademIQDbContext _context;

    public UsuarioService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios
            .Include(u => u.Rol)
            .ToListAsync();
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.IdUsuario == id);
    }

    public async Task<Usuario?> GetByGmailAsync(string gmail)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Gmail == gmail);
    }

    public async Task AddAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var u = await _context.Usuarios.FindAsync(id);
        if (u != null)
        {
            _context.Usuarios.Remove(u);
            await _context.SaveChangesAsync();
        }
    }

    // Nuevos métodos para manejar cursos seguidos
    public async Task<List<Curso>> GetCursosSeguidosAsync(int idUsuario)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);
        
        if (usuario == null || !usuario.CursosSeguidos.Any())
            return new List<Curso>();
        
        return await _context.Cursos
            .Where(c => usuario.CursosSeguidos.Contains(c.IdCurso))
            .ToListAsync();
    }

    public async Task SeguirCursoAsync(int idUsuario, int idCurso)
    {
        var usuario = await _context.Usuarios.FindAsync(idUsuario);
        if (usuario == null)
            throw new Exception("Usuario no encontrado");
        
        if (!usuario.CursosSeguidos.Contains(idCurso))
        {
            usuario.CursosSeguidos.Add(idCurso);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DejarDeSeguirCursoAsync(int idUsuario, int idCurso)
    {
        var usuario = await _context.Usuarios.FindAsync(idUsuario);
        if (usuario == null)
            return;
        
        if (usuario.CursosSeguidos.Contains(idCurso))
        {
            usuario.CursosSeguidos.Remove(idCurso);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> SigueCursoAsync(int idUsuario, int idCurso)
    {
        var usuario = await _context.Usuarios.FindAsync(idUsuario);
        if (usuario == null)
            return false;
        
        return usuario.CursosSeguidos.Contains(idCurso);
    }
}
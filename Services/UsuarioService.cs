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
}

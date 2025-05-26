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
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            // 1. Eliminar comentarios
            var comentarios = _context.ComentariosVideo.Where(c => c.IdUsuario == id);
            _context.ComentariosVideo.RemoveRange(comentarios);

            // 2. Eliminar videos
            var videos = _context.Videos.Where(v => v.IdUsuario == id);
            _context.Videos.RemoveRange(videos);

            // 3. Eliminar favoritos, reportes, relaciones, etc.
            var favoritos = _context.Favoritos.Where(f => f.IdUsuario == id);
            _context.Favoritos.RemoveRange(favoritos);

            var usuarioCursos = _context.UsuarioCursos.Where(uc => uc.IdUsuario == id);
            _context.UsuarioCursos.RemoveRange(usuarioCursos);

            // 4. Finalmente, eliminar el usuario
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }

    //TOKEN
    public async Task<Usuario?> GetByTokenAsync(string token)
    {
        return await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Token == token);
    }

    //funcion para el grafico
    public async Task<List<RolEstadisticaDTO>> ObtenerEstadisticasPorRol()
    {
        return await _context.Usuarios
            .Include(u => u.Rol)
            .GroupBy(u => u.Rol.Nombre)
            .Select(g => new RolEstadisticaDTO
            {
                Rol = g.Key,
                Total = g.Count()
            })
            .ToListAsync();
    }

}

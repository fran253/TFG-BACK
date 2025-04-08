using Dapper;
using MySql.Data.MySqlClient;
using TFG_BACK.Models;

namespace TFG_BACK.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"
                SELECT 
                    u.IdUsuario, u.NombreUsuario, u.Gmail, u.Contraseña, u.NumeroTelefono,
                    u.IdRol, r.IdRol, r.Nombre,
                    u.IdPreferencia, p.IdPreferencia, p.ColorFondo, p.ColorBordes, p.ImagenFondo,
                    u.IdSeguimiento, s.IdSeguimiento, s.IdAlumno, s.IdProfesor
                FROM Usuario u
                JOIN Rol r ON u.IdRol = r.IdRol
                JOIN Preferencias p ON u.IdPreferencia = p.IdPreferencia
                JOIN Seguimiento s ON u.IdSeguimiento = s.IdSeguimiento";

            var usuarios = await connection.QueryAsync<Usuario, Rol, Preferencias, Seguimiento, Usuario>(
                query,
                (usuario, rol, pref, seg) =>
                {
                    usuario.Rol = rol;
                    usuario.Preferencias = pref;
                    usuario.Seguimiento = seg;
                    return usuario;
                },
                splitOn: "IdRol,IdPreferencia,IdSeguimiento"
            );

            return usuarios.ToList();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"
                SELECT 
                    u.IdUsuario, u.NombreUsuario, u.Gmail, u.Contraseña, u.NumeroTelefono,
                    u.IdRol, r.IdRol, r.Nombre,
                    u.IdPreferencia, p.IdPreferencia, p.ColorFondo, p.ColorBordes, p.ImagenFondo,
                    u.IdSeguimiento, s.IdSeguimiento, s.IdAlumno, s.IdProfesor
                FROM Usuario u
                JOIN Rol r ON u.IdRol = r.IdRol
                JOIN Preferencias p ON u.IdPreferencia = p.IdPreferencia
                JOIN Seguimiento s ON u.IdSeguimiento = s.IdSeguimiento
                WHERE u.IdUsuario = @Id";

            var result = await connection.QueryAsync<Usuario, Rol, Preferencias, Seguimiento, Usuario>(
                query,
                (usuario, rol, pref, seg) =>
                {
                    usuario.Rol = rol;
                    usuario.Preferencias = pref;
                    usuario.Seguimiento = seg;
                    return usuario;
                },
                new { Id = id },
                splitOn: "IdRol,IdPreferencia,IdSeguimiento"
            );

            return result.FirstOrDefault();
        }

        public async Task AddAsync(Usuario user)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"
                INSERT INTO Usuario (NombreUsuario, Gmail, Contraseña, NumeroTelefono, IdRol, IdPreferencia, IdSeguimiento)
                VALUES (@NombreUsuario, @Gmail, @Contraseña, @NumeroTelefono, @IdRol, @IdPreferencia, @IdSeguimiento)";
            
            await connection.ExecuteAsync(query, user);
        }

        public async Task UpdateAsync(Usuario user)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"
                UPDATE Usuario
                SET NombreUsuario = @NombreUsuario,
                    Gmail = @Gmail,
                    Contraseña = @Contraseña,
                    NumeroTelefono = @NumeroTelefono,
                    IdRol = @IdRol,
                    IdPreferencia = @IdPreferencia,
                    IdSeguimiento = @IdSeguimiento
                WHERE IdUsuario = @IdUsuario";
            
            await connection.ExecuteAsync(query, user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "DELETE FROM Usuario WHERE IdUsuario = @Id";
            var result = await connection.ExecuteAsync(query, new { Id = id });
            return result > 0;
        }
    }
}

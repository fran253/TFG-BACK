using Dapper;
using MySql.Data.MySqlClient;
using TFG_BACK.Models;

namespace TFG_BACK.Repositories
{
    public class UsuarioAsignaturaRepository : IUsuarioAsignaturaRepository
    {
        private readonly string _connectionString;

        public UsuarioAsignaturaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<UsuarioAsignatura>> GetAllAsync()
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "SELECT IdUsuario, IdAsignatura FROM UsuarioAsignatura";
            var relaciones = await connection.QueryAsync<UsuarioAsignatura>(query);
            return relaciones.ToList();
        }

        public async Task<List<Asignatura>> GetByUsuarioIdAsync(int idUsuario)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"
                SELECT a.IdAsignatura, a.Nombre, a.Descripcion
                FROM UsuarioAsignatura ua
                JOIN Asignatura a ON ua.IdAsignatura = a.IdAsignatura
                WHERE ua.IdUsuario = @IdUsuario";
            
            var asignaturas = await connection.QueryAsync<Asignatura>(query, new { IdUsuario = idUsuario });
            return asignaturas.ToList();
        }

        public async Task AddAsync(UsuarioAsignatura relacion)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"INSERT INTO UsuarioAsignatura (IdUsuario, IdAsignatura)
                          VALUES (@IdUsuario, @IdAsignatura)";
            await connection.ExecuteAsync(query, relacion);
        }

        public async Task<bool> DeleteAsync(int idUsuario, int idAsignatura)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"DELETE FROM UsuarioAsignatura 
                          WHERE IdUsuario = @IdUsuario AND IdAsignatura = @IdAsignatura";
            
            var result = await connection.ExecuteAsync(query, new { IdUsuario = idUsuario, IdAsignatura = idAsignatura });
            return result > 0;
        }
    }
}

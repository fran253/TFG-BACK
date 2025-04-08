using Dapper;
using MySql.Data.MySqlClient;
using TFG_BACK.Models;

namespace TFG_BACK.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly string _connectionString;

        public RolRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Rol>> GetAllAsync()
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "SELECT IdRol, Nombre FROM Rol";
            var roles = await connection.QueryAsync<Rol>(query);
            return roles.ToList();
        }

        public async Task<Rol> GetByIdAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "SELECT IdRol, Nombre FROM Rol WHERE IdRol = @Id";
            return await connection.QueryFirstOrDefaultAsync<Rol>(query, new { Id = id });
        }

        public async Task AddAsync(Rol rol)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "INSERT INTO Rol (Nombre) VALUES (@Nombre)";
            await connection.ExecuteAsync(query, new { rol.Nombre });
        }

        public async Task UpdateAsync(Rol rol)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "UPDATE Rol SET Nombre = @Nombre WHERE IdRol = @IdRol";
            await connection.ExecuteAsync(query, new { rol.Nombre, rol.IdRol });
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "DELETE FROM Rol WHERE IdRol = @Id";
            var result = await connection.ExecuteAsync(query, new { Id = id });
            return result > 0;
        }
    }
}

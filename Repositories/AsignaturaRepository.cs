using Dapper;
using MySql.Data.MySqlClient;
using TFG_BACK.Models;

namespace TFG_BACK.Repositories
{
    public class AsignaturaRepository : IAsignaturaRepository
    {
        private readonly string _connectionString;

        public AsignaturaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Asignatura>> GetAllAsync()
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "SELECT IdAsignatura, Nombre, Descripcion FROM Asignatura";
            var asignaturas = await connection.QueryAsync<Asignatura>(query);
            return asignaturas.ToList();
        }

        public async Task<Asignatura> GetByIdAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "SELECT IdAsignatura, Nombre, Descripcion FROM Asignatura WHERE IdAsignatura = @Id";
            return await connection.QueryFirstOrDefaultAsync<Asignatura>(query, new { Id = id });
        }

        public async Task AddAsync(Asignatura asignatura)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"INSERT INTO Asignatura (Nombre, Descripcion) 
                          VALUES (@Nombre, @Descripcion)";
            await connection.ExecuteAsync(query, asignatura);
        }

        public async Task UpdateAsync(Asignatura asignatura)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"UPDATE Asignatura 
                          SET Nombre = @Nombre, Descripcion = @Descripcion 
                          WHERE IdAsignatura = @IdAsignatura";
            await connection.ExecuteAsync(query, asignatura);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "DELETE FROM Asignatura WHERE IdAsignatura = @Id";
            var result = await connection.ExecuteAsync(query, new { Id = id });
            return result > 0;
        }
    }
}

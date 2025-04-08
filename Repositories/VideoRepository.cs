using Dapper;
using MySql.Data.MySqlClient;
using TFG_BACK.Models;

namespace TFG_BACK.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly string _connectionString;

        public VideoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Video>> GetAllAsync()
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "SELECT IdVideo, NombreVideo, Descripcion, MeGusta, Miniatura, IdTablaMinutos, IdAsignatura, IdUsuario FROM Video";
            var videos = await connection.QueryAsync<Video>(query);
            return videos.ToList();
        }

        public async Task<Video> GetByIdAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "SELECT IdVideo, NombreVideo, Descripcion, MeGusta, Miniatura, IdTablaMinutos, IdAsignatura, IdUsuario FROM Video WHERE IdVideo = @Id";
            return await connection.QueryFirstOrDefaultAsync<Video>(query, new { Id = id });
        }

        public async Task AddAsync(Video video)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"INSERT INTO Video (NombreVideo, Descripcion, MeGusta, Miniatura, IdTablaMinutos, IdAsignatura, IdUsuario)
                          VALUES (@NombreVideo, @Descripcion, @MeGusta, @Miniatura, @IdTablaMinutos, @IdAsignatura, @IdUsuario)";
            await connection.ExecuteAsync(query, video);
        }

        public async Task UpdateAsync(Video video)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = @"UPDATE Video 
                          SET NombreVideo = @NombreVideo, Descripcion = @Descripcion, MeGusta = @MeGusta, 
                              Miniatura = @Miniatura, IdTablaMinutos = @IdTablaMinutos, 
                              IdAsignatura = @IdAsignatura, IdUsuario = @IdUsuario 
                          WHERE IdVideo = @IdVideo";
            await connection.ExecuteAsync(query, video);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            var query = "DELETE FROM Video WHERE IdVideo = @Id";
            var result = await connection.ExecuteAsync(query, new { Id = id });
            return result > 0;
        }
    }
}

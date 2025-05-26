using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class FavoritoController : ControllerBase
{
    private readonly IFavoritoService _service;
    private readonly IUsuarioService _usuarioService;

    public FavoritoController(IFavoritoService service, IUsuarioService usuarioService)
    {
        _service = service;
        _usuarioService = usuarioService;
    }

    [HttpGet("usuario/{idUsuario}")]
    public async Task<ActionResult<List<Video>>> GetFavoritos(int idUsuario)
    {
        var videos = await _service.GetFavoritosPorUsuario(idUsuario);
        return Ok(videos);
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] FavoritoDTO dto)
    {
        var favorito = new Favorito
        {
            IdUsuario = dto.IdUsuario,
            IdVideo = dto.IdVideo
        };

        await _service.AddAsync(favorito);
        return Ok();
    }

    [HttpDelete("{idUsuario}/{idVideo}")]
    public async Task<ActionResult> Delete(int idUsuario, int idVideo)
    {
        await _service.DeleteAsync(idUsuario, idVideo);
        return NoContent();
    }
    
    [HttpPost("toggle/{idVideo}")]
    public async Task<ActionResult> ToggleFavorito(int idVideo)
    {
        try
        {
            // CORREGIDO: Extraer token del header Authorization
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized(new { message = "Token de autorización requerido" });
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();
            
            var usuario = await _usuarioService.GetByTokenAsync(token);
            if (usuario == null) 
            {
                return Unauthorized(new { message = "Token inválido o expirado" });
            }

            Console.WriteLine($"Toggle favorito - Usuario: {usuario.IdUsuario}, Video: {idVideo}");

            var liked = await _service.ToggleFavoritoAsync(usuario.IdUsuario, idVideo);
            
            return Ok(new { liked = liked });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en ToggleFavorito: {ex.Message}");
            return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
        }
    }

    [HttpGet("usuario-likea/{idVideo}")]
    public async Task<ActionResult<bool>> UsuarioHaLikeado(int idVideo)
    {
        try
        {
            // CORREGIDO: Extraer token del header Authorization
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                return Unauthorized(new { message = "Token de autorización requerido" });
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();
            
            var usuario = await _usuarioService.GetByTokenAsync(token);
            if (usuario == null) 
            {
                return Unauthorized(new { message = "Token inválido o expirado" });
            }

            var likeado = await _service.ExisteFavorito(usuario.IdUsuario, idVideo);
            return Ok(likeado);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en UsuarioHaLikeado: {ex.Message}");
            return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
        }
    }
}
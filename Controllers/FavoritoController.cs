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
    public async Task<ActionResult> ToggleFavorito(int idVideo, [FromHeader] string token)
    {
        var usuario = await _usuarioService.GetByTokenAsync(token);
        if (usuario == null) return Unauthorized();

        var liked = await _service.ToggleFavoritoAsync(usuario.IdUsuario, idVideo);
        return Ok(new { liked });
    }

    [HttpGet("usuario-likea/{idVideo}")]
    public async Task<ActionResult<bool>> UsuarioHaLikeado(int idVideo, [FromHeader] string token)
    {
        var usuario = await _usuarioService.GetByTokenAsync(token);
        if (usuario == null) return Unauthorized();

        var likeado = await _service.ExisteFavorito(usuario.IdUsuario, idVideo);
        return Ok(likeado);
    }


}

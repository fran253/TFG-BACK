using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class FavoritoController : ControllerBase
{
    private readonly IFavoritoService _service;

    public FavoritoController(IFavoritoService service)
    {
        _service = service;
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
}

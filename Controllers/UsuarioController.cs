using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _service;
    private readonly IFavoritoService _favoritoService;

    public UsuarioController(IUsuarioService service, IFavoritoService favoritoService)
    {
        _service = service;
        _favoritoService = favoritoService;
    }



    [HttpGet]
    public async Task<ActionResult<List<Usuario>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetById(int id)
    {
        var usuario = await _service.GetByIdAsync(id);
        return usuario == null ? NotFound() : Ok(usuario);
    }

    [HttpGet("gmail/{gmail}")]
    public async Task<ActionResult<Usuario>> GetByGmail(string gmail)
    {
        var usuario = await _service.GetByGmailAsync(gmail);
        return usuario == null ? NotFound() : Ok(usuario);
    }

    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] Usuario usuario)
    {
        await _service.AddAsync(usuario);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Editar(int id, [FromBody] Usuario usuario)
    {
        if (id != usuario.IdUsuario) return BadRequest();
        await _service.UpdateAsync(usuario);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("{id}/aceptar-profesor")]
    public async Task<ActionResult> AceptarComoProfesor(int id)
    {
        var usuario = await _service.GetByIdAsync(id);
        if (usuario == null)
            return NotFound("Usuario no encontrado");

        usuario.IdRol = 2;
        await _service.UpdateAsync(usuario);

        return Ok(new { mensaje = "Usuario actualizado a profesor" });
    }

    [HttpGet("estadisticas-roles")]
    public async Task<ActionResult<List<RolEstadisticaDTO>>> GetUsuariosPorRol()
    {
        var resultado = await _service.ObtenerEstadisticasPorRol();
        return Ok(resultado);
    }

    [HttpGet("top-usuarios-videos")]
    public async Task<ActionResult<List<UsuarioVideosDTO>>> GetUsuariosConMasVideos()
    {
        var resultado = await _service.GetUsuariosConMasVideosAsync(10);
        return Ok(resultado);
    }

    [HttpGet("usuario-likea/{idVideo}")]
    public async Task<ActionResult<bool>> UsuarioHaLikeado(int idVideo, [FromHeader] string token)
    {
        var usuario = await _service.GetByTokenAsync(token);
        if (usuario == null) return Unauthorized();

        var likeado = await _favoritoService.ExisteFavorito(usuario.IdUsuario, idVideo);
        return Ok(likeado);
    }
}

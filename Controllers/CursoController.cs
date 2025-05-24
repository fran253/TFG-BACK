using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFG_BACK.Services; 

[Route("api/[controller]")]
[ApiController]
public class CursoController : ControllerBase
{

    private readonly ICursoService _service;
    private readonly IUsuarioService _usuarioService;
    private readonly ICursoService _cursoService;
    private readonly IS3UploaderService _s3UploaderService; 

    public CursoController(ICursoService service, IUsuarioService usuarioService, ICursoService cursoService, IS3UploaderService s3UploaderService) 
    {
        _service = service;
        _usuarioService = usuarioService;
        _cursoService = cursoService;
        _s3UploaderService = s3UploaderService; 
    }
    

    [HttpGet]
    public async Task<ActionResult<List<Curso>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Curso>> GetById(int id)
    {
        var curso = await _service.GetByIdAsync(id);
        return curso == null ? NotFound() : Ok(curso);
    }

    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] Curso curso)
    {
        await _service.AddAsync(curso);
        return CreatedAtAction(nameof(GetById), new { id = curso.IdCurso }, curso);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Editar(int id, [FromBody] Curso curso)
    {
        if (id != curso.IdCurso) return BadRequest();
        await _service.UpdateAsync(curso);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("crear")]
    public async Task<ActionResult> CrearCurso([FromForm] CursoCrearDTO dto)
    {
        // Subir imagen a S3 si existe
        string urlImagen = null;
        if (dto.Imagen != null)
        {
            urlImagen = await _s3UploaderService.SubirArchivoAsync(dto.Imagen, "cursos");
        }

        var curso = await _service.AddCursoConUsuarioAsync(dto, dto.IdUsuario, urlImagen);

        if (curso == null)
            return BadRequest("Ya existe un curso con ese nombre.");

        return CreatedAtAction(nameof(GetById), new { id = curso.IdCurso }, curso);
    }

    [HttpGet("top-cursos-videos")]
    public async Task<ActionResult<List<CursoVideosDTO>>> GetTopCursosConMasVideos()
    {
        var resultado = await _cursoService.GetTopCursosConMasVideosAsync(4);
        return Ok(resultado);
    }
}

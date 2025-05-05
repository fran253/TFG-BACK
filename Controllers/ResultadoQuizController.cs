using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ResultadoQuizController : ControllerBase
{
    private readonly IResultadoQuizService _service;

    public ResultadoQuizController(IResultadoQuizService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ResultadoQuiz>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResultadoQuiz>> GetById(int id)
    {
        var resultado = await _service.GetByIdAsync(id);
        return resultado == null ? NotFound() : Ok(resultado);
    }

    [HttpGet("usuario/{idUsuario}")]
    public async Task<ActionResult<List<ResultadoQuiz>>> GetByUsuario(int idUsuario)
    {
        return Ok(await _service.GetByUsuarioAsync(idUsuario));
    }

    [HttpGet("quiz/{idQuiz}")]
    public async Task<ActionResult<List<ResultadoQuiz>>> GetByQuiz(int idQuiz)
    {
        return Ok(await _service.GetByQuizAsync(idQuiz));
    }

    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] ResultadoQuiz resultado)
    {
        await _service.AddAsync(resultado);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}

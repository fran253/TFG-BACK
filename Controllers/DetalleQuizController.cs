using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class DetalleQuizController : ControllerBase
{
    private readonly IDetalleQuizService _service;

    public DetalleQuizController(IDetalleQuizService service)
    {
        _service = service;
    }

    [HttpGet("quiz/{idQuiz}")]
    public async Task<ActionResult<List<DetalleQuiz>>> GetByQuiz(int idQuiz)
    {
        return Ok(await _service.GetByQuizIdAsync(idQuiz));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DetalleQuiz>> GetById(int id)
    {
        var detalle = await _service.GetByIdAsync(id);
        return detalle == null ? NotFound() : Ok(detalle);
    }

    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] DetalleQuiz detalle)
    {
        await _service.AddAsync(detalle);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Editar(int id, [FromBody] DetalleQuiz detalle)
    {
        if (id != detalle.IdDetalleQuiz)
            return BadRequest("ID no coincide.");
        await _service.UpdateAsync(detalle);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}

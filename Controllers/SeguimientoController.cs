using Microsoft.AspNetCore.Mvc;
using TFG_BACK.Models;
using TFG_BACK.Services;

namespace TFG_BACK.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeguimientoController : ControllerBase
    {
        private readonly ISeguimientoService _seguimientoService;

        public SeguimientoController(ISeguimientoService seguimientoService)
        {
            _seguimientoService = seguimientoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seguimiento>>> GetAll()
        {
            var seguimientos = await _seguimientoService.GetAllAsync();
            return Ok(seguimientos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Seguimiento>> GetById(int id)
        {
            var seguimiento = await _seguimientoService.GetByIdAsync(id);
            if (seguimiento == null)
                return NotFound();
            return Ok(seguimiento);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Seguimiento seguimiento)
        {
            await _seguimientoService.AddAsync(seguimiento);
            return CreatedAtAction(nameof(GetById), new { id = seguimiento.IdSeguimiento }, seguimiento);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Seguimiento seguimiento)
        {
            if (id != seguimiento.IdSeguimiento)
                return BadRequest();

            var existente = await _seguimientoService.GetByIdAsync(id);
            if (existente == null)
                return NotFound();

            await _seguimientoService.UpdateAsync(seguimiento);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminado = await _seguimientoService.DeleteAsync(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}

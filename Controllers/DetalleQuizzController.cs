using Microsoft.AspNetCore.Mvc;
using TFG_BACK.Models;
using TFG_BACK.Services;

namespace TFG_BACK.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleQuizzController : ControllerBase
    {
        private readonly IDetalleQuizzService _detalleQuizzService;

        public DetalleQuizzController(IDetalleQuizzService detalleQuizzService)
        {
            _detalleQuizzService = detalleQuizzService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleQuizz>>> GetAll()
        {
            var detalles = await _detalleQuizzService.GetAllAsync();
            return Ok(detalles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleQuizz>> GetById(int id)
        {
            var detalle = await _detalleQuizzService.GetByIdAsync(id);
            if (detalle == null)
                return NotFound();
            return Ok(detalle);
        }

        [HttpPost]
        public async Task<ActionResult> Create(DetalleQuizz detalle)
        {
            await _detalleQuizzService.AddAsync(detalle);
            return CreatedAtAction(nameof(GetById), new { id = detalle.IdDetalleQuizz }, detalle);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, DetalleQuizz detalle)
        {
            if (id != detalle.IdDetalleQuizz)
                return BadRequest();

            var existe = await _detalleQuizzService.GetByIdAsync(id);
            if (existe == null)
                return NotFound();

            await _detalleQuizzService.UpdateAsync(detalle);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminado = await _detalleQuizzService.DeleteAsync(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}

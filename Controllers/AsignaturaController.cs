using Microsoft.AspNetCore.Mvc;
using TFG_BACK.Models;
using TFG_BACK.Services;

namespace TFG_BACK.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsignaturaController : ControllerBase
    {
        private readonly IAsignaturaService _asignaturaService;

        public AsignaturaController(IAsignaturaService asignaturaService)
        {
            _asignaturaService = asignaturaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignatura>>> GetAll()
        {
            var asignaturas = await _asignaturaService.GetAllAsync();
            return Ok(asignaturas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Asignatura>> GetById(int id)
        {
            var asignatura = await _asignaturaService.GetByIdAsync(id);
            if (asignatura == null)
                return NotFound();
            return Ok(asignatura);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Asignatura asignatura)
        {
            await _asignaturaService.AddAsync(asignatura);
            return CreatedAtAction(nameof(GetById), new { id = asignatura.IdAsignatura }, asignatura);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Asignatura asignatura)
        {
            if (id != asignatura.IdAsignatura)
                return BadRequest();

            var existing = await _asignaturaService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _asignaturaService.UpdateAsync(asignatura);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _asignaturaService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}

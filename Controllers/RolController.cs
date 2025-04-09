using Microsoft.AspNetCore.Mvc;
using TFG_BACK.Models;
using TFG_BACK.Services;

namespace TFG_BACK.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> GetAll()
        {
            var roles = await _rolService.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> GetById(int id)
        {
            var rol = await _rolService.GetByIdAsync(id);
            if (rol == null)
                return NotFound();
            return Ok(rol);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Rol rol)
        {
            await _rolService.AddAsync(rol);
            return CreatedAtAction(nameof(GetById), new { id = rol.IdRol }, rol);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Rol rol)
        {
            if (id != rol.IdRol)
                return BadRequest();

            var existente = await _rolService.GetByIdAsync(id);
            if (existente == null)
                return NotFound();

            await _rolService.UpdateAsync(rol);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminado = await _rolService.DeleteAsync(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}

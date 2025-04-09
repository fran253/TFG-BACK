using Microsoft.AspNetCore.Mvc;
using TFG_BACK.Models;
using TFG_BACK.Services;

namespace TFG_BACK.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreferenciasController : ControllerBase
    {
        private readonly IPreferenciasService _preferenciasService;

        public PreferenciasController(IPreferenciasService preferenciasService)
        {
            _preferenciasService = preferenciasService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Preferencias>>> GetAll()
        {
            var preferencias = await _preferenciasService.GetAllAsync();
            return Ok(preferencias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Preferencias>> GetById(int id)
        {
            var pref = await _preferenciasService.GetByIdAsync(id);
            if (pref == null)
                return NotFound();
            return Ok(pref);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Preferencias preferencias)
        {
            await _preferenciasService.AddAsync(preferencias);
            return CreatedAtAction(nameof(GetById), new { id = preferencias.IdPreferencia }, preferencias);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Preferencias preferencias)
        {
            if (id != preferencias.IdPreferencia)
                return BadRequest();

            var existente = await _preferenciasService.GetByIdAsync(id);
            if (existente == null)
                return NotFound();

            await _preferenciasService.UpdateAsync(preferencias);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminado = await _preferenciasService.DeleteAsync(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}

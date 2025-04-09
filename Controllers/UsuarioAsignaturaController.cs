using Microsoft.AspNetCore.Mvc;
using TFG_BACK.Models;
using TFG_BACK.Services;

namespace TFG_BACK.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioAsignaturaController : ControllerBase
    {
        private readonly IUsuarioAsignaturaService _usuarioAsignaturaService;

        public UsuarioAsignaturaController(IUsuarioAsignaturaService usuarioAsignaturaService)
        {
            _usuarioAsignaturaService = usuarioAsignaturaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioAsignatura>>> GetAll()
        {
            var relaciones = await _usuarioAsignaturaService.GetAllAsync();
            return Ok(relaciones);
        }

        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<Asignatura>>> GetAsignaturasByUsuarioId(int idUsuario)
        {
            var asignaturas = await _usuarioAsignaturaService.GetByUsuarioIdAsync(idUsuario);
            return Ok(asignaturas);
        }

        [HttpPost]
        public async Task<ActionResult> Create(UsuarioAsignatura relacion)
        {
            await _usuarioAsignaturaService.AddAsync(relacion);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] int idUsuario, [FromQuery] int idAsignatura)
        {
            var eliminado = await _usuarioAsignaturaService.DeleteAsync(idUsuario, idAsignatura);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}

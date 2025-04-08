using Microsoft.AspNetCore.Mvc;
using TFG_BACK.Models;
using TFG_BACK.Services;

namespace TFG_BACK.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Video>>> GetAll()
        {
            var videos = await _videoService.GetAllAsync();
            return Ok(videos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Video>> GetById(int id)
        {
            var video = await _videoService.GetByIdAsync(id);
            if (video == null)
                return NotFound();
            return Ok(video);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Video video)
        {
            await _videoService.AddAsync(video);
            return CreatedAtAction(nameof(GetById), new { id = video.IdVideo }, video);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Video video)
        {
            if (id != video.IdVideo)
                return BadRequest();

            var existente = await _videoService.GetByIdAsync(id);
            if (existente == null)
                return NotFound();

            await _videoService.UpdateAsync(video);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminado = await _videoService.DeleteAsync(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}

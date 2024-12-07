using InforceProject.Server.Models.UrlModel;
using InforceProject.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace InforceProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly UrlService _urlService;

        public UrlController(UrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Url>>> GetUrls()
        {
            try
            {
                var user = User.Identity?.Name;
                var urls = await _urlService.GetAllUrls(user);
                return Ok(urls);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Url>> CreateUrl([FromBody] string originalUrl)
        {
            var user = User.Identity?.Name;
            try
            {
                var url = await _urlService.CreateShortUrl(originalUrl, user);
                return CreatedAtAction(nameof(GetUrls), new { id = url.Id }, url);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUrl(int id)
        {
            var user = User.Identity?.Name;
            try
            {
                await _urlService.DeleteUrl(id, user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

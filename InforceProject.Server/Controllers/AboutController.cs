using InforceProject.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InforceProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly UrlContext _context;

        public AboutController(UrlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetAboutInfo()
        {
            var aboutInfo = await _context.AboutInfos.FirstOrDefaultAsync();
            if (aboutInfo == null)
            {
                return NotFound("About info not found.");
            }
            return Ok(aboutInfo.Text);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAboutInfo([FromBody] string newAboutText)
        {
            var aboutInfo = await _context.AboutInfos.FirstOrDefaultAsync();
            if (aboutInfo == null)
            {
                return NotFound("About info not found.");
            }

            aboutInfo.Text = newAboutText;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}

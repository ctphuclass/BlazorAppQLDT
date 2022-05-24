using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppQLDT.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FQAController : ControllerBase
    {
        private readonly DataContext _context;

        public FQAController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<FAQApp>>> GetFAQApps()
        {
            var faqapps = await _context.FAQApps.FromSqlRaw("SELECT * FROM FAQApp").ToListAsync();
            // var faqapps = await _context.FAQApps
            //     .ToListAsync();
            if (faqapps != null)
                return Ok(faqapps);
            else
            {
                return NotFound();
            }
        }
    }
}

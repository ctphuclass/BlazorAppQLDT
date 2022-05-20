
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppQLDT.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
        {
            var heroes = await _context.SuperHeroes
                .Include(h => h.Comic)
                .ToListAsync();
            return Ok(heroes);
        }
        [HttpGet("comic")]
        public async Task<ActionResult<List<Comic>>> GetComic()
        {
            var comic = await _context.Comics.ToListAsync();
            return Ok(comic);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSingleHero(int id)
        {
            var hero = await _context.SuperHeroes
                .Include(h => h.Comic)
                .FirstOrDefaultAsync(h => h.Id == id);
            if(hero == null)
            {
                return NotFound("Sorry no hero here");
            }
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<SuperHero>> CreateHero(SuperHero hero)
        {
            hero.Comic = null;
            await _context.SuperHeroes.AddAsync(hero);
            await _context.SaveChangesAsync();
            return Ok(hero);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<SuperHero>> UpdateHero(SuperHero hero, int id)
        {
            var oldhero = await _context.SuperHeroes
                .FirstOrDefaultAsync(h => h.Id == id);
            if (oldhero == null)
            {
                return NotFound("Sorry no hero here");
            }
            oldhero.Comic = null;
            oldhero.FirstName = hero.FirstName;
            oldhero.LastName = hero.LastName;
            oldhero.HeroName = hero.HeroName;
            oldhero.ComicId = hero.ComicId;
            await _context.SaveChangesAsync();
            return Ok(hero);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHero(int id)
        {
            var hero = await _context.SuperHeroes
                .FirstOrDefaultAsync(h => h.Id == id);
            if (hero == null)
            {
                return NotFound("Sorry no hero here");
            }
            _context.SuperHeroes.Remove(hero);
            _context.SaveChanges();
            return Ok();
        }
    }
}

using System;
using SuperheroAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SuperheroAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class SuperheroController : ControllerBase
	{
		private readonly DataContext _context;
		public SuperheroController(DataContext context)
        {
			_context = context;
        }


		[HttpGet]
		public async Task<ActionResult<List<Superhero>>> Get()
		{

			return Ok(await _context.Superheroes.ToListAsync());

		}


		[HttpGet("{id}")]
		public async Task<ActionResult<Superhero>> Get(int id)
            {
				var hero = await _context.Superheroes.FindAsync(id);
				if (hero == null)
					return BadRequest("Hero not found lmao");

				return Ok(hero);

				
            }



		[HttpPost]
		public async Task<ActionResult<List<Superhero>>> AddHero(Superhero hero)
        {
			_context.Superheroes.Add(hero);
			await _context.SaveChangesAsync();
			return Ok(await _context.Superheroes.ToListAsync());



        }

		[HttpPut]
		public async Task<ActionResult<List<Superhero>>> UpdateHero(Superhero request)
        {

			var hero = await _context.Superheroes.FindAsync(request.Id);
			if (hero == null)
				return BadRequest("hero not here lor scrub");
			hero.Name = request.Name;
			hero.FirstName = request.FirstName;
			hero.LastName = request.LastName;
			hero.Place = request.Place;

			await _context.SaveChangesAsync();

			return Ok(await _context.Superheroes.ToListAsync());

		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<List<Superhero>>> DeleteHero(int id)
        {

			var hero = await _context.Superheroes.FindAsync(id);
			if (hero == null)
				return BadRequest("Hero not found lmao noob");

			_context.Superheroes.Remove(hero);

			await _context.SaveChangesAsync();
			return Ok(await _context.Superheroes.ToListAsync());
		}
		
	}
}


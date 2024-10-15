using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using PokemonApi.Schema;

namespace PokemonApi.Controllers
{
    [Route("api/pokemon")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Pokemon> _pokemon;
        public PokemonController(IMongoDatabase database)
        {
            _database = database;
            _pokemon = _database.GetCollection<Pokemon>("Pokemon");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pokemon>>> GetAll()
        {
            var pokemon = await _pokemon.Find(_ => true).ToListAsync();
            return Ok(new { success = true, pokemon });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pokemon>> GetById(string id)
        {
            var pokemon = await _pokemon.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (pokemon == null)
            {
                return BadRequest(new { success = false, message = "El Pokemon no existe." });
            }

            return Ok(new { success = true, pokemon });
        }

        [HttpPost]
        public async Task<ActionResult<Pokemon>> Create([FromBody] Pokemon newPokemon)
        {
            await _pokemon.InsertOneAsync(newPokemon);
            return Ok(new { success = true, message = "Pokemon creado con exito." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Pokemon updatedPokemon)
        {
            var type = await _pokemon.Find(m => m.Id == id).FirstOrDefaultAsync();
            if (type == null)
            {
                return BadRequest(new { success = false, message = "El Pokemon no existe." });
            }

            updatedPokemon.Id = type.Id;
            await _pokemon.ReplaceOneAsync(m => m.Id == id, updatedPokemon);

            return Ok(new { success = true, message = "Pokemon actualizado con exito." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _pokemon.DeleteOneAsync(m => m.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return Ok(new { success = true, message = "Pokemon eliminado con exito." });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using PokemonApi.Schema;

namespace PokemonApi.Controllers
{
    public class AbilityController : Controller
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Abilities> _abilities;
        public AbilityController(IMongoDatabase database)
        {
            _database = database;
            _abilities = _database.GetCollection<Abilities>("Abilities");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Abilities>>> GetAll()
        {
            var classes = await _abilities.Find(_ => true).ToListAsync();
            return Ok(new { success = true, classes });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Abilities>> GetById(ObjectId id)
        {
            var classe = await _abilities.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (classe == null)
            {
                return BadRequest(new { success = false, message = "La Habilidad no existe." });
            }

            return Ok(new { success = true, classe });
        }

        [HttpPost]
        public async Task<ActionResult<Abilities>> Create([FromBody] Abilities newClasse)
        {
            await _abilities.InsertOneAsync(newClasse);
            return Ok(new { success = true, message = "Clase creada con exito." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ObjectId id, [FromBody] Abilities updatedClasse)
        {
            var classe = await _abilities.Find(m => m.Id == id).FirstOrDefaultAsync();
            if (classe == null)
            {
                return BadRequest(new { success = false, message = "La Clase no existe." });
            }

            updatedClasse.Id = classe.Id;
            await _abilities.ReplaceOneAsync(m => m.Id == id, updatedClasse);

            return Ok(new { success = true, message = "Clase actualizado con exito." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ObjectId id)
        {
            var result = await _abilities.DeleteOneAsync(m => m.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return Ok(new { success = true, message = "Clase eliminada con exito." });
        }
    }
}

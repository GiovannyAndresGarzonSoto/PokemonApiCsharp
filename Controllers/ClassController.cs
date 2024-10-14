using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using PokemonApi.Schema;

namespace PokemonApi.Controllers
{
    [Route("api/class")]
    [ApiController]
    public class ClassController : Controller
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Classes> _classes;
        public ClassController(IMongoDatabase database)
        {
            _database = database;
            _classes = _database.GetCollection<Classes>("Classes");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classes>>> GetAll()
        {
            var classes = await _classes.Find(_ => true).ToListAsync();
            return Ok(new { success = true, classes });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Classes>> GetById(ObjectId id)
        {
            var classe = await _classes.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (classe == null)
            {
                return BadRequest(new { success = false, message = "La Clase no existe." });
            }

            return Ok(new { success = true, classe });
        }

        [HttpPost]
        public async Task<ActionResult<Classes>> Create([FromBody] Classes newClasse)
        {
            await _classes.InsertOneAsync(newClasse);
            return Ok(new { success = true, message = "Clase creada con exito." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ObjectId id, [FromBody] Classes updatedClasse)
        {
            var classe = await _classes.Find(m => m.Id == id).FirstOrDefaultAsync();
            if (classe == null)
            {
                return BadRequest(new { success = false, message = "La Clase no existe." });
            }

            updatedClasse.Id = classe.Id;
            await _classes.ReplaceOneAsync(m => m.Id == id, updatedClasse);

            return Ok(new { success = true, message = "Clase actualizado con exito." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ObjectId id)
        {
            var result = await _classes.DeleteOneAsync(m => m.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return Ok(new { success = true, message = "Clase eliminada con exito." });
        }
    }
}

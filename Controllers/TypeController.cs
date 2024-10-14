using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using PokemonApi.Schema;


namespace PokemonApi.Controllers
{
    [Route("api/type")]
    [ApiController]
    public class TypeController : Controller
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Types> _types;
        public TypeController(IMongoDatabase database)
        {
            _database = database;
            _types = _database.GetCollection<Types>("Types");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Types>>> GetAll()
        {
            var types = await _types.Find(_ => true).ToListAsync();
            return Ok(new{ success = true,  types });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Types>> GetById(ObjectId id)
        {
            var type = await _types.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (type == null)
            {
                return BadRequest(new { success = false, message = "El Tipo no existe." });
            }

            return Ok(new{ success = true, type });
        }

        [HttpPost]
        public async Task<ActionResult<Types>> Create([FromBody] Types newType)
        {
            await _types.InsertOneAsync(newType);
            return Ok(new { success = true, message = "Tipo creado con exito." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ObjectId id, [FromBody] Types updatedType)
        {
            var type = await _types.Find(m => m.Id == id).FirstOrDefaultAsync();
            if (type == null)
            {
                return BadRequest(new { success = false, message = "El Tipo no existe." });
            }

            updatedType.Id = type.Id;
            await _types.ReplaceOneAsync(m => m.Id == id, updatedType);

            return Ok(new { success = true, message = "Tipo actualizado con exito." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ObjectId id)
        {
            var result = await _types.DeleteOneAsync(m => m.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return Ok(new { success = true, message = "Tipo eliminado con exito." });
        }
    }
}

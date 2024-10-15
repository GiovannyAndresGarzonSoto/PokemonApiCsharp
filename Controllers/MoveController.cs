using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using PokemonApi.Schema;

namespace PokemonApi.Controllers
{
    [Route("api/move")]
    [ApiController]
    public class MoveController : Controller
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Moves> _moves;

        public MoveController(IMongoDatabase database)
        {
            _database = database;
            _moves = _database.GetCollection<Moves>("Moves");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moves>>> GetAll()
        {
            var moves = await _moves.Find(_ => true).ToListAsync();
            return Ok(new { success = true, moves });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Moves>> GetById(string id)
        {
            var move = await _moves.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (move == null)
            {
                return BadRequest(new { success = false, message = "El Movimiento no existe." });
            }

            return Ok(new{ success = true, move });
        }

        [HttpPost]
        public async Task<ActionResult<Moves>> Create([FromBody] Moves newMove)
        {
            await _moves.InsertOneAsync(newMove);
            return Ok(new {success = true, message = "Movimiento creado con exito."});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Moves updatedMove)
        {
            var move = await _moves.Find(m => m.Id == id).FirstOrDefaultAsync();
            if (move == null)
            {
                return BadRequest(new { success = false, message = "El Movimiento no existe." });
            }

            updatedMove.Id = move.Id; 
            await _moves.ReplaceOneAsync(m => m.Id == id, updatedMove);

            return Ok(new { success = true, message = "Movimiento actualizado con exito." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _moves.DeleteOneAsync(m => m.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return Ok(new { success = true, message = "Movimiento eliminado con exito." });
        }
    }
}

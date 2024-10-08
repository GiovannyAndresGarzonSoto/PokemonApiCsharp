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
        private readonly IMongoCollection<Move> _moves;

        public MoveController(IMongoDatabase database)
        {
            _database = database;
            _moves = _database.GetCollection<Move>("Moves");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Move>>> GetAll()
        {
            var moves = await _moves.Find(_ => true).ToListAsync();
            return Ok(moves);
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<Move>> GetById(ObjectId id)
        {
            var move = await _moves.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (move == null)
            {
                return BadRequest(new { success = false, message = "El Movimiento no existe." });
            }

            return Ok(move);
        }

        [HttpPost]
        public async Task<ActionResult<Move>> Create([FromBody] Move newMove)
        {
            await _moves.InsertOneAsync(newMove);
            return CreatedAtRoute("GetById", new { id = newMove.Id }, newMove);
        }
    }
}

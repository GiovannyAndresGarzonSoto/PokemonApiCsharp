using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using PokemonApi.Schema;
using System;

namespace PokemonApi.Controllers
{
    [Route("api/group")]
    [ApiController]
    public class GroupController : Controller
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Groups> _groups;
        public GroupController(IMongoDatabase database)
        {
            _database = database;
            _groups = _database.GetCollection<Groups>("Groups");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Groups>>> GetAll()
        {
            var groups = await _groups.Find(_ => true).ToListAsync();
            return Ok(new { success = true, groups });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Groups>> GetById(string id)
        {
            var group = await _groups.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (group == null)
            {
                return BadRequest(new { success = false, message = "El Grupo no existe." });
            }

            return Ok(new { success = true, group });
        }

        [HttpPost]
        public async Task<ActionResult<Groups>> Create([FromBody] Groups newGroup)
        {
            await _groups.InsertOneAsync(newGroup);
            return Ok(new { success = true, message = "Grupo creado con exito." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Groups updatedGroup)
        {
            var group = await _groups.Find(m => m.Id == id).FirstOrDefaultAsync();
            if (group == null)
            {
                return BadRequest(new { success = false, message = "El Grupo no existe." });
            }

            updatedGroup.Id = group.Id;
            await _groups.ReplaceOneAsync(m => m.Id == id, updatedGroup);

            return Ok(new { success = true, message = "Tipo actualizado con exito." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _groups.DeleteOneAsync(m => m.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return Ok(new { success = true, message = "Grupo eliminado con exito." });
        }
    }
}

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
        private readonly Cloudinary _cloudinary;
        public ClassController(IMongoDatabase database, Cloudinary cloudinary)
        {
            _database = database;
            _classes = _database.GetCollection<Classes>("Classes");
            _cloudinary = cloudinary;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classes>>> GetAll()
        {
            var classes = await _classes.Find(_ => true).ToListAsync();
            return Ok(new { success = true, classes });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Classes>> GetById(string id)
        {
            var classe = await _classes.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (classe == null)
            {
                return BadRequest(new { success = false, message = "La Clase no existe." });
            }

            return Ok(new { success = true, classe });
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<Classes>> Create([FromForm] IFormFile image, [FromForm] string name)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(image.FileName, image.OpenReadStream()),
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = true
            };

            var uploadResult = _cloudinary.Upload(uploadParams);

            var newClasse = new Classes
            {
                Name = name,
                ImageUrl = uploadResult.SecureUrl.ToString(),
                PublicId = uploadResult.PublicId
            };

            await _classes.InsertOneAsync(newClasse);
            return Ok(new { success = true, message = "Clase creada con éxito.", newClasse });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Classes updatedClasse)
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
        public async Task<IActionResult> Delete(string id)
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

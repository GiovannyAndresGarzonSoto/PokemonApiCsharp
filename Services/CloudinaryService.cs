namespace PokemonApi.Services
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.Extensions.Configuration;
    using System.IO;
    using System.Threading.Tasks;

    namespace PokemonApi.Services
    {
        public class CloudinaryService
        {
            private readonly Cloudinary _cloudinary;

            public CloudinaryService(IConfiguration configuration)
            {
                var account = new Account(
                    configuration["CLOUDINARY_CLOUD_NAME"],
                    configuration["CLOUDINARY_API_KEY"],
                    configuration["CLOUDINARY_API_SECRET"]
                );

                _cloudinary = new Cloudinary(account);
            }

            public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
            {
                var uploadResult = new ImageUploadResult();

                if (file.Length > 0)
                {
                    using var stream = file.OpenReadStream();
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        UseFilename = true,
                        UniqueFilename = true,
                        Overwrite = true
                    };

                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }

                return uploadResult;
            }

            public async Task<DeletionResult> DeleteImageAsync(string publicId)
            {
                var deleteParams = new DeletionParams(publicId);
                var result = await _cloudinary.DestroyAsync(deleteParams);
                return result;
            }
        }
    }

}

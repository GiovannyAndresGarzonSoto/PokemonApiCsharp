using MongoDB.Driver;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using PokemonApi.Services.PokemonApi.Services;

namespace PokemonApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var mongoDbSettings = builder.Configuration.GetSection("MongoDB");
            var connectionString = mongoDbSettings["ConnectionString"];
            var databaseName = mongoDbSettings["DatabaseName"];

            builder.Services.AddScoped<CloudinaryService>();

            builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionString));

            builder.Services.AddScoped<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(databaseName);
            });


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAllOrigins");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}

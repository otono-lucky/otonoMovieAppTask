using otonoMovieAppTask.Services;

namespace OtonoMovieTask.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://otono-movieapp-task-fe.vercel.app")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .WithExposedHeaders("Authorization"); // This adds the custom authorization header to response
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

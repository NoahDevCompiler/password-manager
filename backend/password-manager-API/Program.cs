using password_manager_API;
using password_manager_API.Helpers;
using System.Runtime.CompilerServices;

internal class program
{
    public static DBManager DB;
    private static void Main(string[] args) {

        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors(options => {
            options.AddPolicy("AllowMyClient", policy => {
                policy.WithOrigins("http://localhost:5173")
                      .AllowAnyMethod()
                      .AllowAnyHeader()                   
                      .AllowCredentials();                
            });
        });


        builder.Services.AddControllers();
        builder.Services.AddSingleton<TokenConfig>();
        builder.Services.AddTransient<TwoFactorAuth>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowMyClient");
        app.UseAuthorization();

        app.MapControllers();

        DB = new DBManager();

        app.Run();

        DB.Close();
    }
}


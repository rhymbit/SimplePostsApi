using Microsoft.EntityFrameworkCore;
using PostsApi.Services;
using PostsApi.Services.Worker;
using PostsApiDatabase;

//const string ConnectionString = "User ID=postgres;Password=postsapitestpassword;Host=34.131.51.218;Database=allposts";
const string ConnectionString = "User ID=postgres;Password=postgres;Host=127.0.0.1;Database=allposts";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<PostsDbContext>(options =>
{
    options.UseNpgsql(ConnectionString);
});

builder.Services.AddSingleton<IPostsAddingService, PostsAddingService>();

builder.Services.AddHostedService<DbSaveWorker>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Basic_Games_Shelf.WebApi.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BasicGamesShelfContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BasicGamesShelfContext") ?? throw new InvalidOperationException("Connection string 'BasicGamesShelfContext' not found.")));

// Add services to the container.

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

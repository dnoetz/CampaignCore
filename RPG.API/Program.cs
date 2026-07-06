using DotNetEnv.Configuration;
using Microsoft.EntityFrameworkCore;
using RPG.Core.Entities;
using RPG.Core.Interfaces.Repositories;
using RPG.Infrastructure.Data;
using RPG.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddDotNetEnv(".env");
// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICampaignActionRepository, CampaignActionRepository>();
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using DotNetEnv.Configuration;
using Microsoft.EntityFrameworkCore;
using RPG.Core.Entities;
using RPG.Core.Entities.Characters.Necromancer;
using RPG.Core.Enums;
using RPG.Core.Interfaces;
using RPG.Core.Interfaces.Repositories;
using RPG.Core.Services;
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
builder.Services.AddScoped<IAbilityProvider, AbilityProvider>();
builder.Services.AddScoped<ActionLoggerService>();
builder.Services.AddScoped<CampaignCodeService>();
builder.Services.AddScoped<CampaignService>();
builder.Services.AddScoped<CharacterService>();
builder.Services.AddScoped<CombatService>();
builder.Services.AddScoped<DamageCalculatorService>();
builder.Services.AddScoped<DiceRollerService>();
builder.Services.AddScoped<ExperienceService>();

builder.Services.AddKeyedScoped<ICombatAbility, AbilityNecrosis>(PlayableClasses.Necromancer);
builder.Services.AddKeyedScoped<ICombatAbility, AbilityReapersMark>(PlayableClasses.Necromancer);

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

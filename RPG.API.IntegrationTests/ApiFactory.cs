using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Respawn;
using RPG.Infrastructure.Data;
using Testcontainers.PostgreSql;

namespace RPG.API.IntegrationTests;

public class ApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _db = new PostgreSqlBuilder("postgres:16")
        .Build();
    private DbConnection _connection = null!;
    private Respawner _respawner = null!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config => 
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:SigningKey"] = "integration-test-signing-key-is-at-a-minimum-32-bytes!",
                ["Jwt:Issuer"] = "test-issuer",
                ["Jwt:Audience"] = "test-audience"
            }));
        
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<DbContextOptions<AppDbContext>>();
            services.AddDbContext<AppDbContext>(o => o.UseNpgsql(_db.GetConnectionString()));
            builder.UseEnvironment("Development");
        });
    }

    public async Task InitializeAsync()
    {
        await _db.StartAsync();

        using (var scope = Services.CreateScope())
        {
            var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await ctx.Database.MigrateAsync();
        }

        _connection = new NpgsqlConnection(_db.GetConnectionString());
        await _connection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = new[] { "public" },
            TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
        });
    }
    
    public async Task ResetDatabaseAsync() => await _respawner.ResetAsync(_connection);

    public new async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _db.DisposeAsync();
    }
}
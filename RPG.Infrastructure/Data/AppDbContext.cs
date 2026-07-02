using Microsoft.EntityFrameworkCore;
using RPG.Core.Entities;
using RPG.Core.Entities.Campaigns;
using RPG.Core.Entities.Characters;


namespace RPG.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Character> Characters { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<CampaignAction> CampaignActions { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campaign>()
            .HasOne(c => c.Owner)
            .WithMany(u => u.OwnedCampaigns)
            .HasForeignKey("OwnerId")
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Character>()
            .HasOne(c => c.Campaign)
            .WithMany(c => c.Characters)
            .HasForeignKey(c => c.CampaignId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CampaignAction>()
            .HasOne(c => c.Campaign)
            .WithMany(c => c.CampaignActions)
            .HasForeignKey(ca => ca.CampaignId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Character>()
            .HasOne(c => c.Player)
            .WithMany()
            .HasForeignKey("PlayerId")
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CampaignAction>()
            .HasOne(ca => ca.Actor)
            .WithMany()
            .HasForeignKey("ActorId")
            .OnDelete(DeleteBehavior.Restrict);
        
        base.OnModelCreating(modelBuilder);
    }
}
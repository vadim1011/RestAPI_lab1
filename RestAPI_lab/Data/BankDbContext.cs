using Microsoft.EntityFrameworkCore;
using BankDepositsApi.Models;

namespace BankDepositsApi.Data;

public class BankDbContext : DbContext
{
    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }

    public DbSet<Deposit> Deposits { get; set; } = null!;
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>()
            .HasOne(a => a.Client)
            .WithMany(c => c.Accounts)
            .HasForeignKey(a => a.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Account>()
            .HasOne(a => a.Deposit)
            .WithMany(d => d.Accounts)
            .HasForeignKey(a => a.DepositId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

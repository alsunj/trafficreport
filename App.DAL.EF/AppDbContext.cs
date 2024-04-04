
using App.Domain.Evidences;
using App.Domain.Identity;
using App.Domain.Vehicles;
using App.Domain.Violations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace App.DAL.EF;

public class AppDbContext :IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>,
    AppUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public DbSet<ViolationType> ViolationTypes { get; set; } = default!;
    
    public DbSet<Violation> Violations { get; set; } = default!;
    
    public DbSet<VehicleType> VehicleTypes { get; set; } = default!;
    
    public DbSet<Vehicle> Vehicles { get; set; } = default!;
    
    public DbSet<AdditionalVehicle> AdditionalVehicles { get; set; } = default!;

    public DbSet<EvidenceType> EvidenceTypes { get; set; } = default!;

    public DbSet<Evidence> Evidences { get; set; } = default!;
    
    public DbSet<Comment> Comments { get; set; } = default!;

    public DbSet<VehicleViolation> VehicleViolations { get; set; } = default!;
    

    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<VehicleType>()
            .ToTable("EVehicleSize");

    }
}
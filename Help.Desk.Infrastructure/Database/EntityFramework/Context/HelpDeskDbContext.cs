using Help.Desk.Infrastructure.Database.EntityFramework.Entities;
using Help.Desk.Infrastructure.Database.EntityFramework.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Help.Desk.Infrastructure.Database.EntityFramework.Context;

public class HelpDeskDbContext: DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<DepartmentEntity> Departments { get; set; }
    
    public HelpDeskDbContext(DbContextOptions<HelpDeskDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
    private void UpdateAuditFields()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = GetCurrentUserId();
                    entry.Entity.LastModifiedByAt = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy= GetCurrentUserId();
                    break;

                case EntityState.Modified:
                    entry.Property(nameof(BaseEntity.CreatedAt)).IsModified = false;
                    entry.Property(nameof(BaseEntity.CreatedBy)).IsModified = false;
                    entry.Entity.LastModifiedByAt = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = 104;
                    break;
            }
        }
    }
    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    private int GetCurrentUserId()
    {
        return 123; // This should be replaced with the actual logic to get the current user ID
    }
}
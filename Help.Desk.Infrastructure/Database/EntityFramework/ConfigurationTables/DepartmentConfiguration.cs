using Help.Desk.Infrastructure.Database.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Help.Desk.Infrastructure.Database.EntityFramework.ConfigurationTables;

public class DepartmentConfiguration: IEntityTypeConfiguration<DepartmentEntity>
{
    public void Configure(EntityTypeBuilder<DepartmentEntity> builder)
    {
        builder.ToTable("Departments");
        builder.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd().UseIdentityColumn();
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasColumnName("Nombre").IsRequired().HasMaxLength(50).IsRequired();
        builder.Property(e => e.Description).HasColumnName("Descripcion").IsRequired().HasMaxLength(200).IsRequired();
        
        builder.HasMany(e => e.Users)
            .WithOne()
            .HasForeignKey(e => e.DepartmentId)
            .HasPrincipalKey(d => d.Id)
            .HasConstraintName("FK_Users_Departments");
        
    }
}
using Help.Desk.Infrastructure.Database.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Help.Desk.Infrastructure.Database.EntityFramework.ConfigurationTables;

public class UserConfiguration: IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");
        builder.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd().UseIdentityColumn();
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasColumnName("Nombre").IsRequired().HasMaxLength(50).IsRequired();
        builder.Property(e => e.LastName).HasColumnName("Apellido").IsRequired().HasMaxLength(50).IsRequired();
        builder.Property(e=> e.PhoneNumber).HasColumnName("Telefono").IsRequired().HasMaxLength(10).IsRequired();
        builder.Property(e => e.Email).HasColumnName("Email").IsRequired().HasMaxLength(50).IsRequired();
        builder.Property(e => e.Password).HasColumnName("Contraseña").IsRequired().HasMaxLength(50).IsRequired();
        
        builder.Property(e => e.DepartmentId).HasColumnName("DepartmentId").IsRequired();
        builder.Property(e => e.Role).HasColumnName("Rol").IsRequired().HasMaxLength(20).IsRequired();
        builder.Property(e => e.Active).HasColumnName("Activo").IsRequired();
        builder.Property(e => e.IsAgent).HasColumnName("EsAgente").IsRequired();
        
        builder.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(e => e.DepartmentId)
            .HasPrincipalKey(d => d.Id)
            .OnDelete(DeleteBehavior.SetNull);
        
    }
}
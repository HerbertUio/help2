using Help.Desk.Infrastructure.Database.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Help.Desk.Infrastructure.Database.EntityFramework.ConfigurationTables;

public class TicketConfiguration: IEntityTypeConfiguration<TicketEntity>
{
    public void Configure(EntityTypeBuilder<TicketEntity> builder)
    {
        builder.ToTable("Ticket");
        builder.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd().UseIdentityColumn();
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Title).HasColumnName("Title").IsRequired().HasMaxLength(50).IsRequired();
        builder.Property(e => e.Description).HasColumnName("Description").IsRequired().HasMaxLength(1000);
        builder.Property(t => t.Priority)
            .HasColumnName("Priority")
            .IsRequired();
        builder.Property(t => t.Status)
            .HasColumnName("Status")
            .IsRequired();
        builder.Property(t => t.Created)
            .HasColumnName("Created")
            .IsRequired();
        builder.Property(t => t.ExpirationDateSla)
            .HasColumnName("ExpirationDateSLA")
            .IsRequired(false);
            
        builder.Property(t => t.ClosedDate)
            .HasColumnName("ClosedDate")
            .IsRequired(false);
        
        builder.Property("LastUpdated")
            .HasColumnName("LastUpdated")
            .IsRequired(false);
        
        builder.HasOne(t => t.PrimaryTicket)
            .WithMany()
            .HasForeignKey(t => t.PrimaryTicketId)
            .HasConstraintName("FK_Tickets_PrimaryTicket")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(t => t.ParentTicket)
            .WithMany().HasForeignKey(t => t.ParentTicketId)
            .HasConstraintName("FK_Tickets_ParentTicket")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.Property(t => t.RequesterId)
            .HasColumnName("RequesterId")
            .IsRequired(false);
        builder.Property(t => t.AssignedAgentId)
            .HasColumnName("AssignedAgentId")
            .IsRequired(false);
        builder.Property(t => t.AssignedGroupId)
            .HasColumnName("AssignedGroupId")
            .IsRequired(false);
        builder.Property(t => t.TypeTicketId)
            .HasColumnName("TypeTicketId")
            .IsRequired(false);
        builder.Property(t => t.SourceOriginId)
            .HasColumnName("SourceOriginId")
            .IsRequired(false);
        builder.Property(t => t.OfficeId)
            .HasColumnName("OfficeId")
            .IsRequired(false);
        builder.Property(t => t.AreaId)
            .HasColumnName("AreaId")
            .IsRequired(false);
        builder.Property(t => t.SubjectId)
            .HasColumnName("SubjectId")
            .IsRequired(false);
    }
}
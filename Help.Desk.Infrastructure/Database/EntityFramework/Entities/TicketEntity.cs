namespace Help.Desk.Infrastructure.Database.EntityFramework.Entities;

public class TicketEntity
{
    public int Id { get; set; }
    public string Title { get; set; } 
    public string Description { get; set; }
    public int Priority { get; set; }
    public int Status { get; set; } 
    public DateTime Created { get; set; }
    public DateTime?  ExpirationDateSla { get; set; }
    public DateTime? ResolutionDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public DateTime LastUpdate { get; set; }

    public int? PrimaryTicketId { get; set; }
    public int? ParentTicketId { get; set; }
    public int? RequesterId { get; set; } 
    public int? AssignedAgentId { get; set; } 
    public int? AssignedGroupId { get; set; } 
    public int? TypeTicketId { get; set; }
    public int? SourceOriginId { get; set; } 
    public int? OfficeId { get; set; }
    public int? AreaId { get; set; }
    public int? SubjectId { get; set; }
    public virtual TicketEntity? PrimaryTicket { get; set; } // Navegación al ticket primario
    public virtual TicketEntity? ParentTicket { get; set; } 
}
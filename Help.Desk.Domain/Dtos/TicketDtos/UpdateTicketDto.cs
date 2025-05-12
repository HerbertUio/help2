namespace Help.Desk.Domain.Dtos.TicketDtos;

public class UpdateTicketDto
{
    public string VisibleTicketNumber { get; set; } // Ejemplo: "BOA-12345"
    public string Tittle { get; set; } // Asunto del ticket
    public string Description { get; set; }
    public string? DescriptionHtml { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? ExpirationDateSla { get; set; }
    public DateTime? ResolutionDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public DateTime? LastUpdate { get; set; }

    public int? PrimaryTicketId { get; set; }
    public int? ParentTicketId { get; set; }
    public int? RequesterId { get; set; } //Solicitante
    public int? AssignedAgentId { get; set; } //Agente asignado
    public int? AssignedGroupId { get; set; } //Grupo asignado
    public int? PriorityId { get; set; }
    public int? StatusId { get; set; }
    public int? TypeTicketId { get; set; }
    public int? SourceOriginId { get; set; } //Origen del ticket
    public int? OfficeId { get; set; }
    public int? SubjectId { get; set; } //Asunto Relacionado con:
}
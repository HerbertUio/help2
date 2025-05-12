namespace Help.Desk.Domain.Dtos.TicketDtos;

public class ResumenForAgentTicketDto
{
    public string Tittle { get; set; }
    public DateTime? Created { get; set; }
    public int? RequesterId { get; set; } //Solicitante
    public int? AssignedAgentId { get; set; }
    public int? PriorityId { get; set; }
    public int? StatusId { get; set; }
}
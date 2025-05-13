namespace Help.Desk.Domain.Dtos.TicketDtos;

public class RegisterTicketDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int? SubjectId { get; set; } // Asunto relacionado con:
    public int? OfficeId { get; set; } // Oficina
    public int? TypeTicketId { get; set; }
    public int? RequesterId { get; set; }
}
    
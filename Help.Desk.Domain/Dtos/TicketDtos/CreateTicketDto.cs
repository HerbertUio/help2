namespace Help.Desk.Domain.Dtos.TicketDtos;

public class CreateTicketDto
{
    public int  RequesterId { get; set; } // Solicitado por
    public string Title { get; set; }
    public string Description { get; set; }
    public int? SubjectId { get; set; } // Asunto Relacionado con:
    public int? OfficeId { get; set; }
    public int? AreaId { get; set; }
    public int? TypeTicketId { get; set; }
    //public string PhoneNumber  { get; set; }
}
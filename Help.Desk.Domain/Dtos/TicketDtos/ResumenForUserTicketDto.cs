namespace Help.Desk.Domain.Dtos.TicketDtos;

public class ResumenForUserTicketDto
{
    public string Title { get; set; }
    public DateTime? Created { get; set; }
    public int? StatusId { get; set; }
}
namespace Help.Desk.Domain.Dtos.TicketDtos;

public class UnmergeTicketDto
{
    public int TicketId { get; set; }
    public int TicketToUnmergeId { get; set; }
}
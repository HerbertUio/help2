namespace Help.Desk.Domain.Models;

public class TicketComment
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int AutorId { get; set; }
    public bool IsPrivate { get; set; }
}
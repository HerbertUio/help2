namespace Help.Desk.Domain.Models;

public class TicketAttachment
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; } // Ruta donde se guarda el archivo en el servidor
    public string ContentType { get; set; } // ej. "image/png" o "application/pdf"
    public long FileSize { get; set; }
    public DateTime UploadedAt { get; set; }
    public int UploaderId { get; set; }
}
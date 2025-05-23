using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.TicketCases;

public class CloseTicketCase
{
    private readonly ITicketRepository _ticketRepository;
    
    public CloseTicketCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }
    public async Task<Result<bool>> ExecuteAsync(int ticketId)
    {
        var existingTicket = await _ticketRepository.GetByIdAsync(ticketId);
        if (existingTicket == null)
        {
            return Result<bool>.Failure(
                new List<string> { "No se encontró el ticket." },
                "Error al cerrar ticket."
            );
        }
        existingTicket.Close();
        var success = await _ticketRepository.CloseTicketAsync(ticketId);

        if (!success)
        {
            return Result<bool>.Failure(
                new List<string> { "No se pudo cerrar el ticket. El ticket ya podría estar cerrado o resuelto." },
                "Error al cerrar ticket."
            );
        }

        return Result<bool>.Success(true, "Ticket cerrado exitosamente.");
    }
}
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.TicketCases;

public class ReopenTicketCase
{
    private readonly ITicketRepository _ticketRepository;
    public ReopenTicketCase(ITicketRepository ticketRepository)
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
                "Error al reabrir ticket."
            );
        }
        
        var success = await _ticketRepository.ReopenTicketAsync(ticketId);

        if (!success)
        {
            return Result<bool>.Failure(
                new List<string> { "No se pudo reabrir el ticket. El ticket no está en un estado que permita la reapertura (Cerrado o Resuelto)." },
                "Error al reabrir ticket."
            );
        }

        return Result<bool>.Success(true, "Ticket reabierto exitosamente.");
    }
}
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.TicketCases;

public class DeleteTicketCase
{
    private readonly ITicketRepository _ticketRepository;
    
    public DeleteTicketCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<Result<bool>> ExecuteAsync(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);
        if (ticket == null)
        {
            return Result<bool>.Failure(
                new List<string> { "No se encontró el ticket." },
                "Error al obtener el ticket."
            );
        }
        var result = await _ticketRepository.DeleteAsync(id);
        if (!result)
        {
            return Result<bool>.Failure(
                new List<string> { "Error al eliminar el ticket." },
                "Error al eliminar ticket."
            );
        }
        return Result<bool>.Success(true, "Ticket eliminado exitosamente.");
    }
}
using Help.Desk.Domain.Dtos.TicketDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.TicketCases;

public class GetByIdTicketCase
{
    private readonly ITicketRepository _ticketRepository;
    
    public GetByIdTicketCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<Result<TicketDto>> ExecuteAsync(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);
        if (ticket == null)
        {
            return Result<TicketDto>.Failure(
                new List<string> { "No se encontró el ticket." },
                "Error al obtener el ticket."
            );
        }
        return Result<TicketDto>.Success(ticket, "Ticket obtenido exitosamente.");
    }
}
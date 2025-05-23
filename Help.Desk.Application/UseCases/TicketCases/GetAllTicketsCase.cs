using Help.Desk.Domain.Dtos.TicketDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Models;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.TicketCases;

public class GetAllTicketsCase
{
    private readonly ITicketRepository _ticketRepository;
    public GetAllTicketsCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }
    public async Task<Result<List<TicketModel>>> ExecuteAsync()
    {
        var tickets = await _ticketRepository.GetAllAsync();
        if (tickets == null || !tickets.Any())
        {
            return Result<List<TicketModel>>.Failure(
                new List<string> { "No se encontraron tickets." },
                "Error al obtener tickets."
            );
        }
        return Result<List<TicketModel>>.Success(tickets, "Tickets obtenidos exitosamente.");
    }
}
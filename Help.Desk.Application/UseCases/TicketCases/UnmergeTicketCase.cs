using FluentValidation;
using Help.Desk.Domain.Dtos.TicketDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.TicketCases;

public class UnmergeTicketCase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IValidator<UnmergeTicketDto> _unmergeTicketValidator;
    
    public UnmergeTicketCase(ITicketRepository ticketRepository, IValidator<UnmergeTicketDto> unmergeTicketValidator)
    {
        _ticketRepository = ticketRepository;
        _unmergeTicketValidator = unmergeTicketValidator;
    }
    public async Task<Result<TicketDto>> ExecuteAsync(UnmergeTicketDto dto)
    {
        var validationResult = await _unmergeTicketValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return Result<TicketDto>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                "Error de validación al desfusionar tickets."
            );
        }

        // Obtener ambos tickets
        var mainTicket = await _ticketRepository.GetByIdAsync(dto.TicketId);
        var ticketToUnmerge = await _ticketRepository.GetByIdAsync(dto.TicketToUnmergeId);

        if (mainTicket == null)
        {
            return Result<TicketDto>.Failure(
                new List<string> { $"El ticket principal con ID {dto.TicketId} no fue encontrado." },
                "Error al desfusionar tickets."
            );
        }

        if (ticketToUnmerge == null)
        {
            return Result<TicketDto>.Failure(
                new List<string> { $"El ticket a desfusionar con ID {dto.TicketToUnmergeId} no fue encontrado." },
                "Error al desfusionar tickets."
            );
        }

        // Delegar la lógica de des-fusión al repositorio
        var resultTicket = await _ticketRepository.UnmergeTicketsAsync(dto.TicketId, dto.TicketToUnmergeId);

        if (resultTicket == null)
        {
            return Result<TicketDto>.Failure(
                new List<string> { "No se pudo completar la operación de des-fusión. Asegúrese de que los tickets estén realmente vinculados de esta manera." },
                "Error al desfusionar tickets."
            );
        }

        return Result<TicketDto>.Success(resultTicket, "Tickets desfusionados exitosamente.");
    }
}
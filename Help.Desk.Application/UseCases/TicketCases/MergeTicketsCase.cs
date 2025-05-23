using FluentValidation;
using Help.Desk.Domain.Dtos.TicketDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Models;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.TicketCases;

public class MergeTicketsCase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IValidator<MergeTicketsDto> _mergeTicketsValidator;
    
    public MergeTicketsCase(ITicketRepository ticketRepository, IValidator<MergeTicketsDto> mergeTicketsValidator)
    {
        _ticketRepository = ticketRepository;
        _mergeTicketsValidator = mergeTicketsValidator;
    }
    public async Task<Result<TicketModel>> ExecuteAsync(MergeTicketsDto dto)
    {
        var validationResult = await _mergeTicketsValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return Result<TicketModel>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                "Error de validación al fusionar tickets."
            );
        }

        // Obtener ambos tickets
        var primaryTicket = await _ticketRepository.GetByIdAsync(dto.PrimaryTicketId);
        var ticketToMerge = await _ticketRepository.GetByIdAsync(dto.TicketToMergeId);

        if (primaryTicket == null)
        {
            return Result<TicketModel>.Failure(
                new List<string> { $"El ticket principal con ID {dto.PrimaryTicketId} no fue encontrado." },
                "Error al fusionar tickets."
            );
        }

        if (ticketToMerge == null)
        {
            return Result<TicketModel>.Failure(
                new List<string> { $"El ticket a fusionar con ID {dto.TicketToMergeId} no fue encontrado." },
                "Error al fusionar tickets."
            );
        }

        // Delegar la lógica de fusión al repositorio, que a su vez usa el DTO
        var resultTicket = await _ticketRepository.MergeTicketsAsync(dto.PrimaryTicketId, dto.TicketToMergeId);

        if (resultTicket == null)
        {
            return Result<TicketModel>.Failure(
                new List<string> { "No se pudo completar la operación de fusión." },
                "Error al fusionar tickets."
            );
        }

        return Result<TicketModel>.Success(resultTicket, "Tickets fusionados exitosamente.");
    }
}
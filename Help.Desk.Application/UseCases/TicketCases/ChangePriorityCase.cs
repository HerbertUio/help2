using FluentValidation;
using Help.Desk.Application.Validators.TicketValidators;
using Help.Desk.Domain.Dtos.TicketDtos;
using Help.Desk.Domain.Enums.TicketEnums;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Models;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.TicketCases;

public class ChangePriorityCase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IValidator<ChangePriorityDto> _validator;
    public ChangePriorityCase(ITicketRepository ticketRepository, IValidator<ChangePriorityDto> validator)
    {
        _ticketRepository = ticketRepository;
        _validator = validator;
    }

    public async Task<Result<TicketModel>> ExecuteAsync(int ticketId, ChangePriorityDto changePriorityDto)
    {
        var validationResult = await _validator.ValidateAsync(changePriorityDto);
        if (!validationResult.IsValid)
        {
            return Result<TicketModel>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                "Error de validación al cambiar prioridad del ticket."
            );
        }
        var existingTicket = await _ticketRepository.GetByIdAsync(ticketId);
        if (existingTicket == null)
        {
            return Result<TicketModel>.Failure(
                new List<string> { "El ticket no existe." },
                "Error al cambiar prioridad del ticket."
            );
        }
        if (!Enum.IsDefined(typeof(Priority), changePriorityDto.NewPriorityId))
        {
            return Result<TicketModel>.Failure(
                new List<string> { $"El ID de prioridad '{changePriorityDto.NewPriorityId}' no es válido." },
                "Error al cambiar la prioridad del ticket."
            );
        }
        var newPriority = (Priority)changePriorityDto.NewPriorityId;
        existingTicket.ChangePriority(newPriority);
        var updatedTicket = await _ticketRepository.UpdateAsync(existingTicket);
        if (updatedTicket == null)
        {
            return Result<TicketModel>.Failure(
                new List<string> { "Error al cambiar la prioridad del ticket." },
                "Error al cambiar prioridad del ticket."
            );
        }
        return Result<TicketModel>.Success(updatedTicket, "Prioridad del ticket cambiada exitosamente.");
    }
}
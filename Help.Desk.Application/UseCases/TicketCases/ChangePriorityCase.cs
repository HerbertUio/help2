using FluentValidation;
using Help.Desk.Application.Validators.TicketValidators;
using Help.Desk.Domain.Dtos.TicketDtos;
using Help.Desk.Domain.Enums.TicketEnums;
using Help.Desk.Domain.IRepositories;
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

    public async Task<Result<TicketDto>> ExecuteAsync(int ticketId, ChangePriorityDto changePriorityDto)
    {
        var validationResult = await _validator.ValidateAsync(changePriorityDto);
        if (!validationResult.IsValid)
        {
            return Result<TicketDto>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                "Error de validación al cambiar prioridad del ticket."
            );
        }
        var existingTicket = await _ticketRepository.GetByIdAsync(ticketId);
        if (existingTicket == null)
        {
            return Result<TicketDto>.Failure(
                new List<string> { "El ticket no existe." },
                "Error al cambiar prioridad del ticket."
            );
        }
        if (!Enum.IsDefined(typeof(Priority), changePriorityDto.NewPriorityId))
        {
            return Result<TicketDto>.Failure(
                new List<string> { $"El ID de prioridad '{changePriorityDto.NewPriorityId}' no es válido." },
                "Error al cambiar la prioridad del ticket."
            );
        }
        var newPriority = (Priority)changePriorityDto.NewPriorityId;
        existingTicket.ChangePriority(newPriority);
        var updatedTicket = await _ticketRepository.UpdateAsync(existingTicket);
        if (updatedTicket == null)
        {
            return Result<TicketDto>.Failure(
                new List<string> { "Error al cambiar la prioridad del ticket." },
                "Error al cambiar prioridad del ticket."
            );
        }
        return Result<TicketDto>.Success(updatedTicket, "Prioridad del ticket cambiada exitosamente.");
    }
}
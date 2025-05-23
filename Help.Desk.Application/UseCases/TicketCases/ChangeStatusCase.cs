using FluentValidation;
using Help.Desk.Domain.Dtos.TicketDtos;
using Help.Desk.Domain.Enums.TicketEnums;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.TicketCases;

public class ChangeStatusCase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IValidator<ChangeStatusDto> _validator;
    public ChangeStatusCase(ITicketRepository ticketRepository, IValidator<ChangeStatusDto> validator)
    {
        _ticketRepository = ticketRepository;
        _validator = validator;
    }
    public async Task<Result<TicketDto>> ExecuteAsync(int id, ChangeStatusDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return Result<TicketDto>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                "Error de validación al cambiar el estado del ticket."
            );
        }

        var existingTicket = await _ticketRepository.GetByIdAsync(id);
        if (existingTicket == null)
        {
            return Result<TicketDto>.Failure(
                new List<string> { "No se encontró el ticket." },
                "Error al cambiar el estado del ticket."
            );
        }
        if (!Enum.IsDefined(typeof(Status), dto.NewStatusId))
        {
            return Result<TicketDto>.Failure(
                new List<string> { $"El ID de estado '{dto.NewStatusId}' no es válido." },
                "Error al cambiar el estado del ticket."
            );
        }
        var newStatus = (Status)dto.NewStatusId;
        existingTicket.ChangeStatus(newStatus);
        var updatedTicket = await _ticketRepository.UpdateAsync(existingTicket);
        if (updatedTicket == null)
        {
            return Result<TicketDto>.Failure(
                new List<string> { "Error al cambiar el estado del ticket." },
                "Error al cambiar el estado del ticket."
            );
        }
        return Result<TicketDto>.Success(updatedTicket, "Estado del ticket cambiado exitosamente.");
    }
    
    
}
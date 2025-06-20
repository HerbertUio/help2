using FluentValidation;
using Help.Desk.Domain.Dtos.TicketDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Models;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.TicketCases;

public class UpdateTicketCase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IValidator<UpdateTicketDto> _updateTicketValidator;
    
    public UpdateTicketCase(ITicketRepository ticketRepository, IValidator<UpdateTicketDto> updateTicketValidator)
    {
        _ticketRepository = ticketRepository;
        _updateTicketValidator = updateTicketValidator;
    }
    
    public async Task<Result<TicketModel>> ExecuteAsync(int id, UpdateTicketDto updateTicketDto)
    {
        var validationResult = await _updateTicketValidator.ValidateAsync(updateTicketDto);
        if (!validationResult.IsValid)
        {
            return Result<TicketModel>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                "Error de validación al actualizar ticket."
            );
        }
        
        var existingTicket = await _ticketRepository.GetByIdAsync(id);
        if (existingTicket == null)
        {
            return Result<TicketModel>.Failure(
                new List<string> { "El ticket no existe." },
                "Error al actualizar ticket."
            );
        }
        
        existingTicket.Update(
            updateTicketDto.Title,
            updateTicketDto.Description,
            updateTicketDto.PriorityId,
            updateTicketDto.StatusId,
            updateTicketDto.PrimaryTicketId,
            updateTicketDto.ParentTicketId,
            updateTicketDto.AssignedAgentId,
            updateTicketDto.AssignedGroupId,
            updateTicketDto.TypeTicketId,
            updateTicketDto.OfficeId,
            updateTicketDto.AreaId,
            updateTicketDto.SubjectId
            );
        var updatedTicket = await _ticketRepository.UpdateAsync(existingTicket);
        
        if (updatedTicket == null)
        {
            return Result<TicketModel>.Failure(
                new List<string> { "Error al actualizar el ticket." },
                "Error al actualizar ticket."
            );
        }
        
        return Result<TicketModel>.Success(updatedTicket, "Ticket actualizado exitosamente.");
    }
}
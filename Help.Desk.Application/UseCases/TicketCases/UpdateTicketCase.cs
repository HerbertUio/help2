using FluentValidation;
using Help.Desk.Domain.Dtos.TicketDtos;
using Help.Desk.Domain.IRepositories;
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
    
    public async Task<Result<TicketDto>> ExecuteAsync(int id, UpdateTicketDto updateTicketDto)
    {
        var validationResult = await _updateTicketValidator.ValidateAsync(updateTicketDto);
        if (!validationResult.IsValid)
        {
            return Result<TicketDto>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                "Error de validación al actualizar ticket."
            );
        }
        
        var existingTicket = await _ticketRepository.GetByIdAsync(id);
        if (existingTicket == null)
        {
            return Result<TicketDto>.Failure(
                new List<string> { "El ticket no existe." },
                "Error al actualizar ticket."
            );
        }
        // Mapear los campos de UpdateTicketDto a Ticket
        existingTicket.Title = updateTicketDto.Title;
        existingTicket.Description = updateTicketDto.Description;
        
        
        
        var updatedTicket = await _ticketRepository.UpdateAsync(ticketToUpdate);
        if (updatedTicket == null)
        {
            return Result<TicketDto>.Failure(
                new List<string> { "Error al actualizar el ticket." },
                "Error al actualizar ticket."
            );
        }
        
        return Result<TicketDto>.Success(updatedTicket, "Ticket actualizado exitosamente.");
    }
}
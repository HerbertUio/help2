using FluentValidation;
using Help.Desk.Domain.Dtos.TicketDtos;
using Help.Desk.Domain.Enums.TicketEnums;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Models;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.TicketCases;

public class CreateTicketCase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IValidator<CreateTicketDto> _createTicketValidator;
    
    public CreateTicketCase(ITicketRepository ticketRepository, IValidator<CreateTicketDto> createTicketValidator)
    {
        _ticketRepository = ticketRepository;
        _createTicketValidator = createTicketValidator;
    }

    public async Task<Result<TicketModel>> ExecuteAsync(CreateTicketDto dto)
    {
        var validationResult = await _createTicketValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return Result<TicketModel>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                "Error de validación al crear ticket."
            );
        }

        var ticket = TicketModel.Create(
            dto.RequesterId,
            dto.Title,
            dto.Description,
            dto.SubjectId,
            dto.OfficeId,
            dto.AreaId,
            dto.TypeTicketId
        );
        
        var createdTicket = await _ticketRepository.CreateAsync(ticket);
        if (createdTicket == null)
        {
            return Result<TicketModel>
                .Failure(new List<string>(), "Error al crear ticket.");
        }
        return Result<TicketModel>.Success(createdTicket, "Ticket creado exitosamente.");
        
    }
    
}
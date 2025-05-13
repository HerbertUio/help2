using FluentValidation;
using Help.Desk.Domain.Dtos.TicketDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.TicketCases;

public class RegisterTicketCase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IValidator<RegisterTicketDto> _registerTicketValidator;

    public RegisterTicketCase(ITicketRepository ticketRepository, IValidator<RegisterTicketDto> registerTicketValidator)
    {
        _ticketRepository = ticketRepository;
        _registerTicketValidator = registerTicketValidator;
    }
    
    public async Task<Result<TicketDto>> ExecuteRegisterTicketAsync(RegisterTicketDto registerTicketDto)
    {
        var validationResult = await _registerTicketValidator.ValidateAsync(registerTicketDto);
        if (!validationResult.IsValid)
        {
            return Result<TicketDto>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                "Error de validación al crear ticket."
            );
        }
        var newTicket = new TicketDto
        {
            Title = registerTicketDto.Title,
            Description = registerTicketDto.Description,
            Created = DateTime.UtcNow,
            RequesterId = registerTicketDto.RequesterId,
            TypeTicketId = registerTicketDto.TypeTicketId,
            OfficeId = registerTicketDto.OfficeId,
            SubjectId = registerTicketDto.SubjectId
        };
        var createdTicket = await _ticketRepository.CreateAsync(newTicket);
        if (createdTicket == null)
        {
            return Result<TicketDto>.Failure(
                new List<string> { "Error al crear el ticket." },
                "Error al crear ticket."
            );
        }
        return Result<TicketDto>.Success(createdTicket, "Ticket creado exitosamente.");
    }
}
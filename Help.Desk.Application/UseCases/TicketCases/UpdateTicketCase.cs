using FluentValidation;
using Help.Desk.Domain.Dtos.TicketDtos;
using Help.Desk.Domain.IRepositories;

namespace Help.Desk.Application.UseCases.TicketCases;

public class UpdateTicketCase
{
    private readonly ITicketRepository _ticketRepository;
    //private readonly IValidator<UpdateTicketCase> _registerTicketValidator;
}
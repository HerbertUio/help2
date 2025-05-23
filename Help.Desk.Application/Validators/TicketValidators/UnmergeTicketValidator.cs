using FluentValidation;
using Help.Desk.Domain.Dtos.TicketDtos;

namespace Help.Desk.Application.Validators.TicketValidators;

public class UnmergeTicketValidator: AbstractValidator<UnmergeTicketDto>
{
    public UnmergeTicketValidator()
    {
        RuleFor(x => x.TicketId)
            .GreaterThan(0).WithMessage("El ID del ticket principal debe ser mayor a 0.");

        RuleFor(x => x.TicketToUnmergeId)
            .GreaterThan(0).WithMessage("El ID del ticket a desfusionar debe ser mayor a 0.");

        RuleFor(x => x)
            .Must(x => x.TicketId != x.TicketToUnmergeId)
            .WithMessage("Un ticket no puede desfusionarse de sí mismo.")
            .WithName("SelfUnmerge");
    }
}
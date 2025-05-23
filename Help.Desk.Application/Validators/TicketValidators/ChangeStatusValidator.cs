using FluentValidation;
using Help.Desk.Domain.Dtos.TicketDtos;
using Help.Desk.Domain.Enums.TicketEnums;

namespace Help.Desk.Application.Validators.TicketValidators;

public class ChangeStatusValidator: AbstractValidator<ChangeStatusDto>
{
    public ChangeStatusValidator()
    {
        RuleFor(x => x.NewStatusId)
            .NotEmpty().WithMessage("El nuevo estado es obligatorio.")
            .Must(BeAValidStatus).WithMessage("El ID de estado no es un valor válido.");
    }
    private bool BeAValidStatus(int statusId)
    {
        return Enum.IsDefined(typeof(Status), statusId);
    }
}
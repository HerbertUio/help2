using FluentValidation;
using Help.Desk.Domain.Dtos.TicketDtos;

namespace Help.Desk.Application.Validators.TicketValidators;

public class UpdateTicketValidator: AbstractValidator<UpdateTicketDto>
{
    public UpdateTicketValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El título no puede estar vacío.")
            .MinimumLength(3).WithMessage("El título debe tener al menos 3 caracteres.")
            .MaximumLength(100).WithMessage("El título debe tener menos de 100 caracteres.")
            .Matches(@"^[a-zA-ZÀ-ÿ0-9\s]+$").WithMessage("El título contiene caracteres inválidos.")
            .When(x => !string.IsNullOrWhiteSpace(x.Title));
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripcicion es requerida.")
            .MinimumLength(3).WithMessage("La descripcicion tener al menos 3 caracteres.")
            .Matches(@"^[a-zA-ZÀ-ÿ0-9\s]+$").WithMessage("El título contiene caracteres inválidos.")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
        //TODO: AVERIGUAR LAS FECHAS PARA EL TICKET
        
            
    }
}
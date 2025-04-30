using FluentValidation;
using Help.Desk.Domain.Dtos.DepartmentDtos;

namespace Help.Desk.Application.Validators.DepartmentValidators;

public class RegisterDepartmentValidator : AbstractValidator<RegisterDepartmentDto>
{
    public RegisterDepartmentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("El nombre del departamento es obligatorio.")
            .Length(2, 50)
            .WithMessage("El nombre del departamento debe tener entre 2 y 50 caracteres.");
    }
}
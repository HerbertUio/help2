using FluentValidation;
using Help.Desk.Domain.Dtos.DepartmentDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.DepartmentUseCases;

public class CreateDepartmentUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IValidator<RegisterDepartmentDto> _departmentValidator;
    public CreateDepartmentUseCase(IDepartmentRepository departmentRepository, IValidator<RegisterDepartmentDto> departmentValidator)
    {
        _departmentRepository = departmentRepository;
        _departmentValidator = departmentValidator;
    }
    
    public async Task<Result<DepartmentDto>> ExecuteCreateDepartmentAsync(RegisterDepartmentDto registerDepartmentDto)
    {
        var validationResult = await _departmentValidator.ValidateAsync(registerDepartmentDto);
        if (!validationResult.IsValid)
        {
            return Result<DepartmentDto>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                "Error de validacion en el departamento."
            );
        }
        var existingDepartment = await _departmentRepository.GetByNameAsync(registerDepartmentDto.Name);
        if (existingDepartment != null)
        {
            return Result<DepartmentDto>.Failure(
                new List<string> { "El nombre del departamento ya está en uso." },
                "Error al crear el departamento."
            );
        }

        var department = new DepartmentDto
        {
            Name = registerDepartmentDto.Name,
            Description = registerDepartmentDto.Description
        };

        var createdDepartment = await _departmentRepository.CreateAsync(department);
        if (createdDepartment == null)
        {
            return Result<DepartmentDto>.Failure(
                new List<string> { "Error al crear el departamento." },
                "Error al crear el departamento."
            );
        }

        return Result<DepartmentDto>.Success(createdDepartment, "Departamento creado exitosamente.");
    }
}
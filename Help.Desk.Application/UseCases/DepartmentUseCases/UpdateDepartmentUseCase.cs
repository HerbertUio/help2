using FluentValidation;
using Help.Desk.Domain.Dtos.DepartmentDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.DepartmentUseCases;

public class UpdateDepartmentUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IValidator<RegisterDepartmentDto> _departmentValidator;
    
    public UpdateDepartmentUseCase(IDepartmentRepository departmentRepository, IValidator<RegisterDepartmentDto> departmentValidator)
    {
        _departmentRepository = departmentRepository;
        _departmentValidator = departmentValidator;
    }
    
    public async Task<Result<DepartmentDto>> ExecuteUpdateAsync(int id, RegisterDepartmentDto departmentDto)
    {
        var validationResult = await _departmentValidator.ValidateAsync(departmentDto);
        if (!validationResult.IsValid)
        {
            return Result<DepartmentDto>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                "Error al validar los datos del departamento."
            );
        }

        var existingDepartment = await _departmentRepository.GetByIdAsync(id);
        if (existingDepartment == null)
        {
            return Result<DepartmentDto>.Failure(
                new List<string> { "El departamento no existe." },
                "Error al actualizar el departamento."
            );
        }

        existingDepartment.Name = departmentDto.Name;
        existingDepartment.Description = departmentDto.Description;

        var updatedDepartment = await _departmentRepository.UpdateAsync(existingDepartment);
        
        return Result<DepartmentDto>.Success(updatedDepartment, "Departamento actualizado exitosamente.");
    }
}
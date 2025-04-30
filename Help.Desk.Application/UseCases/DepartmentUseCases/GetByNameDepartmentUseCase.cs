using Help.Desk.Domain.Dtos.DepartmentDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.DepartmentUseCases;

public class GetByNameDepartmentUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    
    public GetByNameDepartmentUseCase(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    public async Task<Result<DepartmentDto>> ExecuteGetByNameAsync(string name)
    {
        var department = await _departmentRepository.GetByNameAsync(name);
        if (department == null)
        {
            return Result<DepartmentDto>.Failure(
                new List<string> { "No se encontró el departamento." },
                "Error al obtener el departamento."
            );
        }
        return Result<DepartmentDto>.Success(department, "Departamento obtenido exitosamente.");
    }
}
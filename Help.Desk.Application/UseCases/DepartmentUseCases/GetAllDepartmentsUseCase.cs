using Help.Desk.Domain.Dtos.DepartmentDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.DepartmentUseCases;

public class GetAllDepartmentsUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    public GetAllDepartmentsUseCase(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    public async Task<Result<List<DepartmentDto>>> ExecuteGetAllAsync()
    {
        var departments = await _departmentRepository.GetAllAsync();
        if (departments == null || !departments.Any())
        {
            return Result<List<DepartmentDto>>.Failure(
                new List<string> { "No se encontraron departamentos." },
                "Error al obtener departamentos."
            );
        }
        return Result<List<DepartmentDto>>.Success(departments, "Departamentos obtenidos exitosamente.");
    }
}
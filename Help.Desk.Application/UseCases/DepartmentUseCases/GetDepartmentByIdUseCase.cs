using Help.Desk.Domain.Dtos.DepartmentDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.DepartmentUseCases;

public class GetDepartmentByIdUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    public GetDepartmentByIdUseCase(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    public async Task<Result<DepartmentDto>> ExecuteGetByIdAsync(int id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);
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
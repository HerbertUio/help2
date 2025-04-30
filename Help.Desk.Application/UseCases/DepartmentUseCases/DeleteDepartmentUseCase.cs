using Help.Desk.Domain.Dtos.DepartmentDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.DepartmentUseCases;

public class DeleteDepartmentUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    public DeleteDepartmentUseCase(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Result<bool>> ExecuteDeleteAsync(int id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);
        if (department == null)
        {
            return Result<bool>.Failure(
                new List<string> { "No se encontró el departamento." },
                "Error al obtener el departamento."
            );
        }
        var result = await _departmentRepository.DeleteAsync(id);
        if (!result)
        {
            return Result<bool>.Failure(
                new List<string> { "Error al eliminar el departamento." },
                "Error al eliminar departamento."
            );
        }
        return Result<bool>.Success(true, "Departamento eliminado exitosamente.");
    }
    
}
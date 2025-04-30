using Help.Desk.Domain.Dtos.UserDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.DepartmentUseCases;

public class GetAllUsersInDepartment
{
    private readonly IDepartmentRepository _departmentRepository;
    public GetAllUsersInDepartment(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    public async Task<Result<List<UserDto>>> ExecuteGetAllUsersInDepartmentAsync(int departmentId)
    {
        var users = await _departmentRepository.GetUsersByDepartmentIdAsync(departmentId);
        
        if (users == null || !users.Any())
        {
            return Result<List<UserDto>>.Failure(
                new List<string> { "No se encontraron usuarios en el departamento." },
                "Error al obtener usuarios en el departamento."
            );
        }
        return Result<List<UserDto>>.Success(users, "Usuarios en el departamento obtenidos exitosamente.");
    }
}
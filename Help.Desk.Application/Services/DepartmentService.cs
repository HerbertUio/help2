using Help.Desk.Application.UseCases.DepartmentUseCases;
using Help.Desk.Domain.Dtos.DepartmentDtos;
using Help.Desk.Domain.Dtos.UserDtos;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.Services;

public class DepartmentService
{
    private readonly CreateDepartmentUseCase _createDepartmentUseCase;
    private readonly DeleteDepartmentUseCase _deleteDepartmentUseCase;
    private readonly GetAllDepartmentsUseCase _getAllDepartmentsUseCase;
    private readonly GetDepartmentByIdUseCase _getDepartmentByIdUseCase;
    private readonly UpdateDepartmentUseCase _updateDepartmentUseCase;
    private readonly GetAllUsersInDepartment _getAllUsersInDepartmentByIdUseCase;
    private readonly GetByNameDepartmentUseCase _getByNameDepartmentUseCase;
    
    public DepartmentService(
        CreateDepartmentUseCase createDepartmentUseCase,
        DeleteDepartmentUseCase deleteDepartmentUseCase,
        GetAllDepartmentsUseCase getAllDepartmentsUseCase,
        GetDepartmentByIdUseCase getDepartmentByIdUseCase,
        UpdateDepartmentUseCase updateDepartmentUseCase,
        GetAllUsersInDepartment getAllUsersInDepartmentByIdUseCase
    )
    {
        _createDepartmentUseCase = createDepartmentUseCase;
        _deleteDepartmentUseCase = deleteDepartmentUseCase;
        _getAllDepartmentsUseCase = getAllDepartmentsUseCase;
        _getDepartmentByIdUseCase = getDepartmentByIdUseCase;
        _updateDepartmentUseCase = updateDepartmentUseCase;
        _getAllUsersInDepartmentByIdUseCase = getAllUsersInDepartmentByIdUseCase;
    }

    public async Task<Result<DepartmentDto>> CreateAsync(RegisterDepartmentDto registerDepartmentDto)
    {
        var result = await _createDepartmentUseCase.ExecuteCreateDepartmentAsync(registerDepartmentDto);
        return result;
    }
    public async Task<Result<DepartmentDto>> UpdateAsync(int id, RegisterDepartmentDto registerDepartmentDto)
    {
        var result = await _updateDepartmentUseCase.ExecuteUpdateAsync(id, registerDepartmentDto);
        return result;
    }
    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var result = await _deleteDepartmentUseCase.ExecuteDeleteAsync(id);
        return result;
    }
    public async Task<Result<DepartmentDto>> GetByIdAsync(int id)
    {
        var result = await _getDepartmentByIdUseCase.ExecuteGetByIdAsync(id);
        return result;
    }
    public async Task<Result<List<DepartmentDto>>> GetAllAsync()
    {
        var result = await _getAllDepartmentsUseCase.ExecuteGetAllAsync();
        return result;
    }
    public async Task<Result<List<UserDto>>> GetAllUsersInDepartmentByIdAsync(int id)
    {
        var result = await _getAllUsersInDepartmentByIdUseCase.ExecuteGetAllUsersInDepartmentAsync(id);
        return result;
    }
    public async Task<Result<DepartmentDto>> GetByNameAsync(string name)
    {
        var result = await _getByNameDepartmentUseCase.ExecuteGetByNameAsync(name);
        return result;
    }
}
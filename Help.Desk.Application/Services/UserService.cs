using Help.Desk.Application.UseCases.UserUseCases;
using Help.Desk.Domain.Dtos.UserDtos;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.Services;

public class UserService
{
    private readonly CreateUserUseCase _createUserUseCase;
    private readonly UpdateUserUseCase _updateUserUseCase;
    private readonly DeleteUserUseCase _deleteUserUseCase;
    private readonly GetUserByIdUseCase _getUserByIdUseCase;
    private readonly GetAllUsersUseCase _getAllUsersUseCase;
    private readonly GetByEmailUseCase _getByEmailUseCase;
    
    public UserService(
        CreateUserUseCase createUserUseCase,
        UpdateUserUseCase updateUserUseCase,
        DeleteUserUseCase deleteUserUseCase,
        GetUserByIdUseCase getUserByIdUseCase,
        GetAllUsersUseCase getAllUsersUseCase,
        GetByEmailUseCase getByEmailUseCase
    )
    {
        _createUserUseCase = createUserUseCase;
        _updateUserUseCase = updateUserUseCase;
        _deleteUserUseCase = deleteUserUseCase;
        _getUserByIdUseCase = getUserByIdUseCase;
        _getAllUsersUseCase = getAllUsersUseCase;
        _getByEmailUseCase = getByEmailUseCase;
    }

    public async Task<Result<UserDto>> CreateAsync(RegisterUserDto registerUserDto)
    {
        var result = await _createUserUseCase.ExecuteCreateUserAsync(registerUserDto);
        return result;
    }
    public async Task<Result<UserDto>> UpdateAsync(int id, RegisterUserDto registerUserDto)
    {
        var result = await _updateUserUseCase.ExecuteUpdateAsync(id, registerUserDto);
        return result;
    }
    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var result = await _deleteUserUseCase.ExecuteDeleteAsync(id);
        return result;
    }
    public async Task<Result<UserDto>> GetByIdAsync(int id)
    {
        var result = await _getUserByIdUseCase.ExecuteGetByIdAsync(id);
        return result;
    }
    public async Task<Result<List<UserDto>>> GetAllAsync()
    {
        var result = await _getAllUsersUseCase.ExecuteGetAllAsync();
        return result;
    }
    public async Task<Result<UserDto>> GetByEmailAsync(string email)
    {
        var result = await _getByEmailUseCase.ExecuteGetByEmailAsync(email);
        return result;
    }
    
}
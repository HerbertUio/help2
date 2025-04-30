using Help.Desk.Domain.Dtos.UserDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.UserUseCases;

public class GetAllUsersUseCase
{
    private readonly IUserRepository _userRepository;
    public GetAllUsersUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Result<List<UserDto>>> ExecuteGetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        if (users == null || !users.Any())
        {
            return Result<List<UserDto>>.Failure(
                new List<string> { "No se encontraron usuarios." },
                "Error al obtener usuarios."
            );
        }
        return Result<List<UserDto>>.Success(users, "Usuarios obtenidos exitosamente.");
    }
}
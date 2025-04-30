using Help.Desk.Domain.Dtos.UserDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.UserUseCases;

public class GetUserByIdUseCase
{
    private readonly IUserRepository _userRepository;
    public GetUserByIdUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Result<UserDto>> ExecuteGetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return Result<UserDto>.Failure(
                new List<string> { "No se encontró el usuario." },
                "Error al obtener el usuario."
            );
        }
        return Result<UserDto>.Success(user, "Usuario obtenido exitosamente.");
    }
}
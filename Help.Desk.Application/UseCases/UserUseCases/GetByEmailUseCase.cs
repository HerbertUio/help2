using Help.Desk.Domain.Dtos.UserDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.UserUseCases;

public class GetByEmailUseCase
{
    private readonly IUserRepository _userRepository;
    public GetByEmailUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Result<UserDto>> ExecuteGetByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
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
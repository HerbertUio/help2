using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.UserUseCases;

public class DeleteUserUseCase
{
    private readonly IUserRepository _userRepository;
    public DeleteUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Result<bool>> ExecuteDeleteAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return Result<bool>.Failure(
                new List<string> { "No se encontró el usuario." },
                "Error al obtener el usuario."
            );
        }
        var result = await _userRepository.DeleteAsync(id);
        if (!result)
        {
            return Result<bool>.Failure(
                new List<string> { "Error al eliminar el usuario." },
                "Error al eliminar usuario."
            );
        }
        return Result<bool>.Success(true, "Usuario eliminado exitosamente.");
    }
}
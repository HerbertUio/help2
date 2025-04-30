using FluentValidation;
using Help.Desk.Domain.Dtos.UserDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.UserUseCases;

public class UpdateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<RegisterUserDto> _userValidator;
    public UpdateUserUseCase(IUserRepository userRepository, IValidator<RegisterUserDto> userValidator)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
    }
    
    public async Task<Result<UserDto>> ExecuteUpdateAsync(int id, RegisterUserDto registerUserDto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return Result<UserDto>.Failure(
                new List<string> { "No se encontró el usuario." },
                "Error al obtener el usuario."
            );
        }
        var validationResult = await _userValidator.ValidateAsync(registerUserDto);
        if (!validationResult.IsValid)
        {
            return Result<UserDto>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                "Error de validación al actualizar usuario."
            );
        }
        var existingUser = await _userRepository.GetByEmailAsync(registerUserDto.Email);
        if (existingUser != null && existingUser.Id != id)
        {
            return Result<UserDto>.Failure(
                new List<string> { "El correo electrónico ya está en uso." },
                "Error al actualizar usuario."
            );
        }
        user.Name = registerUserDto.Name;
        user.LastName = registerUserDto.LastName;
        user.PhoneNumber = registerUserDto.PhoneNumber;
        user.Email = registerUserDto.Email;
        user.Password = registerUserDto.Password;
        user.DepartmentId = registerUserDto.DepartmentId;
        user.Role = registerUserDto.Role;
        user.Active = true;
        user.IsAgent = registerUserDto.IsAgent;
        
        
        var updatedUser = await _userRepository.UpdateAsync(user);
        if (updatedUser == null)
        {
            return Result<UserDto>.Failure(
                new List<string> { "Error al actualizar el usuario." },
                "Error al actualizar usuario."
            );
        }
        return Result<UserDto>.Success(updatedUser, "Usuario actualizado exitosamente.");
    }
}
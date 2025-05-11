using System.Net;
using FluentValidation;
using Help.Desk.Domain.Dtos.UserDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.UserUseCases;

public class CreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<RegisterUserDto> _registerUserValidator;

    public CreateUserUseCase(IUserRepository userRepository, IValidator<RegisterUserDto> registerUserValidator)
    {
        _userRepository = userRepository;
        _registerUserValidator = registerUserValidator;
    }

    public async Task<Result<UserDto>> ExecuteCreateUserAsync(RegisterUserDto registerUserDto)
    {
        var validationResult = await _registerUserValidator.ValidateAsync(registerUserDto);
        if (!validationResult.IsValid)
        {
            return Result<UserDto>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                "Error de validación al crear usuario."
            );
        }
        var existingUser = await _userRepository.GetByEmailAsync(registerUserDto.Email);
        if (existingUser != null)
        {
            return Result<UserDto>.Failure(
                new List<string> { "El correo electrónico ya está en uso." },
                "Error al crear usuario."
            );
        }
        // TODO: Implementar la encriptación de la contraseña aqui
        var newUser = new UserDto
        {
            Name = registerUserDto.Name,
            LastName = registerUserDto.LastName,
            PhoneNumber = registerUserDto.PhoneNumber,
            Email = registerUserDto.Email,
            Password = registerUserDto.Password, // registerUserDto.Password = EncryptPassword(registerUserDto.Password);
            DepartmentId = registerUserDto.DepartmentId,
            Role = registerUserDto.Role,
            Active = true,
            IsAgent = registerUserDto.IsAgent
        };
        var createdUser = await _userRepository.CreateAsync(newUser);
        if (createdUser == null)
        {
            return Result<UserDto>.Failure(
                new List<string> { "Error al crear el usuario." },
                "Error al crear usuario."
            );
        }
        return Result<UserDto>.Success(createdUser, "Usuario creado exitosamente.");
    }
}
using System.Net;
using FluentValidation;
using Help.Desk.Domain.Dtos.SupportGroupDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.UseCases.SupportGroupUseCases;

public class CreateSupportGroupUseCase
{
    private readonly ISupportGroupRepository _supportGroupRepository;
    private readonly IValidator<RegisterSupportGroupDto> _registerSupportGroupValidator;
    
    public CreateSupportGroupUseCase(ISupportGroupRepository supportGroupRepository, IValidator<RegisterSupportGroupDto> registerSupportGroupValidator)
    {
        _supportGroupRepository = supportGroupRepository;
        _registerSupportGroupValidator = registerSupportGroupValidator;
    }

    public async Task<Result<SupportGroupDto>> ExecuteCreateSupportGroupAsync(RegisterSupportGroupDto registerSupportGroupDto)
    {
        var validationResult = await _registerSupportGroupValidator.ValidateAsync(registerSupportGroupDto);
        if (!validationResult.IsValid)
        {
            return Result<SupportGroupDto>.Failure(
                validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                "Error de validación al crear grupo de soporte."
            );
        }

        var supportGroup = new SupportGroupDto
        {
            Name = registerSupportGroupDto.Name,
            IsSecondLevel = registerSupportGroupDto.IsSecondLevel
        };
        var createdSupportGroup = await _supportGroupRepository.CreateAsync(supportGroup);
        if (createdSupportGroup == null)
        {
            return Result<SupportGroupDto>.Failure(
                new List<string> { "Error al crear el grupo de soporte." },
                "Error al crear grupo de soporte."
            );
        }
        return Result<SupportGroupDto>.Success(createdSupportGroup, HttpStatusCode.Created,"Grupo de soporte creado exitosamente.");
    }
}
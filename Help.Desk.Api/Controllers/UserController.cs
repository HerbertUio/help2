using Help.Desk.Application.Services;
using Help.Desk.Domain.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace Help.Desk.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserService _userService;
    public UserController(ILogger<UserController> logger, UserService userService)
    {
        _logger = logger;
        _userService = userService;
    }
    [HttpGet]
    [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _userService.GetAllAsync();
        if (!result.IsSuccess)
        {
            _logger.LogError("Error al obtener usuarios: {Errors}", string.Join(", ", result.Errors));
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
        return Ok(result.Data);
        
    }
    [HttpGet("{id:int}", Name = "GetUserById")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _userService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            _logger.LogError("Error al obtener usuario por ID: {Errors}", string.Join(", ", result.Errors));
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpGet("{email}", Name = "GetUserByEmail")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var result = await _userService.GetByEmailAsync(email);
        if (!result.IsSuccess)
        {
            _logger.LogError("Error al obtener usuario por correo: {Errors}", string.Join(", ", result.Errors));
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] RegisterUserDto registerUserDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _userService.CreateAsync(registerUserDto);
        if (!result.IsSuccess)
        {
            _logger.LogError("Error al crear usuario: {Errors}", string.Join(", ", result.Errors));
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
        return CreatedAtRoute("GetUserById", new { id = result.Data.Id }, result.Data);
    }
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] RegisterUserDto registerUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _userService.UpdateAsync(id, registerUserDto);
        if (!result.IsSuccess)
        {
            _logger.LogError("Error al actualizar usuario: {Errors}", string.Join(", ", result.Errors));
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _userService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            _logger.LogError("Error al eliminar usuario: {Errors}", string.Join(", ", result.Errors));
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
        return NoContent();
    }
    
}
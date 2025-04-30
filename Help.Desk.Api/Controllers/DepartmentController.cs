using Help.Desk.Application.Services;
using Help.Desk.Domain.Dtos.DepartmentDtos;
using Help.Desk.Infrastructure.Database.EntityFramework.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Help.Desk.Api.Controllers;
[ApiController]
[Route("api/department/[controller]")]
public class DepartmentController: ControllerBase
{
    private readonly ILogger<DepartmentController> _logger;
    private readonly DepartmentService _departmentService;
    public DepartmentController(ILogger<DepartmentController> logger, DepartmentService departmentService)
    {
        _logger = logger;
        _departmentService = departmentService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(List<DepartmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllDepartments()
    {
        var result = await _departmentService.GetAllAsync();
        if (!result.IsSuccess)
        {
            _logger.LogError("Error Al obtener departamentos: {Errors}", result.Errors);
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
        return Ok(result.Data);
    }
    
    [HttpGet("{id:int}", Name = "GetDepartmentById")]
    [ProducesResponseType(typeof(DepartmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDepartmentById(int id)
    {
        var result = await _departmentService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            _logger.LogError("Error Al obtener departamento: {Errors}", result.Errors);
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpGet("{name}", Name = "GetDepartmentByName")]
    [ProducesResponseType(typeof(DepartmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDepartmentByName(string name)
    {
        var result = await _departmentService.GetByNameAsync(name);
        if (!result.IsSuccess)
        {
            _logger.LogError("Error Al obtener departamento: {Errors}", result.Errors);
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
        return Ok(result.Data);
    }
    
    [HttpGet("{id:int}/users", Name = "GetAllUsersInDepartmentById")]
    [ProducesResponseType(typeof(List<DepartmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllUsersInDepartmentById(int id)
    {
        var result = await _departmentService.GetAllUsersInDepartmentByIdAsync(id);
        if (!result.IsSuccess)
        {
            _logger.LogError("Error Al obtener usuarios en departamento: {Errors}", result.Errors);
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
        return Ok(result.Data);
    }
    
    
    [HttpPost]
    [ProducesResponseType(typeof(DepartmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateDepartment([FromBody] RegisterDepartmentDto departmentDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var result = await _departmentService.CreateAsync(departmentDto);
        if (!result.IsSuccess)
        {
            _logger.LogError("Error Al crear departamento: {Errors}", result.Errors);
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
        return CreatedAtRoute("GetDepartmentById", new { id = result.Data.Id }, result.Data);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(DepartmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateDepartment(int id, [FromBody] RegisterDepartmentDto departmentDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var result = await _departmentService.UpdateAsync(id, departmentDto);
        if (!result.IsSuccess)
        {
            _logger.LogError("Error Al actualizar departamento: {Errors}", result.Errors);
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var result = await _departmentService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            _logger.LogError("Error Al eliminar departamento: {Errors}", result.Errors);
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
        return NoContent();
    }
    
}
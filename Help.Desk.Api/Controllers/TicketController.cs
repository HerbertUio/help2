using Help.Desk.Application.Services;
using Help.Desk.Domain.Dtos.TicketDtos;
using Help.Desk.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Help.Desk.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TicketController: ControllerBase
{
    private readonly ILogger<TicketController> _logger;
    private readonly TicketService _ticketService;
    
    public TicketController(ILogger<TicketController> logger, TicketService ticketService)
    {
        _logger = logger;
        _ticketService = ticketService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(List<TicketModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllTickets()
    {
        var result = await _ticketService.GetAllAsync();
        if (!result.IsSuccess)
        {
            _logger.LogError("Error al obtener todos los tickets: {Errors}", string.Join(", ", result.Errors));
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
        return Ok(result.Data);
    }
    
    [HttpGet("{id:int}", Name = "GetTicketById")]
    [ProducesResponseType(typeof(TicketModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTicketById(int id)
    {
        var result = await _ticketService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            if (result.Errors.Contains("No se encontró el ticket."))
            {
                return NotFound(result.Errors);
            }
            _logger.LogError("Error al obtener el ticket por ID {TicketId}: {Errors}", id, string.Join(", ", result.Errors));
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
        return Ok(result.Data);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(TicketModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto createTicketDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _ticketService.CreateAsync(createTicketDto);
        if (!result.IsSuccess)
        {
            _logger.LogError("Error al crear el ticket: {Errors}", string.Join(", ", result.Errors));
            return BadRequest(result.Errors);
        }

        return CreatedAtRoute("GetTicketById", new { id = result.Data.Id }, result.Data);
    }
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(TicketModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateTicket(int id, [FromBody] UpdateTicketDto updateTicketDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _ticketService.UpdateAsync(id, updateTicketDto);
        if (!result.IsSuccess)
        {
            _logger.LogError("Error al actualizar el ticket {TicketId}: {Errors}", id, string.Join(", ", result.Errors));
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var result = await _ticketService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            _logger.LogError("Error al eliminar el ticket {TicketId}: {Errors}", id, string.Join(", ", result.Errors));
            return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }
        return NoContent();
    }
    
    [HttpPost("{id:int}/status")]
    [ProducesResponseType(typeof(TicketModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangeStatus(int id, [FromBody] ChangeStatusDto dto)
    {
        var result = await _ticketService.ChangeStatusAsync(id, dto);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);
    }
    
    [HttpPost("{id:int}/priority")]
    [ProducesResponseType(typeof(TicketModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePriority(int id, [FromBody] ChangePriorityDto dto)
    {
        var result = await _ticketService.ChangePriorityAsync(id, dto);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpPost("{id:int}/close")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CloseTicket(int id)
    {
        var result = await _ticketService.CloseTicketAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }
        return NoContent();
    }
    [HttpPost("{id:int}/reopen")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ReopenTicket(int id)
    {
        var result = await _ticketService.ReopenTicketAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }
        return NoContent();
    }
    [HttpPost("merge")]
    [ProducesResponseType(typeof(TicketModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MergeTickets([FromBody] MergeTicketsDto dto)
    {
        var result = await _ticketService.MergeTicketsAsync(dto);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);
    }
    
}
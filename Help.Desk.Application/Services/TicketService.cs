using Help.Desk.Application.UseCases.TicketCases;
using Help.Desk.Domain.Dtos.TicketDtos;
using Help.Desk.Domain.Responses;

namespace Help.Desk.Application.Services;

public class TicketService
{
    private readonly CreateTicketCase _createTicketCase;
    private readonly UpdateTicketCase _updateTicketCase;
    private readonly DeleteTicketCase _deleteTicketCase;
    private readonly GetByIdTicketCase _getByIdTicketCase;
    private readonly GetAllTicketsCase _getAllTicketsCase;
    
    public TicketService(
        CreateTicketCase createTicketCase,
        UpdateTicketCase updateTicketCase,
        DeleteTicketCase deleteTicketCase,
        GetByIdTicketCase getByIdTicketCase,
        GetAllTicketsCase getAllTicketsCase
    )
    {
        _createTicketCase = createTicketCase;
        _updateTicketCase = updateTicketCase;
        _deleteTicketCase = deleteTicketCase;
        _getByIdTicketCase = getByIdTicketCase;
        _getAllTicketsCase = getAllTicketsCase;
    }
    public async Task<Result<TicketDto>> CreateAsync(CreateTicketDto createTicketDto)
    {
        var result = await _createTicketCase.ExecuteAsync(createTicketDto);
        return result;
    }
    
    /*
     public async Task<Result<TicketDto>> UpdateAsync(int id, UpdateTicketDto updateTicketDto)
    {
        var result = await _updateTicketCase.ExecuteAsync(id, updateTicketDto);
        return result;
    }
    */
    
    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var result = await _deleteTicketCase.ExecuteAsync(id);
        return result;
    }
    
    public async Task<Result<TicketDto>> GetByIdAsync(int id)
    {
        var result = await _getByIdTicketCase.ExecuteAsync(id);
        return result;
    }
    public async Task<Result<List<TicketDto>>> GetAllAsync()
    {
        var result = await _getAllTicketsCase.ExecuteAsync();
        return result;
    }
}
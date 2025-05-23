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
    private readonly ChangeStatusCase _changeStatusCase;
    private readonly ChangePriorityCase _changePriorityCase;
    private readonly MergeTicketsCase _mergeTicketsCase;
    private readonly UnmergeTicketCase _unmergeTicketCase;
    private readonly CloseTicketCase _closeTicketCase;
    private readonly ReopenTicketCase _reopenTicketCase;
    
    public TicketService(
        CreateTicketCase createTicketCase,
        UpdateTicketCase updateTicketCase,
        DeleteTicketCase deleteTicketCase,
        GetByIdTicketCase getByIdTicketCase,
        GetAllTicketsCase getAllTicketsCase,
        ChangeStatusCase changeStatusCase,
        ChangePriorityCase changePriorityCase,
        MergeTicketsCase mergeTicketsCase,
        UnmergeTicketCase unmergeTicketCase,
        CloseTicketCase closeTicketCase,
        ReopenTicketCase reopenTicketCase
    )
    {
        _createTicketCase = createTicketCase;
        _updateTicketCase = updateTicketCase;
        _deleteTicketCase = deleteTicketCase;
        _getByIdTicketCase = getByIdTicketCase;
        _getAllTicketsCase = getAllTicketsCase;
        _changeStatusCase = changeStatusCase;
        _changePriorityCase = changePriorityCase;
        _mergeTicketsCase = mergeTicketsCase;
        _unmergeTicketCase = unmergeTicketCase;
        _closeTicketCase = closeTicketCase;
        _reopenTicketCase = reopenTicketCase;
    }
    public async Task<Result<TicketDto>> CreateAsync(CreateTicketDto createTicketDto)
    {
        var result = await _createTicketCase.ExecuteAsync(createTicketDto);
        return result;
    }
    
    
     public async Task<Result<TicketDto>> UpdateAsync(int id, UpdateTicketDto updateTicketDto)
    {
        var result = await _updateTicketCase.ExecuteAsync(id, updateTicketDto);
        return result;
    }
    
    
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
    public async Task<Result<TicketDto>> ChangeStatusAsync(int id, ChangeStatusDto dto)
    {
        var result = await _changeStatusCase.ExecuteAsync(id, dto);
        return result;
    }
    public async Task<Result<TicketDto>> ChangePriorityAsync(int id, ChangePriorityDto dto)
    {
        var result = await _changePriorityCase.ExecuteAsync(id, dto);
        return result;
    }
    public async Task<Result<TicketDto>> MergeTicketsAsync(MergeTicketsDto dto)
    {
        var result = await _mergeTicketsCase.ExecuteAsync(dto);
        return result;
    }
    public async Task<Result<TicketDto>> UnmergeTicketAsync(UnmergeTicketDto dto)
    {
        var result = await _unmergeTicketCase.ExecuteAsync(dto);
        return result;
    }
    public async Task<Result<bool>> CloseTicketAsync(int id)
    {
        var result = await _closeTicketCase.ExecuteAsync(id);
        return result;
    }
    public async Task<Result<bool>> ReopenTicketAsync(int id)
    {
        var result = await _reopenTicketCase.ExecuteAsync(id);
        return result;
    }
}
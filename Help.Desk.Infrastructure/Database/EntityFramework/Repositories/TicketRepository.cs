using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Models;

namespace Help.Desk.Infrastructure.Database.EntityFramework.Repositories;

public class TicketRepository: ITicketRepository
{
    public Task<TicketModel> CreateAsync(TicketModel entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<TicketModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TicketModel> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TicketModel> UpdateAsync(TicketModel entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TicketModel> ChangeStatusAsync(int ticketId, int statusId)
    {
        throw new NotImplementedException();
    }

    public Task<TicketModel> ChangePriorityAsync(int ticketId, int priorityId)
    {
        throw new NotImplementedException();
    }

    public Task<TicketModel> MergeTicketsAsync(int ticketId, int ticketIdToMerge)
    {
        throw new NotImplementedException();
    }

    public Task<TicketModel> UnmergeTicketsAsync(int ticketId, int ticketIdToUnmerge)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CloseTicketAsync(int ticketId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ReopenTicketAsync(int ticketId)
    {
        throw new NotImplementedException();
    }
}
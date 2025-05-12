using Help.Desk.Domain.Dtos.TicketDtos;
using Help.Desk.Domain.IRepositories.Common;

namespace Help.Desk.Domain.IRepositories;

public interface ITicketRepository: IGenericRepository<TicketDto>
{
    Task<IList<TicketDto>> GetMergedTicketsAsync(int ticketId);
    Task<TicketDto> MergeTicketsAsync(int ticketId, int ticketIdToMerge);
    Task<TicketDto> UnmergeTicketsAsync(int ticketId, int ticketIdToUnmerge);
    
    /*
    Task<IEnumerable<TicketDto>> GetTicketsByRequesterIdAsync(int requesterId);
    Task<IEnumerable<TicketDto>> GetTicketsByAssignedAgentIdAsync(int assignedAgentId);
    Task<IEnumerable<TicketDto>> GetTicketsByAssignedGroupIdAsync(int assignedGroupId);
    Task<IEnumerable<TicketDto>> GetTicketsByStatusIdAsync(int statusId);
    Task<IEnumerable<TicketDto>> GetTicketsByPriorityIdAsync(int priorityId);
    Task<IEnumerable<TicketDto>> GetTicketsBySourceOriginIdAsync(int sourceOriginId);
    Task<ResumenForUserTicketDto> GetResumenForUserTicketAsync(int ticketId);
    Task<ResumenForAgentTicketDto> GetResumenForAgentTicketAsync(int ticketId);
    */
}

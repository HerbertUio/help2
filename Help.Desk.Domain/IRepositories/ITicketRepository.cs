using Help.Desk.Domain.Dtos.TicketDtos;
using Help.Desk.Domain.IRepositories.Common;

namespace Help.Desk.Domain.IRepositories;

public interface ITicketRepository: IGenericRepository<TicketDto>
{
    //TODO: VERIFICAR  LOS CASOS DE USO DE UN TICKET
    Task<IList<TicketDto>> GetMergedTicketsAsync(int ticketId);
    Task<TicketDto> MergeTicketsAsync(int ticketId, int ticketIdToMerge);
    Task<TicketDto> UnmergeTicketsAsync(int ticketId, int ticketIdToUnmerge);
    Task<bool> ReopenTicketAsync(int ticketId);
    Task<bool> CloseTicketAsync(int ticketId);
    Task<bool> ReassignTicketAsync(int ticketId, int newAssignedAgentId);
    Task<bool> ReassignTicketToGroupAsync(int ticketId, int newAssignedGroupId);
    Task<bool> ChangeTicketStatusAsync(int ticketId, int newStatusId);
    Task<bool> ChangeTicketPriorityAsync(int ticketId, int newPriorityId);
    Task<bool> ChangeTicketTypeAsync(int ticketId, int newTypeTicketId);
    
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

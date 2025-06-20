using Help.Desk.Domain.Enums.TicketEnums;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Domain.Models;
using Help.Desk.Infrastructure.Database.EntityFramework.Context;
using Help.Desk.Infrastructure.Database.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Help.Desk.Infrastructure.Database.EntityFramework.Repositories;

public class TicketRepository: ITicketRepository
{
    private readonly HelpDeskDbContext _context;
    private readonly DbSet<TicketEntity> _tickets;

    private static TicketModel ToModel(TicketEntity entity)
    {
        if (entity is null){return null;}

        return new TicketModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Priority = (Priority)entity.Priority,
            Status = (Status)entity.Status,
            Created = entity.Created,
            ExpirationDateSla = entity.ExpirationDateSla,
            ResolutionDate = entity.ResolutionDate,
            ClosedDate = entity.ClosedDate,
            LastUpdate = entity.LastUpdate,
            PrimaryTicketId = entity.PrimaryTicketId,
            ParentTicketId = entity.ParentTicketId,
            RequesterId = entity.RequesterId,
            AssignedAgentId = entity.AssignedAgentId,
            AssignedGroupId = entity.AssignedGroupId,
            TypeTicketId = entity.TypeTicketId,
            SourceOriginId = entity.SourceOriginId,
            OfficeId = entity.OfficeId,
            AreaId = entity.AreaId,
            SubjectId = entity.SubjectId
        };
    }

    private static TicketEntity ToEntity(TicketModel model)
    {
        if (model is null){return null;}
        return new TicketEntity
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            Priority = (int)model.Priority,
            Status = (int)model.Status,
            Created = model.Created,
            ExpirationDateSla = model.ExpirationDateSla,
            ResolutionDate = model.ResolutionDate,
            ClosedDate = model.ClosedDate,
            LastUpdate = model.LastUpdate,
            PrimaryTicketId = model.PrimaryTicketId,
            ParentTicketId = model.ParentTicketId,
            RequesterId = model.RequesterId,
            AssignedAgentId = model.AssignedAgentId,
            AssignedGroupId = model.AssignedGroupId,
            TypeTicketId = model.TypeTicketId,
            SourceOriginId = model.SourceOriginId,
            OfficeId = model.OfficeId,
            AreaId = model.AreaId,
            SubjectId = model.SubjectId
        };
    }
    
    
    public async Task<TicketModel> CreateAsync(TicketModel model)
    {
        var entity = ToEntity(model);
        await _context.Tickets.AddAsync(entity);
        await _context.SaveChangesAsync();
        return ToModel(entity);
    }

    public async Task<List<TicketModel>> GetAllAsync()
    {
        var entities = await _context.Tickets.AsNoTracking().ToListAsync();
        return entities.Select(ToModel).ToList();
    }

    public async Task<TicketModel> GetByIdAsync(int id)
    {
        var entity = await _context.Tickets.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        return ToModel(entity);
    }

    public async Task<TicketModel> UpdateAsync(TicketModel model)
    {
        var entityToUpdate = await _context.Tickets.FindAsync(model.Id);
        if (entityToUpdate is null)
        {
            throw new KeyNotFoundException($"Ticket with ID {model.Id} not found.");
        }
        entityToUpdate.Title = model.Title;
        entityToUpdate.Description = model.Description;
        entityToUpdate.Priority = (int)model.Priority;
        entityToUpdate.Status = (int)model.Status;
        entityToUpdate.ExpirationDateSla = model.ExpirationDateSla;
        entityToUpdate.ResolutionDate = model.ResolutionDate;
        entityToUpdate.ClosedDate = model.ClosedDate;
        entityToUpdate.LastUpdate = DateTime.Now;
        entityToUpdate.PrimaryTicketId = model.PrimaryTicketId;
        entityToUpdate.ParentTicketId = model.ParentTicketId;
        entityToUpdate.AssignedAgentId = model.AssignedAgentId;
        entityToUpdate.AssignedGroupId = model.AssignedGroupId;
        entityToUpdate.TypeTicketId = model.TypeTicketId;
        entityToUpdate.OfficeId = model.OfficeId;
        entityToUpdate.AreaId = model.AreaId;
        entityToUpdate.SubjectId = model.SubjectId;
        
        await _context.SaveChangesAsync();
        return ToModel(entityToUpdate);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entityToDelete = await _context.Tickets.FindAsync(id);
        if (entityToDelete is null)
        {
            return false;
        }
        _context.Tickets.Remove(entityToDelete);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<TicketModel> ChangeStatusAsync(int ticketId, int statusId)
    {
        var entity = await _context.Tickets.FindAsync(ticketId);
        if (entity is null) {return null;}
        
        entity.Status = statusId;
        entity.LastUpdate = DateTime.Now;
        
        await _context.SaveChangesAsync();
        return ToModel(entity);
    }

    public async Task<TicketModel> ChangePriorityAsync(int ticketId, int priorityId)
    {
        var entity = await _context.Tickets.FindAsync(ticketId);
        if (entity is null) {return null;}
        
        entity.Priority = priorityId;
        entity.LastUpdate = DateTime.Now;
        
        await _context.SaveChangesAsync();
        return ToModel(entity);
    }

    public async Task<TicketModel> MergeTicketsAsync(int ticketId, int ticketIdToMerge)
    {
        var primaryTicket = await _context.Tickets.FindAsync(ticketId);
        var ticketToMerge = await _context.Tickets.FindAsync(ticketIdToMerge);
        
        if (primaryTicket is null)
        {
            throw new KeyNotFoundException("El primer ticket no fue encontrado.");
        }
        if (ticketToMerge is null)
        {
            throw new KeyNotFoundException("El ticket a fusionar no fue encontrado.");
        }
        
        ticketToMerge.PrimaryTicketId = primaryTicket.PrimaryTicketId;
        ticketToMerge.Status = (int)Status.Cerrado;
        ticketToMerge.LastUpdate = DateTime.Now;
        ticketToMerge.ClosedDate = DateTime.Now;
        
        await _context.SaveChangesAsync();
        return ToModel(primaryTicket);
        
    }

    public async Task<TicketModel> UnmergeTicketsAsync(int ticketId, int ticketIdToUnmerge)
    {
        var ticketToUnmerge = await _context.Tickets.FindAsync(ticketIdToUnmerge);
        if (ticketToUnmerge == null || ticketToUnmerge.PrimaryTicketId != ticketId)
        {
            return null;
        }

        ticketToUnmerge.PrimaryTicketId = null;
        ticketToUnmerge.Status = (int)Status.Reabierto; 
        ticketToUnmerge.LastUpdate = DateTime.Now;
        ticketToUnmerge.ClosedDate = null;

        await _context.SaveChangesAsync();
        return ToModel(ticketToUnmerge);
    }

    public async Task<bool> CloseTicketAsync(int ticketId)
    {
        var entity = await _context.Tickets.FindAsync(ticketId);
        if (entity == null) return false;

        if (entity.Status == (int)Status.Cerrado) return true; 

        entity.Status = (int)Status.Cerrado;
        entity.LastUpdate = DateTime.Now;
        entity.ClosedDate = DateTime.Now;
        if (entity.ResolutionDate == null)
        {
            entity.ResolutionDate = DateTime.Now;
        }

        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> ReopenTicketAsync(int ticketId)
    {
        var entity = await _context.Tickets.FindAsync(ticketId);
        if (entity == null) return false;
        
        if (entity.Status != (int)Status.Cerrado && entity.Status != (int)Status.Resuelto) return false;

        entity.Status = (int)Status.Reabierto;
        entity.LastUpdate = DateTime.Now;
        entity.ClosedDate = null;
        entity.ResolutionDate = null;
        
        return await _context.SaveChangesAsync() > 0;
    }
}
using Help.Desk.Domain.Enums.TicketEnums;

namespace Help.Desk.Domain.Dtos.TicketDtos;

public class TicketDto
{
    public int Id { get; set; }
    public string Title { get; set; } 
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; } 
    public DateTime Created { get; set; }
    public DateTime?  ExpirationDateSla { get; set; }
    public DateTime? ResolutionDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public DateTime LastUpdate { get; set; }

    public int? PrimaryTicketId { get; set; }
    public int? ParentTicketId { get; set; }
    public int? RequesterId { get; set; } //Solicitante
    public int? AssignedAgentId { get; set; } 
    public int? AssignedGroupId { get; set; } 
    public int? TypeTicketId { get; set; }
    public int? SourceOriginId { get; set; } //Origen del ticket
    public int? OfficeId { get; set; }
    public int? AreaId { get; set; }
    public int? SubjectId { get; set; } //Asunto Relacionado con:


    public static TicketDto Create(int? requesterId, string title, string description, int? subjectId, int? officeId,
        int? areaId, int? typeTicketId)
    {
        // Antes de retornar el ticket, se podria disparar algun evento de dominio
        // deberiamos de disparar el evento que una vez creado el ticket este assigne este ticket a algun agente que este disponible
        return new TicketDto
        {
            Title = title,
            Description = description,
            Priority = Priority.Bajo,
            Status = Status.Abierto,
            Created = DateTime.Now, 
            ExpirationDateSla = null, 
            ResolutionDate = null, 
            ClosedDate = null, 
            LastUpdate = DateTime.Now, 
            PrimaryTicketId = null, 
            ParentTicketId = null,
            RequesterId = requesterId, 
            AssignedAgentId = null, 
            AssignedGroupId = null,
            TypeTicketId = typeTicketId, 
            SourceOriginId = null,
            OfficeId = officeId,
            AreaId = areaId,
            SubjectId = subjectId
        };
    }
    public void Update (string title, string description, int? priorityId, int? statusId, int? primaryTicketId, int? parentTicketId, int? assignedAgentId, int? assignedGroupId, int? typeTicketId, int? officeId, int? areaId, int? subjectId)
    {
        Title = title;
        Description = description;
        Priority = (Priority)(priorityId ?? (int)Priority.Bajo); 
        Status = (Status)(statusId ?? (int)Status.Abierto);
        PrimaryTicketId = primaryTicketId;
        ParentTicketId = parentTicketId;
        AssignedAgentId = assignedAgentId;
        AssignedGroupId = assignedGroupId;
        TypeTicketId = typeTicketId;
        OfficeId = officeId;
        AreaId = areaId;
        SubjectId = subjectId;
        LastUpdate = DateTime.Now;
        // Disparar algun evento
    }

    public void ChangePriority(Priority newPriority)
    {
        if (Status == Status.Cerrado) return;
        if (newPriority == null) return;
        if (Priority == newPriority) return;
        
        var previousPriority = Priority;
        Priority = newPriority;
        LastUpdate = DateTime.Now;
        // Disparar algun evento
    }

    public void ChangeStatus(Status newStatus)
    {
        if (Status == Status.Cerrado && newStatus != Status.Reabierto) return;
        if (newStatus == null) return;
        if (newStatus == Status) return;
        
        var previousStatus = Status;
        Status = newStatus;
        LastUpdate = DateTime.Now;
        
        if (newStatus == Status.Resuelto)
        {
            ResolutionDate = DateTime.Now;
            ClosedDate = null;
        }
        else if (newStatus == Status.Cerrado)
        {
            if (ResolutionDate == null) 
                ResolutionDate = DateTime.Now;
            
            ClosedDate = DateTime.Now;
        }
        else if (newStatus == Status.Reabierto)
        {
            ResolutionDate = null;
            ClosedDate = null;
        }
        else if (newStatus == Status.Pendiente)
        {
            ResolutionDate = null;
            ClosedDate = null;
        }
           // Disparar algun evento
    }

    public void Resolve()
    {
        if (Status == Status.Cerrado) return;
        if (Status == Status.Resuelto) return;
        var previousStatus = Status;
        Status = Status.Resuelto;
        ResolutionDate = DateTime.Now;
        ClosedDate = null;
        LastUpdate = DateTime.Now;
        // Disparar algun evento
    }
    public void Close()
    {
        if (Status == Status.Cerrado) return;
        if (Status == Status.Resuelto) return;
        var previousStatus = Status;
        Status = Status.Cerrado;
        ClosedDate = DateTime.Now;
        if (ResolutionDate == null) ResolutionDate = DateTime.Now;
        LastUpdate = DateTime.Now;
        // Disparar algun evento
    }
    public void Reopen()
    {
        if (Status != Status.Cerrado && Status != Status.Resuelto) return;
        var previousStatus = Status;
        Status = Status.Reabierto; 
        ResolutionDate = null;
        ClosedDate = null;
        LastUpdate = DateTime.Now;
        // Disparar algun evento
    }
    public bool AssignToAgent (int agentId)
    {
        if (AssignedAgentId == agentId) return false;
        if (Status == Status.Cerrado) return false;
        AssignedAgentId = agentId;
        LastUpdate = DateTime.Now;
        // Disparar algun evento
        return true;
    }

    public bool UnassignFromAgent(int agentId)
    {
        if (AssignedAgentId != agentId) return false;
        if (Status == Status.Cerrado) return false;
        AssignedAgentId = null;
        LastUpdate = DateTime.Now;
        // Disparar algun evento
        return true;
    }
    public bool AssignToGroup(int groupId)
    {
        if (AssignedGroupId == groupId) return false;
        if (Status == Status.Cerrado) return false;
        AssignedGroupId = groupId;
        LastUpdate = DateTime.Now;
        // Disparar algun evento
        return true;
    }
    public bool UnassignFromGroup(int groupId)
    {
        if (AssignedGroupId != groupId) return false;
        if (Status == Status.Cerrado) return false;
        AssignedGroupId = null;
        LastUpdate = DateTime.Now;
        // Disparar algun evento
        return true;
    }
    public bool SetParentTicket(int parentTicketId)
    {
        if (ParentTicketId == parentTicketId) return false;
        if (Status == Status.Cerrado) return false;
        ParentTicketId = parentTicketId;
        LastUpdate = DateTime.Now;
        // Disparar algun evento
        return true;
    }
    public bool UnsetParentTicket()
    {
        if (ParentTicketId == null) return false;
        if (Status == Status.Cerrado) return false;
        ParentTicketId = null;
        LastUpdate = DateTime.Now;
        // Disparar algun evento
        return true;
    }
    public bool SetPrimaryTicket(int primaryTicketId)
    {
        if (PrimaryTicketId == primaryTicketId) return false;
        if (Status == Status.Cerrado) return false;
        PrimaryTicketId = primaryTicketId;
        LastUpdate = DateTime.Now;
        // Disparar algun evento
        return true;
    }
    public bool UnsetPrimaryTicket()
    {
        if (PrimaryTicketId == null) return false;
        if (Status == Status.Cerrado) return false;
        PrimaryTicketId = null;
        LastUpdate = DateTime.Now;
        // Disparar algun evento
        return true;
    }
}
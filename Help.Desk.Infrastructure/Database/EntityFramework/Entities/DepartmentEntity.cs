using Help.Desk.Infrastructure.Database.EntityFramework.Entities.Common;

namespace Help.Desk.Infrastructure.Database.EntityFramework.Entities;

public class DepartmentEntity: BaseEntity, IIdentifiable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}
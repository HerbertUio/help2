using Help.Desk.Domain.Dtos.SupportGroupDtos;
using Help.Desk.Domain.IRepositories.Common;

namespace Help.Desk.Domain.IRepositories;

public interface ISupportGroupRepository: IGenericRepository<SupportGroupDto>
{
    Task<List<SupportGroupDto>> GetAllSecondLevelGroups();
}
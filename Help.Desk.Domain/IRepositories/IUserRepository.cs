using Help.Desk.Domain.Dtos.UserDtos;
using Help.Desk.Domain.IRepositories.Common;

namespace Help.Desk.Domain.IRepositories;

public interface IUserRepository: IGenericRepository<UserDto>
{
     Task<UserDto> GetByEmailAsync(string email);
}    
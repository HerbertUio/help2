using Help.Desk.Domain.Dtos.UserDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Infrastructure.Database.EntityFramework.Context;
using Help.Desk.Infrastructure.Database.EntityFramework.Entities;
using Help.Desk.Infrastructure.Database.EntityFramework.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Help.Desk.Infrastructure.Database.EntityFramework.Repositories;

public class UserRepository: IUserRepository
{
    private readonly HelpDeskDbContext _context;
    private readonly DbSet<UserEntity> _users;
    public UserRepository(HelpDeskDbContext context)
    {
        _context = context;
        _users = context.Set<UserEntity>();
    }
    public async Task<UserDto> CreateAsync(UserDto entity)
    {
        var userEntity = new UserEntity
        {
            Id = entity.Id,
            Name = entity.Name,
            LastName = entity.LastName,
            PhoneNumber = entity.PhoneNumber,
            Email = entity.Email,
            Password = entity.Password,
            DepartmentId = entity.DepartmentId,
            Role = entity.Role,
            Active = entity.Active,
            IsAgent = entity.IsAgent,
        };
        await _users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
        return new UserDto
        {
            Id = userEntity.Id,
            Name = userEntity.Name,
            LastName = userEntity.LastName,
            PhoneNumber = userEntity.PhoneNumber,
            Email = userEntity.Email,
            Password = userEntity.Password,
            DepartmentId = userEntity.DepartmentId,
            Role = userEntity.Role,
            Active = userEntity.Active,
            IsAgent = userEntity.IsAgent,
        };
        
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _users.ToListAsync();
        return users.Select(user => new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            Password = user.Password,
            DepartmentId = user.DepartmentId,
            Role = user.Role,
            Active = user.Active,
            IsAgent = user.IsAgent,
        }).ToList();
        
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _users.FindAsync(id);
        if (user == null)
        {
            return null;
        }
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            Password = user.Password,
            DepartmentId = user.DepartmentId,
            Role = user.Role,
            Active = user.Active,
            IsAgent = user.IsAgent,
        };
    }

    public async Task<UserDto> UpdateAsync(UserDto entity)
    {
        var userEntity = await _users.FindAsync(entity.Id);
        if (userEntity == null)
        {
            return null;
        }
        
        var userEntityToUpdate = new UserEntity
        {
            Id = entity.Id,
            Name = entity.Name,
            LastName = entity.LastName,
            PhoneNumber = entity.PhoneNumber,
            Email = entity.Email,
            Password = entity.Password,
            DepartmentId = entity.DepartmentId,
            Role = entity.Role,
            Active = entity.Active,
            IsAgent = entity.IsAgent,
        };
        userEntity.Name = userEntityToUpdate.Name;
        userEntity.LastName = userEntityToUpdate.LastName;
        userEntity.PhoneNumber = userEntityToUpdate.PhoneNumber;
        userEntity.Email = userEntityToUpdate.Email;
        userEntity.Password = userEntityToUpdate.Password;
        userEntity.DepartmentId = userEntityToUpdate.DepartmentId;
        userEntity.Role = userEntityToUpdate.Role;
        userEntity.Active = userEntityToUpdate.Active;
        userEntity.IsAgent = userEntityToUpdate.IsAgent;
        
        

        _context.Entry(userEntity).State = EntityState.Modified;
         var changes = await _context.SaveChangesAsync();
         if (changes == 0)
         {
             throw new Exception($"No se pudo actualizar el usuario {userEntity.Id}");       
         }
         
        return new UserDto
        {
            Id = userEntity.Id,
            Name = userEntity.Name,
            LastName = userEntity.LastName,
            PhoneNumber = userEntity.PhoneNumber,
            Email = userEntity.Email,
            Password = userEntity.Password,
            DepartmentId = userEntity.DepartmentId,
            Role = userEntity.Role,
            Active = userEntity.Active,
            IsAgent = userEntity.IsAgent,
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var userEntity = await _users.FindAsync(id);
        if (userEntity == null)
        {
            return false;
        }
        _users.Remove(userEntity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<UserDto> GetByEmailAsync(string email)
    {
        var user = await _users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            return null;
        }
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            Password = user.Password,
            DepartmentId = user.DepartmentId,
            Role = user.Role,
            Active = user.Active,
            IsAgent = user.IsAgent,
        };
    }
}
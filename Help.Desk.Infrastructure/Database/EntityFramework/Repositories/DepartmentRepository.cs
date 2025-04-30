using Help.Desk.Domain.Dtos.DepartmentDtos;
using Help.Desk.Domain.Dtos.UserDtos;
using Help.Desk.Domain.IRepositories;
using Help.Desk.Infrastructure.Database.EntityFramework.Context;
using Help.Desk.Infrastructure.Database.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Help.Desk.Infrastructure.Database.EntityFramework.Repositories;

public class DepartmentRepository: IDepartmentRepository
{
    private readonly HelpDeskDbContext _context;
    private readonly DbSet<DepartmentEntity> _departments;
    public DepartmentRepository(HelpDeskDbContext context)
    {
        _context = context;
        _departments = context.Set<DepartmentEntity>();
    }
    public async Task<DepartmentDto> CreateAsync(DepartmentDto entity)
    {
        var departmentEntity = new DepartmentEntity
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
        };
        await _context.AddAsync(departmentEntity);
        await _context.SaveChangesAsync();
        
        return new DepartmentDto
        {
            Id = departmentEntity.Id,
            Name = departmentEntity.Name,
            Description = departmentEntity.Description,
        };
    }

    public async Task<List<DepartmentDto>> GetAllAsync()
    {
        var departments = await _departments.ToListAsync();
        return departments.Select(department => new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
        }).ToList();
    }

    public async Task<DepartmentDto> GetByIdAsync(int id)
    {
        var department = await _departments.FindAsync(id);
        if (department == null)
        {
            return null;
        }
        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
        };
    }

    public async Task<DepartmentDto> UpdateAsync(DepartmentDto entity)
    {
        var departmentEntity = await _departments.FindAsync(entity.Id);
        if (departmentEntity == null)
        {
            return null;
        }
        
        var entityToUpdate = new DepartmentEntity
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
        };
        departmentEntity.Name = entity.Name;
        departmentEntity.Description = entity.Description;
        _context.Entry(entityToUpdate).State = EntityState.Modified;
        var changes =  _context.SaveChanges();
        if (changes == 0)
        {
            throw new Exception($"No se pudo actualizar el departamento {departmentEntity.Id}");
        }
        return new DepartmentDto
        {
            Id = departmentEntity.Id,
            Name = departmentEntity.Name,
            Description = departmentEntity.Description,
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var departmentEntity = await _departments.FindAsync(id);
        if (departmentEntity == null)
        {
            return false;
        }
        _context.Remove(departmentEntity);
        var changes = await _context.SaveChangesAsync();
        if (changes == 0)
        {
            throw new Exception($"No se pudo eliminar el departamento {departmentEntity.Id}");
        }
        return true;
    }

    public async Task<DepartmentDto> GetByNameAsync(string name)
    {
        var department = await _departments.FirstOrDefaultAsync(d => d.Name == name);
        if (department == null)
        {
            return null;
        }
        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
        };
    }

    public Task<List<UserDto>> GetUsersByDepartmentIdAsync(int departmentId)
    {
        var users = _context.Users
            .Where(u => u.DepartmentId == departmentId)
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                Email = u.Email,
                Password = u.Password,
                DepartmentId = u.DepartmentId,
                Role = u.Role,
                Active = u.Active,
                IsAgent = u.IsAgent,
            }).ToListAsync();
        return users;
    }
}
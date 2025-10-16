using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileCore.Domain.IRepository;
using ProfileCore.Domain.Aggregate;
using ProfileCore.Infrastructure.Database.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ProfileCore.Infrastructure.Database.Repository
{
    public class EmployeePostgreRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeePostgreRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddAsync(Employee employee)
        {
            var entity = _mapper.Map<EmployeeEntity>(employee);
            await _context.Employees.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(entity != null)
            {
                _context.Employees.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeeAsync()
        {
            IEnumerable<EmployeeEntity> entities = await _context.Employees.ToListAsync();
            return _mapper.Map<IEnumerable<Employee>>(entities) ?? Enumerable.Empty<Employee>();
        }

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<Employee>(entity);
        }

        public async Task<Employee?> GetByMailAsync(string mail)
        {
            var entity = await _context.Employees.FirstOrDefaultAsync(x => x.User.Email == mail);
            return _mapper.Map<Employee>(entity);
        }

        public async Task UpdateAsync(Employee employee)
        {
            var entity = await _context.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);
            if(entity != null)
            {
                _mapper.Map(employee,entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

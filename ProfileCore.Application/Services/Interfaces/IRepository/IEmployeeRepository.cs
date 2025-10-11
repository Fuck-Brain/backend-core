using ProfileCore.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileCore.Domain.Aggregate;

namespace ProfileCore.Application.Services.Interfaces.IRepository
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(Guid id);
        Task<Employee?> GetByMailAsync(string mail);
        Task<IEnumerable<Employee>> GetAllEmployeeAsync();
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Guid id);
    }
}

using ProfileCore.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileCore.Domain.IRepository
{
    public interface IPluginRepository
    {
        Task<Plugin?> GetByIdAsync(Guid id);
        Task<IEnumerable<Plugin>> GetAllPlugin();
        Task AddAsync(Plugin plugin);
        Task UpdateAsync(Plugin plugin);
        Task DeleteAsync(Guid id);
    }
}

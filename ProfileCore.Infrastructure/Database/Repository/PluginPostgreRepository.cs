using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProfileCore.Infrastructure.Database.Entities;
using ProfileCore.Domain;
using ProfileCore.Domain.Entity;
using ProfileCore.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
namespace ProfileCore.Infrastructure.Database.Repository
{
    public class PluginPostgreRepository : IPluginRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PluginPostgreRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddAsync(Plugin plugin)
        {
            var entity = _mapper.Map<PluginEntity>(plugin);
            await _context.Plugins.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Plugins.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                _context.Plugins.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Plugin>> GetAllPlugin()
        {
            IEnumerable<PluginEntity> entities = await _context.Plugins.ToListAsync();
            return _mapper.Map<IEnumerable<Plugin>>(entities) ?? Enumerable.Empty<Plugin>();
        }

        public async Task<Plugin?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Plugins.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<Plugin>(entity);
        }

        public async Task UpdateAsync(Plugin plugin)
        {
            var entity = await _context.Plugins.FirstOrDefaultAsync(x => x.Id == plugin.Id);
            if (entity != null)
            {
                _mapper.Map(plugin, entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

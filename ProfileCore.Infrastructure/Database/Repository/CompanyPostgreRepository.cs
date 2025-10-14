using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProfileCore.Domain.Aggregate;
using ProfileCore.Domain.IRepository;
using ProfileCore.Infrastructure.Database.Entities;
using ProfileCore.Infrastructure.Database.Repository;

namespace ProfileCore.Infrastructure.Database.Repository
{
    public class CompanyPostgreRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CompanyPostgreRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Company company)
        {
            var entity = _mapper.Map<CompanyEntity>(company);
            await _context.Companies.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);
            if(entity != null)
            {
                var owner = entity.Owner;
                var employees = entity.Employees;
                _context.Companies.Remove(entity);
                _context.Employees.Remove(owner);
                _context.Employees.RemoveRange(employees);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Company>> GetAllCompanyAsync()
        {
            IEnumerable<CompanyEntity> entities = await _context.Companies.ToListAsync();
            return _mapper.Map<IEnumerable<Company>>(entities) ?? Enumerable.Empty<Company>();
        }

        public async Task<Company?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<Company>(entity);
        }

        public async Task UpdateAsync(Company company)
        {
            var entity = await _context.Companies.FirstOrDefaultAsync(x => x.Id == company.Id);
            if(entity != null)
            {
                _mapper.Map(company,entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

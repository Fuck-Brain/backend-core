using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileCore.Domain.IRepository;
using ProfileCore.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata;
using AutoMapper;
using ProfileCore.Infrastructure.Database.Entities;




namespace ProfileCore.Infrastructure.Database.Repository
{
    public class UserPostgreRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserPostgreRepository(ApplicationDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task AddAsync(User user)
        {
            UserEntity entity = _mapper.Map<UserEntity>(user);
            await _dbContext.Users.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            UserEntity? entity = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if(entity != null)
            {
                _dbContext.Users.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            IEnumerable<UserEntity> entities = await _dbContext.Users.ToListAsync();
            return _mapper.Map<IEnumerable<User>>(entities) ?? Enumerable.Empty<User>();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            UserEntity? entity = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            return _mapper.Map<User>(entity);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<User>(entity);
        }

        public async Task UpdateAsync(User user)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (entity != null)
            {
                _mapper.Map(user, entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}

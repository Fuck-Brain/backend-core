using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileCore.Infrastructure.Database.Entities
{
    internal class EmployeeEntity : IAuditable
    {
        public Guid Id { get; set; }
        public UserEntity UserEntity { get; set; }
        public bool IsOwner { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public EmployeeEntity(UserEntity user, bool isOwner = false, bool isAdmin = false)
        {
            Id = new Guid();
            UserEntity = user;
            IsOwner = isOwner;
            IsAdmin = isAdmin;
        }
    }
}

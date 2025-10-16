using ProfileCore.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileCore.Domain.Aggregate
{
    public class Employee
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public bool IsOwner { get; set; }
        public bool IsAdmin { get; set; }
		
		public Employee() {}

        public Employee(User user, bool isOwner = false, bool isAdmin = false)
        {
            Id = new Guid();
            User = user;
            IsOwner = isOwner;
            IsAdmin = isAdmin;
        }
    }
}

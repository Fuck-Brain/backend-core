using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileCore.Domain.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = String.Empty;
        public string HashPassword { get; set; } = String.Empty;
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string FatherName { get; set; } = String.Empty;

        public User(string email, string hashPassword, string firstName, string lastName, string fatherName)
        {
            this.Id = Guid.NewGuid();
            this.Email = email;
            this.HashPassword = hashPassword;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FatherName = fatherName;
        }
		
		public User() {}
    }
}

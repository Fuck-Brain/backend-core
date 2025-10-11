using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileCore.Infrastructure.Database.Entities
{
    internal class UserEntity : IAuditable
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        UserEntity(string email, string hashPassword, string firstName, string lastName, string fatherName)
        {
            this.Id = new Guid();
            this.Email = email;
            this.HashPassword = hashPassword;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FatherName = fatherName;


            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileCore.Domain.DomainAbstract;

namespace ProfileCore.Domain.Entity
{
    public class User : BaseEntity, IAuditable
    {
        public string Login { get; private set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        
        public UserProfile Profile { get; private set; }
        public List<Employee> UserWorks { get; private set; }

        private User() {}
        private User(Guid id, string login, string email, string hashPassword, UserProfile profile, List<Employee> userWorks)
        {
            this.Id = Guid.NewGuid();
            this.Login = login;
            this.Email = email;
            this.HashPassword = hashPassword;
            this.Profile = profile;
            UserWorks = userWorks;
        }

        public static User Create(string login, string email, string hashPassword)
        {
            if (String.IsNullOrWhiteSpace(login)) 
                throw new ArgumentException("Login cannot be null or whitespace");
            if (String.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or whitespace");
            
            var userId = Guid.NewGuid();
            var userProfile = UserProfile.Create(userId, login);
            
            return new User(
                userId,
                login,
                email,
                hashPassword,
                userProfile,
                new List<Employee>());
        }

        public void ChangeLogin(string login)
        {
            if (String.IsNullOrWhiteSpace(login))
                throw new  ArgumentException("Login cannot be null or whitespace");
            
            this.Login = login;    
        }

        public void ChangeEmail(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
                throw new  ArgumentException("Email cannot be null or whitespace");
            
            this.Email = email;
        }
        
        public void ChangeHashPassword(string hashPassword) => HashPassword = hashPassword;
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

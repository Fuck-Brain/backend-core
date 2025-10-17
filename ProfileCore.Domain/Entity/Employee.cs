using ProfileCore.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileCore.Domain.Aggregate;
using ProfileCore.Domain.DomainAbstract;

namespace ProfileCore.Domain.Entity
{
    public enum EmployeeRole
    {
        Owner = 1,
        Worker = 2
    }
    public class Employee : BaseEntity, IAuditable
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid CompanyId { get; private set; }
        public EmployeeRole Role { get; private set; }
        
        public User User { get; private set; }
        public Company Company { get; private set; }
		
        private Employee() { }
        private Employee(Guid id, User user, Company company, EmployeeRole role)
        {
            Id = id;
            User = user;
            Company = company;
            Role = role;
        }
        private Employee(Guid id, Guid user, Guid company, EmployeeRole role)
        {
            Id = id;
            UserId = user;
            CompanyId = company;
            Role = role;
        }

        public static Employee Create(User user, Company company, EmployeeRole role)
        {
            return new Employee(
                Guid.NewGuid(),
                user,
                company,
                role
            );
        }

        /*public Employee Build(Guid id, User user, EmployeeRole role)
        {
            return new Employee(
                id,
                user,
                role
            );
        }*/

        public void PromoteToOwner()
        {
            if (Role == EmployeeRole.Owner) 
                throw new InvalidOperationException($"User {UserId} is already an owner.");
            Role = EmployeeRole.Owner;
        }

        public void DemoteToWorker()
        {
            if (Role == EmployeeRole.Worker)
                throw new InvalidOperationException($"User {UserId} is already a worker.");
            Role = EmployeeRole.Worker;
        }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

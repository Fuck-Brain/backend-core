using ProfileCore.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileCore.Domain.DomainAbstract;

namespace ProfileCore.Domain.Aggregate
{
    public class Company : BaseEntity, IAuditable, IAggregateRoot
    {
        public string Name { get; private set; }
        public Employee Owner { get; private set; }
        public List<Employee> Employees { get; private set; }
        public List<PluginConnection> PluginConnections { get; private set; }
		
        private Company() { }
		private Company(Guid id, string name, Employee owner, List<Employee> employees, List<PluginConnection> pluginConnections)
		{
			Id = id;
			Name = name;
			Owner = owner;
			Employees = employees;
			PluginConnections = pluginConnections;
		}

		public static Company Create(string name, Employee owner)
		{
			if (String.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("Name cannot be null or whitespace.");
			}

			return new Company(
				Guid.NewGuid(),
				name,
				owner,
				new List<Employee> { owner },
				new List<PluginConnection>());
		}

		/*public Company Build(Guid id, string name, Employee owner, List<Plugin> plugins, List<Employee> employees)
		{
			
		}*/

		public void ChangeOwner(Employee newOwner)
		{
			if (Employees.All(e => e.Id != newOwner.Id))
				throw new ArgumentException("newOwner must belong to company.");
			if (Owner.Id == newOwner.Id)
				throw new ArgumentException("User is already owner.");
			
			Owner.DemoteToWorker();
			Owner = newOwner;
			Owner.PromoteToOwner();
		}

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

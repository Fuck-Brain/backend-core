using ProfileCore.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileCore.Domain.Aggregate
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Employee Owner { get; set; }
        public List<Plugin> Plugins { get; set; }
        public List<Employee> Employees { get; set; }

		public Company(Guid id, string name, Employee owner, List<Plugin> plugins, List<Employee> employees)
		{
			Id = id;
			Name = name;
			Owner = owner;
			Plugins = plugins;
			Employees = employees;
		}
		
        public Company(string name, Employee owner)
        {
            Id = new Guid();
            Name = name;
            Owner = owner;
            Plugins = new List<Plugin>();
            Employees = new List<Employee>();
        }

    }
}

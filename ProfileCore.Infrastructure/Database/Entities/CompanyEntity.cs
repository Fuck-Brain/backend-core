using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileCore.Infrastructure.Database.Entities
{
    public class CompanyEntity : IAuditable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public EmployeeEntity Owner { get; set; }
        public List<PluginEntity> Plugins { get; set; }
        public List<EmployeeEntity> Employees { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt {  get; set; }

        public CompanyEntity(string name, EmployeeEntity owner)
        {
            Id = new Guid();
            Name = name;
            Owner = owner;
            Plugins = new List<PluginEntity>();
            Employees = new List<EmployeeEntity>();
        }

    }
}

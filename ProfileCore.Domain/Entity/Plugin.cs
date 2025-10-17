using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileCore.Domain.Aggregate;
using ProfileCore.Domain.DomainAbstract;

namespace ProfileCore.Domain.Entity
{
    public class Plugin : BaseEntity, IAuditable
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public List<PluginConnection> Connections { get; private set; }
        // Something else, that idn yet

        private Plugin() { }
        private Plugin(Guid id, string name, string? description, List<PluginConnection> connections)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Connections = connections;
        }

        public static Plugin Create(string name, string? description)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or whitespace.");
            return new Plugin(
                Guid.NewGuid(),
                name,
                description,
                new List<PluginConnection>()
            );
        }

        public void Rename(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or whitespace.");
            if (this.Name == name)
                throw new ArgumentException("Name cannot be the same as the old name.");
            
            this.Name = name;
        }

        public void ChangeDescription(string description)
        {
            if (String.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be null or whitespace.");
            if (this.Description == description)
                throw new ArgumentException("Description cannot be the same as the old description.");
            
            this.Description = description;
        }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

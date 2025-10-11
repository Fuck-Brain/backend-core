using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileCore.Infrastructure.Database.Entities
{
    internal class PluginEntity : IAuditable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PeriodСost {  get; set; }
        public double Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public PluginEntity(Guid id, string name, string description, string periodСost, double rating)
        {
            this.Id = new Guid();
            this.Name = name;
            this.Description = description;
            this.PeriodСost = periodСost;
            this.Rating = rating;
        }
    }
}

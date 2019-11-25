using Chilicki.Commline.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chilicki.Commline.Domain.Entities
{
    public class Line
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsCircular { get; set; }
        public virtual LineType LineType { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
    }
}

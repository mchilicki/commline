using Chilicki.Commline.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.DTOs
{
    public class DepartureDTO
    {
        public long Id { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public virtual DayType DayType { get; set; }
    }
}

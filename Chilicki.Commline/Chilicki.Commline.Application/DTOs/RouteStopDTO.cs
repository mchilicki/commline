using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.DTOs
{
    public class RouteStopDTO
    {
        public long Id { get; set; }
        public int StopIndex { get; set; }
        public virtual IEnumerable<DepartureDTO> Departures { get; set; }
        public virtual StopDTO Stop { get; set; }
    }
}

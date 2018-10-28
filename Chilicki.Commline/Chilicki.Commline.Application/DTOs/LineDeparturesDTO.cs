using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.DTOs
{
    public class LineDeparturesDTO
    {
        public LineDTO Line { get; set; }
        public IEnumerable<RouteStopDTO> RouteStops { get; set; }
    }
}

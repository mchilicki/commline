using System;
using System.Collections.Generic;

namespace Chilicki.Commline.Application.DTOs
{
    public class RouteStopDTO
    {
        public Guid Id { get; set; }
        public bool IsReturnStop { get; set; }
        public int StopIndex { get; set; }
        public virtual IEnumerable<DepartureDTO> Departures { get; set; }
        public virtual StopDTO Stop { get; set; }
    }
}

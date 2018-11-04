using System.Collections.Generic;

namespace Chilicki.Commline.Application.DTOs
{
    public class LineDeparturesDTO
    {
        public LineDTO Line { get; set; }
        public IEnumerable<IEnumerable<DepartureDTO>> Departures { get; set; }

        public LineDTO ReturnLine { get; set; }
        public IEnumerable<IEnumerable<DepartureDTO>> ReturnDepartures { get; set; }
    }
}

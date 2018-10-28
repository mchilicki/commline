using System.Collections.Generic;

namespace Chilicki.Commline.Application.DTOs
{
    public class LineDeparturesDTO
    {
        public LineDTO Line { get; set; }
        public IEnumerable<IEnumerable<DepartureDTO>> Departures { get; set; }
    }
}

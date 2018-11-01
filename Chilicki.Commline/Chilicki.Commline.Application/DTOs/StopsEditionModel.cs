using System.Collections.Generic;

namespace Chilicki.Commline.Application.DTOs
{
    public class StopsEditionModel
    {
        public IEnumerable<StopDTO> Added { get; set; }
        public IEnumerable<StopDTO> Modified { get; set; }
        public IEnumerable<StopDTO> Deleted { get; set; }
    }
}

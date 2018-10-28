using System.Collections.Generic;

namespace Chilicki.Commline.Application.DTOs
{
    public class LinesEditionModel
    {
        public IEnumerable<LineDTO> Added { get; set; }
        public IEnumerable<LineDTO> Modified { get; set; }
        public IEnumerable<LineDTO> Deleted { get; set; }
    }
}

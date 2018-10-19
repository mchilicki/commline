using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.DTOs
{
    public class StopsEditionModel
    {
        public IEnumerable<StopDTO> Added { get; set; }
        public IEnumerable<StopDTO> Modified { get; set; }
        public IEnumerable<StopDTO> Deleted { get; set; }
    }
}

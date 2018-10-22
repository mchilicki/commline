using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.DTOs
{
    public class LinesEditionModel
    {
        public IEnumerable<LineDTO> Added { get; set; }
        public IEnumerable<LineDTO> Modified { get; set; }
        public IEnumerable<LineDTO> Deleted { get; set; }
    }
}

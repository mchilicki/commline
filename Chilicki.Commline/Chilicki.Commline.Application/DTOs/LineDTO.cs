using Chilicki.Commline.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.DTOs
{
    public class LineDTO
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public LineType LineType { get; set; }
        public IEnumerable<StopDTO> Stops { get; set; }
    }
}

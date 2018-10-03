using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.DTOs
{
    public class AllLinesDTO
    {
        IEnumerable<LineDTO> Lines { get; set; }
    }
}

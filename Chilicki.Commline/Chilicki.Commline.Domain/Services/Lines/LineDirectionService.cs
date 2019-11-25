using Chilicki.Commline.Domain.Entities;
using System.Linq;

namespace Chilicki.Commline.Domain.Services.Lines
{
    public class LineDirectionService
    {
        public Stop GetDirectionStop(Line line)
        {
            return line.Trips.Last().Stop;
        }
    }
}

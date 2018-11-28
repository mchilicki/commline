using Chilicki.Commline.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.Correctors
{
    public class LineCorrector
    {
        public LineDTO CorrectLine(LineDTO line)
        {
            StopDTO previousStop = null;
            var stopIndexesToDelete = new List<int>();
            int stopIndex = 0;
            foreach (var stop in line.Stops)
            {
                if (previousStop != null && stop.Id == previousStop.Id)
                {                     
                    stopIndexesToDelete.Add(stopIndex);                                        
                }
                previousStop = stop;
                stopIndex++;
            }
            stopIndexesToDelete.Reverse();
            var tempStops = line.Stops.ToList();
            foreach (int stopIndexToDelete in stopIndexesToDelete)
            {
                tempStops.RemoveAt(stopIndexToDelete);
            }
            line.Stops = tempStops;
            return line;
        }
    }
}

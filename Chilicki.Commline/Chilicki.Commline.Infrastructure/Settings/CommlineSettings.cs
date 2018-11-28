using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Infrastructure.Settings
{
    public class CommlineSettings
    {
        // Google Maps Settings
        public double MapCenterPointLatitude { get; set; } = 50.120965;
        public double MapCenterPointLongitude { get; set; } = 19.015120;
        public int StartZoom { get; set; } = 13;
        public int MinimumZoomStopsAppear { get; set; } = 12;

        // Departures Settings
        public int CopyTimesOption1 { get; set; } = 15;
        public int CopyTimesOption2 { get; set; } = 30;
        public int CopyTimesOption3 { get; set; } = 60;
    }
}

using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Enums;

namespace Chilicki.Commline.Domain.Factories
{
    public class StopFactory
    {
        public Stop FillIn(Stop stop, string name, double latitude, 
            double longitude, StopType stopType, int stationNumber)
        {
            stop.Name = name;
            stop.Latitude = latitude;
            stop.Longitude = longitude;
            stop.StopType = stopType;
            stop.StopNumber = stationNumber;
            return stop;
        }
    }
}

using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Databases;
using Chilicki.Commline.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Infrastructure.Repositories
{
    public class StopRepository : BaseRepository<Stop>
    {
        public StopRepository(CommlineDBContext db) : base(db)
        {
        }

        public bool DoesStopWithIdExist(long id)
        {
            return _entities
                .Where(p => p.Id == id)
                .Count() == 1;
        }

        public IEnumerable<Stop> GetAllConnectedToAnyLine()
        {
            return _entities
                .Where(e => e.RouteStops.Any())
                .ToList();
        }

        public IEnumerable<Stop> GetAllNotConnectedToAnyLine()
        {
            return _entities
                .Where(e => !e.RouteStops.Any())
                .ToList();
        }

        public int GetNextStopNumberForStopName(string stopName)
        {
            var stopNumbersForStopName = _entities
                .Where(e => e.Name == stopName)
                .Select(p => p.StopNumber);
            int currentStopNumber = 1;
            foreach (int stopNumber in stopNumbersForStopName)
            {
                if (stopNumber != currentStopNumber)
                    return currentStopNumber;
                currentStopNumber++;
            }
            return currentStopNumber;
        }             
        
        public bool IsStopConnectedToAnyLine(long id)
        {
            return _entities
                .Find(id)
                .RouteStops
                .Count > 0;
        }
    }
}

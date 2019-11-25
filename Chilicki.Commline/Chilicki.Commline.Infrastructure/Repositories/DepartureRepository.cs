using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Databases;
using Chilicki.Commline.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Infrastructure.Repositories
{
    public class DepartureRepository : BaseRepository<Departure>
    {
        public DepartureRepository(CommlineDBContext db) : base(db)
        {

        }    
        
        public void Create(Line line, IEnumerable<IEnumerable<Departure>> departures)
        {
            if (departures != null)
            {
                departures = departures
                    .OrderBy(p => p.First().DepartureTime);
                int runIndex = 0;
                foreach (var departureRun in departures)
                {
                    int stopIndex = 0;
                    foreach (var departure in departureRun)
                    {
                        departure.RunIndex = runIndex;
                        departure.RouteStop = line.Trips
                            .First(p => p.StopIndex == stopIndex);
                        _entities.Add(departure);
                        stopIndex++;
                    }
                    runIndex++;
                }
                _database.SaveChanges();
            }            
        }

        public void DeleteAllDeparturesForLine(Line line)
        {
            var departuresToRemove = _entities
                .Where(e => e.RouteStop.Line.Id == line.Id);
            if (departuresToRemove != null && departuresToRemove.Count() > 0)
                _entities.RemoveRange(departuresToRemove);
            _database.SaveChanges();
        }

        public IEnumerable<IEnumerable<Departure>> 
            GetAllLineDeparturesOrderedByRuns(long lineId)
        {
            var departures = new List<List<Departure>>();
            var runIndexes = GetLineRunIndexes(lineId);
            foreach (int runIndex in runIndexes)
            {
                departures.Add(_entities
                    .Where(p => 
                        p.RouteStop.Line.Id == lineId && 
                        p.RunIndex == runIndex)
                    .OrderBy(p => p.RouteStop.StopIndex)
                    .ToList());
            }
            return departures;            
        }

        private IEnumerable<int> GetLineRunIndexes(long lineId)
        {
            return _entities
                .Where(p => p.RouteStop.Line.Id == lineId)
                .Select(p => p.RunIndex)
                .Distinct();
        }
    }
}

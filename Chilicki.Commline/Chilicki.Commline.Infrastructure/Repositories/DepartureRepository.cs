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

        public IEnumerable<IEnumerable<Departure>> 
            GetAllLineDeparturesOrderedByRuns(long lineId)
        {
            var departures = new List<List<Departure>>();
            var runIndexes = GetLineRunIndexes(lineId);
            foreach (int runIndex in runIndexes)
            {
                departures.Add(_dbSet
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
            return _dbSet
                .Select(p => p.RunIndex)
                .Distinct();
        }
    }
}

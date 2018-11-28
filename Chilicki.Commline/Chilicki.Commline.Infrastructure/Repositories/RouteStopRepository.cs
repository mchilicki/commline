using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Databases;
using Chilicki.Commline.Infrastructure.Repositories.Base;
using System.Collections.Generic;

namespace Chilicki.Commline.Infrastructure.Repositories
{
    public class RouteStopRepository : BaseRepository<RouteStop>
    {
        readonly LineRepository _lineRepository;
        readonly StopRepository _stopRepository;

        public RouteStopRepository(CommlineDBContext db, 
            LineRepository lineRepository, StopRepository stopRepository) : base(db)
        {
            _lineRepository = lineRepository;
            _stopRepository = stopRepository;
        }

        public void InsertForLineAndStops(Line line, IEnumerable<Stop> stops)
        {
            int stopIndex = 0;
            var entityLine = _lineRepository.Find(line.Id);
            foreach (var stop in stops)
            {
                var entityStop = _stopRepository.Find(stop.Id);
                _entities.Add(new RouteStop()
                {
                    Line = entityLine,
                    Stop = entityStop,
                    StopIndex = stopIndex,
                });
                stopIndex++;
            }
            _database.SaveChanges();
        }
    }
}

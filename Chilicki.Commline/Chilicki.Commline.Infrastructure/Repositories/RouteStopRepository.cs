using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Databases;
using Chilicki.Commline.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<RouteStop> GetAllForLine(long lineId)
        {
            return GetAll()
                .Where(r => r.Line != null & r.Line.Id == lineId)
                .OrderBy(r => r.StopIndex)
                .ToList();
        }

        public IEnumerable<RouteStop> GetAllForLine(Line line)
        {
            return GetAllForLine(line.Id);
        }

        public void InsertForLineAndStops(Line line, IEnumerable<Stop> stops)
        {
            //if (!_lineRepository.DoesLineHaveEmptyRoute(line.Id))
            //    throw new InvalidOperationException($"{DatabaseResources.Exception_LineWithId} " +
            //        $"{line.Id} {DatabaseResources.Exception_lineHasStops}");
            int stopIndex = 1;
            var entityLine = _lineRepository.GetById(line.Id);
            foreach (var stop in stops)
            {
                var entityStop = _stopRepository.GetById(stop.Id);
                _dbSet.Add(new RouteStop()
                {
                    Line = entityLine,
                    Stop = entityStop,
                    StopIndex = stopIndex,
                });
                stopIndex++;
            }
            _db.SaveChanges();
        }
    }
}

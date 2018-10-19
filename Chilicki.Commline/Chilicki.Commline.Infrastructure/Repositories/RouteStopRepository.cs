using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Databases;
using Chilicki.Commline.Infrastructure.Repositories.Base;
using Chilicki.Commline.Infrastructure.Resources;
using System;
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
            if (!_lineRepository.DoesLineHaveEmptyRoute(line.Id))
                throw new InvalidOperationException($"{DatabaseResources.Exception_LineWithId} " +
                    $"{line.Id} {DatabaseResources.Exception_lineHasStops}");
            int stopIndex = 1;
            foreach (var stop in stops)
            {
                _dbSet.Add(new RouteStop()
                {
                    Line = line,
                    Stop = stop,
                    StopIndex = stopIndex,
                });
                stopIndex++;
            }
            _db.SaveChanges();
        }
    }
}

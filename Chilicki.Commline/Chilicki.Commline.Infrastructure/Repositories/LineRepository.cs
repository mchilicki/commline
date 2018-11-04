using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Databases;
using Chilicki.Commline.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Infrastructure.Repositories
{
    public class LineRepository : BaseRepository<Line>
    {
        readonly StopRepository _stopRepository;

        public LineRepository(CommlineDBContext db, StopRepository stopRepository) : base(db)
        {
            _stopRepository = stopRepository;
        }

        public int GetCountByLineName(string lineName)
        {
            return _entities
                .Where(p => p.Name == lineName)
                .Count();
        }

        public Line GetReturnLine(Line line)
        {
            var returnLines = _entities
                .Where(p => p.Id != line.Id && p.Name == line.Name);
            if (returnLines != null && returnLines.Count() > 0)
                return returnLines.First();
            return null;
        }

        public IEnumerable<Line> GetAllWithoutReturnLines()
        {
            IList<Line> linesWithoutReturns = new List<Line>();
            foreach (var line in _entities)
            {
                if (line.IsCircular || GetReturnLine(line) == null)
                {
                    linesWithoutReturns.Add(line);
                }                    
                else
                {
                    if (!linesWithoutReturns.Contains(GetReturnLine(line)))
                        linesWithoutReturns.Add(line);
                }
            }
            return linesWithoutReturns
                .OrderBy(p => p.Name);
        }
    }
}

using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Databases;
using Chilicki.Commline.Infrastructure.Repositories.Base;
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
    }
}

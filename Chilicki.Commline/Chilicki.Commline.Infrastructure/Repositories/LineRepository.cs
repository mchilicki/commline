using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Databases;
using Chilicki.Commline.Infrastructure.Repositories.Base;

namespace Chilicki.Commline.Infrastructure.Repositories
{
    public class LineRepository : BaseRepository<Line>
    {
        readonly StopRepository _stopRepository;

        public LineRepository(CommlineDBContext db, StopRepository stopRepository) : base(db)
        {
            _stopRepository = stopRepository;
        }

        public bool DoesLineHaveEmptyRoute(long lineId)
        {
            return _stopRepository.GetCount() == 0;
        }
    }
}

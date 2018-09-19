using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Repositories.Base;
using System.Data.Entity;

namespace Chilicki.Commline.Infrastructure.Repositories
{
    public class LineRepository : BaseRepository<Line>
    {
        public LineRepository(DbContext db) : base(db)
        {

        }
    }
}

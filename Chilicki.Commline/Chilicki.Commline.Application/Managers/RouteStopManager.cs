using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.Managers
{
    public class RouteStopManager
    {
        readonly RouteStopRepository _RouteStopRepository;

        public RouteStopManager(RouteStopRepository RouteStopRepository)
        {
            _RouteStopRepository = RouteStopRepository;
        }

        public RouteStop GetById(long id)
        {
            return _RouteStopRepository.GetById(id);
        }

        public IEnumerable<RouteStop> GetAll()
        {
            return _RouteStopRepository.GetAll();
        }

        public void Create(RouteStop RouteStop)
        {
            _RouteStopRepository.Insert(RouteStop);
        }

        public void Edit(RouteStop RouteStop)
        {
            _RouteStopRepository.Update(RouteStop);
        }
    }
}

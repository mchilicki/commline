using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Repositories;
using System.Collections.Generic;

namespace Chilicki.Commline.Application.Managers
{
    public class RouteStopManager
    {
        readonly RouteStopRepository _routeStopRepository;

        public RouteStopManager(RouteStopRepository routeStopRepository)
        {
            _routeStopRepository = routeStopRepository;
        }

        public RouteStop GetById(long id)
        {
            return _routeStopRepository.GetById(id);
        }

        public IEnumerable<RouteStop> GetAll()
        {
            return _routeStopRepository.GetAll();
        }

        public void Create(RouteStop RouteStop)
        {
            _routeStopRepository.Insert(RouteStop);
        }

        public void Edit(RouteStop RouteStop)
        {
            _routeStopRepository.Update(RouteStop);
        }
    }
}

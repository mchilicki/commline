using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.Managers
{
    public class RouteStationManager
    {
        readonly RouteStationRepository _routeStationRepository;

        public RouteStationManager(RouteStationRepository RouteStationRepository)
        {
            _routeStationRepository = RouteStationRepository;
        }

        public RouteStation GetById(long id)
        {
            return _routeStationRepository.GetById(id);
        }

        public IEnumerable<RouteStation> GetAll()
        {
            return _routeStationRepository.GetAll();
        }

        public void Create(RouteStation RouteStation)
        {
            _routeStationRepository.Insert(RouteStation);
        }

        public void Edit(RouteStation RouteStation)
        {
            _routeStationRepository.Update(RouteStation);
        }
    }
}

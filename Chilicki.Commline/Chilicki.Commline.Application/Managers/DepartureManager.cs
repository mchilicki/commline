using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Repositories;
using System.Collections.Generic;

namespace Chilicki.Commline.Application.Managers
{
    public class DepartureManager
    {
        readonly DepartureRepository _departureRepository;

        public DepartureManager(DepartureRepository departureRepository)
        {
            _departureRepository = departureRepository;
        }

        public Departure GetById(long id)
        {
            return _departureRepository.GetById(id);
        }

        public IEnumerable<Departure> GetAll()
        {
            return _departureRepository.GetAll();
        }

        public void Create(Departure Departure)
        {
            _departureRepository.Insert(Departure);
        }

        public void Edit(Departure Departure)
        {
            _departureRepository.Update(Departure);
        }
    }
}

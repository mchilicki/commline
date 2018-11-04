using Chilicki.Commline.Application.DTOs.Search;
using Chilicki.Commline.Application.Resources;
using Chilicki.Commline.Application.Validators.Base;
using Chilicki.Commline.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.Validators
{
    public class SearchValidator : IValidator<SearchInputDTO>
    {
        readonly StopRepository _stopRepository;

        public SearchValidator(StopRepository stopRepository)
        {
            _stopRepository = stopRepository;
        }

        public bool Validate(SearchInputDTO search)
        {
            if (search == null)
                throw new ArgumentNullException(nameof(search));
            if (search.StartStopId == -1)
                throw new ArgumentException(ValidationResources.StartStopIsEmpty);
            if (search.DestinationStopId == -1)
                throw new ArgumentException(ValidationResources.EndStopIsEmpty);
            if (!_stopRepository.DoesStopWithIdExist(search.StartStopId))
                throw new ArgumentException($"{ValidationResources.StopWithIdDoesNotExist} {search.StartStopId}");
            if (!_stopRepository.DoesStopWithIdExist(search.DestinationStopId))
                throw new ArgumentException($"{ValidationResources.StopWithIdDoesNotExist} {search.DestinationStopId}");
            if (search.StartDate.Equals(DateTime.MinValue))
                throw new ArgumentException(ValidationResources.DateIsEmpty);
            return true;
        }

        public bool Validate(IEnumerable<SearchInputDTO> searches)
        {
            foreach (var search in searches)
            {
                Validate(search);
            }
            return true;
        }
    }
}

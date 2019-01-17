using Chilicki.Commline.Application.Search.DTOs;
using Chilicki.Commline.Application.Search.Resources;
using Chilicki.Commline.Application.Validators.Base;
using Chilicki.Commline.Infrastructure.Repositories;
using System;
using System.Collections.Generic;

namespace Chilicki.Commline.Application.Search.Validators
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
            if (search.StartStopId == Guid.Empty)
                throw new ArgumentException(SearchValidationResources.StartStopIsEmpty);
            if (search.DestinationStopId == Guid.Empty)
                throw new ArgumentException(SearchValidationResources.EndStopIsEmpty);
            if (!_stopRepository.DoesStopWithIdExist(search.StartStopId))
                throw new ArgumentException($"{SearchValidationResources.StopWithIdDoesNotExist} {search.StartStopId}");
            if (!_stopRepository.DoesStopWithIdExist(search.DestinationStopId))
                throw new ArgumentException($"{SearchValidationResources.StopWithIdDoesNotExist} {search.DestinationStopId}");
            if (search.StartDate.Equals(DateTime.MinValue))
                throw new ArgumentException(SearchValidationResources.DateIsEmpty);
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

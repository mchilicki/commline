using Chilicki.Commline.Application.ManualMappers.Search.Base;
using Chilicki.Commline.Application.Search.DTOs;
using Chilicki.Commline.Application.Search.Validators;
using Chilicki.Commline.Domain.Search.Aggregates;
using Chilicki.Commline.Infrastructure.Repositories;

namespace Chilicki.Commline.Application.Search.ManualMappers
{
    public class SearchInputManualMapper : IToDomainManualMapper<SearchInputDTO, SearchInput>
    {
        readonly StopRepository _stopRepository;
        readonly SearchValidator _searchValidator;


        public SearchInputManualMapper(           
            StopRepository stopRepository,
            SearchValidator searchValidator)
        {
            _stopRepository = stopRepository;
            _searchValidator = searchValidator;
        }

        public SearchInput ToDomain(SearchInputDTO searchInput)
        {            
            return new SearchInput()
            {
                StartStop = _stopRepository.GetById(searchInput.StartStopId),
                DestinationStop = _stopRepository.GetById(searchInput.DestinationStopId),
                StartTime = searchInput.StartTime,
                StartDate = searchInput.StartDate,
            };
        }
    }
}

using Chilicki.Commline.Application.DTOs.Search;
using Chilicki.Commline.Application.ManualMappers.Base;
using Chilicki.Commline.Application.Validators;
using Chilicki.Commline.Domain.Aggregates.Search;
using Chilicki.Commline.Domain.Services.Search.Base;
using Chilicki.Commline.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.ManualMappers
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

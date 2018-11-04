using Chilicki.Commline.Application.DTOs.Search;
using Chilicki.Commline.Application.Validators;
using Chilicki.Commline.Application.ManualMappers;
using Chilicki.Commline.Domain.Aggregates.Search;
using Chilicki.Commline.Domain.Services.Search.Base;

namespace Chilicki.Commline.Application.Managers
{
    public class SearchManager
    {
        readonly IConnectionSearchEngine _connectionSearchEngine;
        readonly SearchValidator _searchValidator;
        readonly SearchInputManualMapper _searchInputManualMapper;

        public SearchManager(
            IConnectionSearchEngine connectionSearchEngine,
            SearchValidator searchValidator,
            SearchInputManualMapper searchInputManualMapper)
        {
            _connectionSearchEngine = connectionSearchEngine;
            _searchValidator = searchValidator;
            _searchInputManualMapper = searchInputManualMapper;
        }

        public void SearchFastestConnections(SearchInputDTO searchInputDTO)
        {
            _searchValidator.Validate(searchInputDTO);
            var searchInput = _searchInputManualMapper.ToDomain(searchInputDTO);
            _connectionSearchEngine.SearchConnections(searchInput);
        }
    }
}

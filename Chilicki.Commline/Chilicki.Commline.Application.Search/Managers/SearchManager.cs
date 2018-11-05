using Chilicki.Commline.Domain.Search.Services.Base;
using Chilicki.Commline.Application.Search.Validators;
using Chilicki.Commline.Application.Search.DTOs;
using Chilicki.Commline.Application.Search.ManualMappers;

namespace Chilicki.Commline.Application.Search.Managers
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

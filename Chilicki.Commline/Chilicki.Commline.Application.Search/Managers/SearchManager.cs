using Chilicki.Commline.Domain.Search.Services.Base;
using Chilicki.Commline.Application.Search.Validators;
using Chilicki.Commline.Application.Search.DTOs;
using Chilicki.Commline.Application.Search.ManualMappers;
using Chilicki.Commline.Domain.Search.Services.GraphGenerator.Base;
using Chilicki.Commline.Domain.Search.Aggregates.Graph;
using Chilicki.Commline.Infrastructure.Repositories;

namespace Chilicki.Commline.Application.Search.Managers
{
    public class SearchManager
    {
        readonly IConnectionSearchEngine _connectionSearchEngine;
        readonly IGraphGenerator<StopGraph> _graphGenerator;
        readonly SearchValidator _searchValidator;
        readonly SearchInputManualMapper _searchInputManualMapper;
        readonly LineRepository _lineRepository;

        public SearchManager(
            IConnectionSearchEngine connectionSearchEngine,
            IGraphGenerator<StopGraph> graphGenerator,
            SearchValidator searchValidator,
            SearchInputManualMapper searchInputManualMapper,
            LineRepository lineRepository)
        {
            _connectionSearchEngine = connectionSearchEngine;
            _graphGenerator = graphGenerator;
            _searchValidator = searchValidator;
            _searchInputManualMapper = searchInputManualMapper;
            _lineRepository = lineRepository;
        }

        public void SearchFastestConnections(SearchInputDTO searchInputDTO)
        {
            _searchValidator.Validate(searchInputDTO);
            var searchInput = _searchInputManualMapper.ToDomain(searchInputDTO);
            var allLines = _lineRepository.GetAll();
            var stopGraph = _graphGenerator.GenerateGraph(allLines);
            _connectionSearchEngine.SearchConnections(searchInput, stopGraph);
        }
    }
}

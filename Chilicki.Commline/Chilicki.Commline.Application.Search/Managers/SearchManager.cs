using Chilicki.Commline.Domain.Search.Services.Base;
using Chilicki.Commline.Application.Search.Validators;
using Chilicki.Commline.Application.Search.DTOs;
using Chilicki.Commline.Application.Search.ManualMappers;
using Chilicki.Commline.Domain.Search.Services.GraphFactories.Base;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using Chilicki.Commline.Infrastructure.Repositories;

namespace Chilicki.Commline.Application.Search.Managers
{
    public class SearchManager
    {
        readonly IConnectionSearchEngine _connectionSearchEngine;
        readonly IGraphFactory<StopGraph> _graphGenerator;
        readonly SearchValidator _searchValidator;
        readonly SearchInputManualMapper _searchInputManualMapper;
        readonly LineRepository _lineRepository;
        readonly StopRepository _stopRepository;

        public SearchManager(
            IConnectionSearchEngine connectionSearchEngine,
            IGraphFactory<StopGraph> graphGenerator,
            SearchValidator searchValidator,
            SearchInputManualMapper searchInputManualMapper,
            LineRepository lineRepository,
            StopRepository stopRepository)
        {
            _connectionSearchEngine = connectionSearchEngine;
            _graphGenerator = graphGenerator;
            _searchValidator = searchValidator;
            _searchInputManualMapper = searchInputManualMapper;
            _lineRepository = lineRepository;
            _stopRepository = stopRepository;
        }

        public void SearchFastestConnections(SearchInputDTO searchInputDTO)
        {
            _searchValidator.Validate(searchInputDTO);
            var searchInput = _searchInputManualMapper.ToDomain(searchInputDTO);
            var stops = _stopRepository.GetAllConnectedToAnyLine();
            var stopGraph = _graphGenerator.CreateGraph(stops);
            _connectionSearchEngine.SearchConnections(searchInput, stopGraph);
        }
    }
}

using Chilicki.Commline.Domain.Search.Aggregates;

namespace Chilicki.Commline.Domain.Search.Services.Base
{
    public interface IConnectionSearchEngine
    {
        void SearchConnections(SearchInput search);
    }
}

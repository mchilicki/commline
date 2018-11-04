using Chilicki.Commline.Domain.Aggregates.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Domain.Services.Search.Base
{
    public interface IConnectionSearchEngine
    {
        void SearchConnections(SearchInput search);
    }
}

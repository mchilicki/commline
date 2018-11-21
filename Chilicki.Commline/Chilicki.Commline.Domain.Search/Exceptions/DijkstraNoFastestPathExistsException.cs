using Chilicki.Commline.Domain.Search.Resources;
using System;

namespace Chilicki.Commline.Domain.Search.Exceptions
{
    public class DijkstraNoFastestPathExistsException : Exception
    {
        public DijkstraNoFastestPathExistsException() : base(SearchResources.NoFastestPathExists)
        {

        }
    }
}

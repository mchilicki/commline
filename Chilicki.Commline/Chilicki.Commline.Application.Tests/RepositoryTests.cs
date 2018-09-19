using System;
using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit;

namespace Chilicki.Commline.Application.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        Mock<StopRepository> _stopRepository;

        [TestInitialize]
        public void Init()
        {
            _stopRepository = new Mock<StopRepository>();
        }

        [TestMethod]
        public void TestRepositories()
        {
            Stop stop = new Stop() { Name = "Przystanek", SiteNumber = 1, Latitude = 1.00, Longitude = 5.00 };
            _stopRepository.Setup(a => a.GetAll());
        }
    }
}

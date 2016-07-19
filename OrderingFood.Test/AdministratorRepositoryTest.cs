using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework;
using Moq;
using OrderingFood.Data.Context;
using OrderingFood.DataAccess.UnitOFWork;

namespace OrderingFood.Test
{
    [TestClass]
    public class AdministratorRepositoryTest
    {
        private Mock<IOrderingContext> _context = null;

        private void Setup()
        {
            _context = new Mock<IOrderingContext>();
        }

        [TestMethod]
        public void GetAdministratorsTest()
        {
            Setup();
            IOrderingContext cont = new OrderingContext();
            UnitOfWork uow = new UnitOfWork(cont);
            var result = uow.AdministratorRepository.Get();
            Assert.IsNotNull(result);
        }
    }
}

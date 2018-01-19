using ABV.Core.Accounts.Queries.GetPeriods;
using ABV.Core.Contracts;
using ABV.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABV.Specification
{
    [TestClass]
    public class PeriodsQueryTests
    {
        IQueryable<Period> data;
        Mock<IDbContextService> mockContext;
        public PeriodsQueryTests()
        {
            SetupPeriods();
            ArrangeMocks();
        }

        private void SetupPeriods()
        {
            data = new List<Period>
            {
                new Period { Id = Guid.Parse("d0190e33-13e3-4a1f-58f9-08d55d0e0132") , AsofDate = new DateTime(2017, 09, 30) },
                new Period { Id = Guid.Parse("2cb74536-b2d4-4aeb-b828-4bea540a0f94") , AsofDate = new DateTime(2017, 12, 31)  },
                new Period { Id = Guid.Parse("b1ed7949-a96e-40c8-495f-08d55b76f591") , AsofDate = new DateTime(2017, 10, 31)  },
                new Period { Id = Guid.Parse("4d613cac-78f1-4879-0089-08d55b74793d") , AsofDate = new DateTime(2017, 11, 30)  }
            }.AsQueryable();
        }

        private void ArrangeMocks()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Period>>();
            mockSet.As<IQueryable<Period>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Period>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Period>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Period>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext = new Mock<IDbContextService>();
            mockContext.Setup(c => c.Periods).Returns(mockSet.Object);
        }

        [TestMethod]
        public void PeriodsQuery_Return_All_Periods()
        {
            // Act
            var query = new GetPeriodsListQuery(mockContext.Object);
            var actual = query.Execute(0);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(List<PeriodListItemModel>));
            Assert.AreEqual(4, actual.Count);  
            Assert.AreEqual(new DateTime(2017, 09, 30), actual[0].AsOfDate);
            Assert.AreEqual(new DateTime(2017, 12, 31), actual[3].AsOfDate);
        }

        [TestMethod]
        public void PeriodsQuery_Return_One_Recent_Period()
        {
            // Act
            var query = new GetPeriodsListQuery(mockContext.Object);
            var actual = query.Execute(1);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count);
            Assert.IsInstanceOfType(actual, typeof(List<PeriodListItemModel>));
            Assert.AreEqual(new DateTime(2017, 12, 31), actual[0].AsOfDate);
        }

        [TestMethod]
        public void PeriodsQuery_Return_One_Period_Model()
        {
            // Act
            var query = new GetPeriodsListQuery(mockContext.Object);
            var actual = query.Execute(new DateTime(2017, 11, 30));

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(new DateTime(2017, 11, 30), actual.AsOfDate);
            Assert.AreEqual(Guid.Parse("4d613cac-78f1-4879-0089-08d55b74793d"), actual.PeriodId);
            Assert.IsInstanceOfType(actual, typeof(PeriodListItemModel));

            // Act
            actual = query.Execute(new DateTime(2017, 06, 30));

            // Assert
            Assert.IsNull(actual);
        }
    }
}

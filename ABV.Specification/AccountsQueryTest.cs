using ABV.Core.Accounts.Queries.GetAccounts;
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
    public class AccountsQueryTest
    {
        IQueryable<Account> data;
        Mock<IDbContextService> mockContext;

        public AccountsQueryTest()
        {
            SetupAccounts();
            ArrangeMocks();
        }

        private void SetupAccounts()
        {
            data = new List<Account>
            {
                new Account { Id = Guid.Parse("d0190e33-13e3-4a1f-58f9-08d55d0e0132") , Name="R & D", Currency = "Rs" },
                new Account { Id = Guid.Parse("2cb74536-b2d4-4aeb-b828-4bea540a0f94") , Name="Canteen", Currency = "Rs" },
                new Account { Id = Guid.Parse("b1ed7949-a96e-40c8-495f-08d55b76f591") , Name="Marketing", Currency = "Rs" },
                new Account { Id = Guid.Parse("4d613cac-78f1-4879-0089-08d55b74793d") , Name="Parking Fines", Currency = "Rs" }
            }.AsQueryable();
        }

        private void ArrangeMocks()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Account>>();
            mockSet.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext = new Mock<IDbContextService>();
            mockContext.Setup(c => c.Accounts).Returns(mockSet.Object);
        }

        [TestMethod]
        public void AccountsQuery_Return_One_Account_Model()
        {
            // Act
            var query = new GetAccountsListQuery(mockContext.Object);
            var actual = query.Execute("Marketing");

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual("Rs", actual.Currency);
            Assert.AreEqual("Marketing", actual.AccountName);
            Assert.AreEqual(Guid.Parse("b1ed7949-a96e-40c8-495f-08d55b76f591"), actual.AccountId);
            Assert.IsInstanceOfType(actual, typeof(AccountsListItemModel));

            // Act
            actual = query.Execute("Gambling");

            // Assert
            Assert.IsNull(actual);
        }
    }
}

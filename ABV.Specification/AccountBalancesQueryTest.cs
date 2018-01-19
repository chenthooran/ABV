using ABV.Core.Accounts.Queries.GetAccountBalances;
using ABV.Core.Contracts;
using ABV.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABV.Specification
{
    [TestClass]
    public class AccountBalancesQueryTest
    {
        IQueryable<AccountBalance> data;
        IQueryable<Account> accountData;
        IQueryable<Period> periodData;
        Mock<IDbContextService> mockContext;
        public AccountBalancesQueryTest()
        {
            SetupAccounts();
            SetupPeriods();
            //SetupAccountBalances();
            ArrangeMocks();
        }

        private void ArrangeMocks()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Account>>();
            mockSet.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(accountData.Provider);
            mockSet.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(accountData.Expression);
            mockSet.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(accountData.ElementType);
            mockSet.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(accountData.GetEnumerator());

            mockContext = new Mock<IDbContextService>();
            mockContext.Setup(c => c.Accounts).Returns(mockSet.Object);
        }

        [TestMethod]
        public void AccountsBalancesQuery_Return_All_AccountBalance_ChartModels()
        {
            // Act
            var query = new GetAccountBalancesListQuery(mockContext.Object);
            var actual = query.Execute();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(4, actual.Count);
            Assert.IsInstanceOfType(actual, typeof(List<AccountBalanceChartModel>));
            //var cou = actual.Single(a => a.Label == "Marketing").Data.Count();
            //Assert.AreEqual(3, cou);          
        }

        private void SetupAccountBalances()
        {
            data = new List<AccountBalance>
            {
                new AccountBalance { AccountId = Guid.Parse("d0190e33-13e3-4a1f-58f9-08d55d0e0132") , PeriodId= Guid.Parse("85fd37fd-f866-4580-9642-211c0970b8c6"), Balance = 7000.00M },
                new AccountBalance { AccountId = Guid.Parse("2cb74536-b2d4-4aeb-b828-4bea540a0f94") , PeriodId= Guid.Parse("85fd37fd-f866-4580-9642-211c0970b8c6"), Balance = 2000.00M },
                new AccountBalance { AccountId = Guid.Parse("b1ed7949-a96e-40c8-495f-08d55b76f591") , PeriodId= Guid.Parse("85fd37fd-f866-4580-9642-211c0970b8c6"), Balance = 4000.00M },
                new AccountBalance { AccountId = Guid.Parse("4d613cac-78f1-4879-0089-08d55b74793d") , PeriodId= Guid.Parse("85fd37fd-f866-4580-9642-211c0970b8c6"), Balance = 1000.00M },

                new AccountBalance { AccountId = Guid.Parse("d0190e33-13e3-4a1f-58f9-08d55d0e0132") , PeriodId= Guid.Parse("0d2e36f0-3aed-4a65-b177-0528e6fc610c"), Balance = 17000.00M },
                new AccountBalance { AccountId = Guid.Parse("2cb74536-b2d4-4aeb-b828-4bea540a0f94") , PeriodId= Guid.Parse("0d2e36f0-3aed-4a65-b177-0528e6fc610c"), Balance = 900.50M },
                new AccountBalance { AccountId = Guid.Parse("b1ed7949-a96e-40c8-495f-08d55b76f591") , PeriodId= Guid.Parse("0d2e36f0-3aed-4a65-b177-0528e6fc610c"), Balance = 11000.00M },
                new AccountBalance { AccountId = Guid.Parse("4d613cac-78f1-4879-0089-08d55b74793d") , PeriodId= Guid.Parse("0d2e36f0-3aed-4a65-b177-0528e6fc610c"), Balance = -2000.00M },

                new AccountBalance { AccountId = Guid.Parse("d0190e33-13e3-4a1f-58f9-08d55d0e0132") , PeriodId= Guid.Parse("93cbbe6f-f551-455d-aff0-cb08e77f6471"), Balance = 11500.00M },
                new AccountBalance { AccountId = Guid.Parse("2cb74536-b2d4-4aeb-b828-4bea540a0f94") , PeriodId= Guid.Parse("93cbbe6f-f551-455d-aff0-cb08e77f6471"), Balance = -4300.00M },
                new AccountBalance { AccountId = Guid.Parse("b1ed7949-a96e-40c8-495f-08d55b76f591") , PeriodId= Guid.Parse("93cbbe6f-f551-455d-aff0-cb08e77f6471"), Balance = 4000.00M },
                new AccountBalance { AccountId = Guid.Parse("4d613cac-78f1-4879-0089-08d55b74793d") , PeriodId= Guid.Parse("93cbbe6f-f551-455d-aff0-cb08e77f6471"), Balance = 3000.40M }
            }.AsQueryable();
        }

        private void SetupAccounts()
        {
            accountData = new List<Account>
            {
                new Account
                {   Id = Guid.Parse("d0190e33-13e3-4a1f-58f9-08d55d0e0132") , Name="R & D", Currency = "Rs",
                    Balances = new List<AccountBalance>()
                    {
                        new AccountBalance { AccountId = Guid.Parse("d0190e33-13e3-4a1f-58f9-08d55d0e0132") , PeriodId= Guid.Parse("85fd37fd-f866-4580-9642-211c0970b8c6"), Balance = 7000.00M },
                        new AccountBalance { AccountId = Guid.Parse("d0190e33-13e3-4a1f-58f9-08d55d0e0132") , PeriodId= Guid.Parse("0d2e36f0-3aed-4a65-b177-0528e6fc610c"), Balance = 17000.00M },
                        new AccountBalance { AccountId = Guid.Parse("d0190e33-13e3-4a1f-58f9-08d55d0e0132") , PeriodId= Guid.Parse("93cbbe6f-f551-455d-aff0-cb08e77f6471"), Balance = 11500.00M }
                    }
                },
                new Account
                {
                    Id = Guid.Parse("2cb74536-b2d4-4aeb-b828-4bea540a0f94") , Name="Canteen", Currency = "Rs",
                    Balances = new List<AccountBalance>()
                    {
                        new AccountBalance { AccountId = Guid.Parse("2cb74536-b2d4-4aeb-b828-4bea540a0f94") , PeriodId= Guid.Parse("85fd37fd-f866-4580-9642-211c0970b8c6"), Balance = 2000.00M },
                        new AccountBalance { AccountId = Guid.Parse("2cb74536-b2d4-4aeb-b828-4bea540a0f94") , PeriodId= Guid.Parse("0d2e36f0-3aed-4a65-b177-0528e6fc610c"), Balance = 900.50M },
                        new AccountBalance { AccountId = Guid.Parse("2cb74536-b2d4-4aeb-b828-4bea540a0f94") , PeriodId= Guid.Parse("93cbbe6f-f551-455d-aff0-cb08e77f6471"), Balance = -4300.00M }
                    }
                },
                new Account
                {
                    Id = Guid.Parse("b1ed7949-a96e-40c8-495f-08d55b76f591") , Name="Marketing", Currency = "Rs",
                    Balances = new List<AccountBalance>()
                    {
                        new AccountBalance { AccountId = Guid.Parse("b1ed7949-a96e-40c8-495f-08d55b76f591") , PeriodId= Guid.Parse("85fd37fd-f866-4580-9642-211c0970b8c6"), Balance = 4000.00M },
                        new AccountBalance { AccountId = Guid.Parse("b1ed7949-a96e-40c8-495f-08d55b76f591") , PeriodId= Guid.Parse("0d2e36f0-3aed-4a65-b177-0528e6fc610c"), Balance = 11000.00M },
                        new AccountBalance { AccountId = Guid.Parse("b1ed7949-a96e-40c8-495f-08d55b76f591") , PeriodId= Guid.Parse("93cbbe6f-f551-455d-aff0-cb08e77f6471"), Balance = 4000.00M }
                    }
                },
                new Account
                {
                    Id = Guid.Parse("4d613cac-78f1-4879-0089-08d55b74793d") , Name="Parking Fines", Currency = "Rs",
                    Balances = new List<AccountBalance>()
                    {
                        new AccountBalance { AccountId = Guid.Parse("4d613cac-78f1-4879-0089-08d55b74793d") , PeriodId= Guid.Parse("85fd37fd-f866-4580-9642-211c0970b8c6"), Balance = 1000.00M },
                        new AccountBalance { AccountId = Guid.Parse("4d613cac-78f1-4879-0089-08d55b74793d") , PeriodId= Guid.Parse("0d2e36f0-3aed-4a65-b177-0528e6fc610c"), Balance = -2000.00M },
                        new AccountBalance { AccountId = Guid.Parse("4d613cac-78f1-4879-0089-08d55b74793d") , PeriodId= Guid.Parse("93cbbe6f-f551-455d-aff0-cb08e77f6471"), Balance = 3000.40M }
                    }
                }
            }.AsQueryable();
        }

        private void SetupPeriods()
        {
            periodData = new List<Period>
            {
                new Period { Id = Guid.Parse("85fd37fd-f866-4580-9642-211c0970b8c6") , AsofDate = new DateTime(2017, 10, 31)},
                new Period { Id = Guid.Parse("0d2e36f0-3aed-4a65-b177-0528e6fc610c") , AsofDate = new DateTime(2017, 11, 30)},
                new Period { Id = Guid.Parse("93cbbe6f-f551-455d-aff0-cb08e77f6471") , AsofDate = new DateTime(2017, 12, 31)}
            }.AsQueryable();
        }
    }
}

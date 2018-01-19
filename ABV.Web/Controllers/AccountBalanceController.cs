using ABV.Core.Accounts.Queries.GetAccountBalances;
using ABV.Core.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ABV.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AccountBalanceController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountBalanceController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("Latest")]
        public IEnumerable<AccountBalancesListItemModel> GetLatestBalances()
        {
            return _accountService.GetLatestAccountBalances();
        }

        [HttpGet("ChartData")]
        [Authorize(Roles = "Admins")]
        public IEnumerable<AccountBalanceChartModel> GetChartData()
        {
            return _accountService.GetAccountwithBalances();
        }

        [HttpGet("ChartLabels")]
        [Authorize(Roles = "Admins")]
        public IEnumerable<string> GetChartLabels()
        {
            return _accountService.GetAllDates();
        }
    }
}
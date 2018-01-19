using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;
using System.Globalization;
using ABV.Core.Contracts;
using ABV.Core.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ABV.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class FileUploadController : Controller
    {
        private readonly IAccountService _accountService;

        public FileUploadController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Post")]
        [Authorize(Roles = "Admins")]
        public async Task<IActionResult> Post(IFormFile selectFile)
        {
            var accountsVMList = new List<AccountBalanceViewModel>();
            if (selectFile != null)
            {
                var asOfDate = GetAsOfDate(selectFile.FileName);

                if (asOfDate == DateTime.MinValue)
                    return BadRequest("Incorrect Filename format. Please upload with the correct format");

                DataTable dt = GetData(selectFile);

                accountsVMList = (from DataRow dr in dt.Rows
                                  select new AccountBalanceViewModel()
                                  {
                                      AccountName = dr[0].ToString(),
                                      AsOfDate = asOfDate,
                                      AccountBalance = Convert.ToDecimal(dr[1].ToString())
                                  }).ToList();

                await _accountService.CreateAccountBalances(accountsVMList);

                return Ok();
            }

            return BadRequest();
        }

        private static DataTable GetData(IFormFile selectFile)
        {
            var stream = selectFile.OpenReadStream();
            var dt = new DataTable();

            using (StreamReader sr = new StreamReader(stream))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        private static DateTime GetAsOfDate(string fileName)
        {
            DateTime date = DateTime.MinValue;
            if (fileName.EndsWith(".csv", true, CultureInfo.InvariantCulture))
            {
                var segments = fileName.Split('_');
                if (segments.Length == 2)
                {
                    var datePart = segments[1].Replace(".csv", "", true, CultureInfo.InvariantCulture);
                    if (DateTime.TryParseExact(datePart, "MMddyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime asOfDate))
                    {
                        return asOfDate;
                    }
                }
            }

            return date;
        }
    }
}
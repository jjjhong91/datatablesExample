using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using datatablesExample.Models;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Binders;

namespace datatablesExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Datatables()
        {
            return View();
        }

        [HttpGet("/api/data")]
        public async Task<IActionResult> GetAsync()
        {
            await Task.Yield();
            var data = new List<Employee>
            {
                new Employee { DT_RowId = "row_1", Name = "Tiger Nixon", Position = "System Architect", Office = "Edinburgh", Age = 61, StartDate = "2011/04/25", Salary = "$320,800" },
                new Employee { DT_RowId = "row_2", Name = "Garrett Winters", Position = "Accountant", Office = "Tokyo", Age = 63, StartDate = "2011/07/25", Salary = "$170,750" },
                new Employee { DT_RowId = "row_3", Name = "Ashton Cox", Position = "Junior Technical Author", Office = "San Francisco", Age = 66, StartDate = "2009/01/12", Salary = "$86,000" },
                new Employee { DT_RowId = "row_4", Name = "Cedric Kelly", Position = "Senior Javascript Developer", Office = "Edinburgh", Age = 22, StartDate = "2012/03/29", Salary = "$433,060" },
                new Employee { DT_RowId = "row_5", Name = "Airi Satou", Position = "Accountant", Office = "Tokyo", Age = 33, StartDate = "2008/11/28", Salary = "$162,700" },
                new Employee { DT_RowId = "row_6", Name = "Brielle Williamson", Position = "Integration Specialist", Office = "New York", Age = 61, StartDate = "2012/12/02", Salary = "$372,000" },
                new Employee { DT_RowId = "row_7", Name = "Herrod Chandler", Position = "Sales Assistant", Office = "San Francisco", Age = 59, StartDate = "2012/08/06", Salary = "$137,500" },
                new Employee { DT_RowId = "row_8", Name = "Rhona Davidson", Position = "Integration Specialist", Office = "Tokyo", Age = 55, StartDate = "2010/10/14", Salary = "$327,900" },
            };
        
            return Ok(new {
                draw = 1,
                recordsTotal = data.Count,
                recordsFiltered = data.Count,
                data = data,
            });
        }

        [HttpGet("/api/data2")]
        public async Task<IActionResult> Get2Async([ModelBinder(typeof(JqueryDataTablesBinder))]  JqueryDataTablesParameters param)
        {
            var searchValue = param.Search?.Value;
            await Task.Yield();
            var data = new List<Employee>
            {
                new Employee { DT_RowId = "row_1", Name = "Tiger Nixon", Position = "System Architect", Office = "Edinburgh", Age = 61, StartDate = "2011/04/25", Salary = "$320,800" },
                new Employee { DT_RowId = "row_2", Name = "Garrett Winters", Position = "Accountant", Office = "Tokyo", Age = 63, StartDate = "2011/07/25", Salary = "$170,750" },
                new Employee { DT_RowId = "row_3", Name = "Ashton Cox", Position = "Junior Technical Author", Office = "San Francisco", Age = 66, StartDate = "2009/01/12", Salary = "$86,000" },
                new Employee { DT_RowId = "row_4", Name = "Cedric Kelly", Position = "Senior Javascript Developer", Office = "Edinburgh", Age = 22, StartDate = "2012/03/29", Salary = "$433,060" },
                new Employee { DT_RowId = "row_5", Name = "Airi Satou", Position = "Accountant", Office = "Tokyo", Age = 33, StartDate = "2008/11/28", Salary = "$162,700" },
                new Employee { DT_RowId = "row_6", Name = "Brielle Williamson", Position = "Integration Specialist", Office = "New York", Age = 61, StartDate = "2012/12/02", Salary = "$372,000" },
                new Employee { DT_RowId = "row_7", Name = "Herrod Chandler", Position = "Sales Assistant", Office = "San Francisco", Age = 59, StartDate = "2012/08/06", Salary = "$137,500" },
                new Employee { DT_RowId = "row_8", Name = "Rhona Davidson", Position = "Integration Specialist", Office = "Tokyo", Age = 55, StartDate = "2010/10/14", Salary = "$327,900" },
            };
        
            return new JsonResult(new JqueryDataTablesResult<Employee>
            {
                Draw = param.Draw,
                RecordsTotal = data.Count,
                RecordsFiltered = data.Count,
                Data = data.Skip(param.Start).Take(param.Length).ToList(),
            });
        }

        
        
    }

    public class Employee
    {
        public string DT_RowId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public int Age { get; set; }
        public string StartDate { get; set; }
        public string Salary { get; set; }
    }
}

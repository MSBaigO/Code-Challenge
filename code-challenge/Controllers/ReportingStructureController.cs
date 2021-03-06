using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/reportingStructure")]
    public class ReportingStructureController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public ReportingStructureController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        /// <summary>
        /// On get request returns the reporting structure of employee.
        /// </summary>
        /// <param name="id">id of user requesting direct reports</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "getReportingStructureById")]
        public IActionResult GetReportingStructureById(String id){

            _logger.LogDebug($"Got request for reporting structure for employee Id: '{id}'");
            var reportingStructure = _employeeService.GetReportingStructureById(id);
            
            if(reportingStructure == null){
                return NotFound();
            }

            return Ok(reportingStructure);
        }
    }
}

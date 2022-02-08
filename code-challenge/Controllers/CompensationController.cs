using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace code_challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        /// <summary>
        /// On post request creates an employee.
        /// </summary>
        /// <param name="employee">the employee object with its parameters filled</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for '{compensation.EmployeeId}'");

            _compensationService.Create(compensation);

            return CreatedAtRoute("getCompensationById", new { id = compensation.EmployeeId }, compensation);
        }

        /// <summary>
        /// On get request returns compensation when Id is included in request.
        /// </summary>
        /// <param name="id">the unique id of employee</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetCompensationById(String id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}'");

            var compensation = _compensationService.GetById(id);
            
            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }

        /// <summary>
        /// Updates a compensation object on put request. 
        /// </summary>
        /// <param name="id">id of the compensation object to update</param>
        /// <param name="newCompensation">the updated compensation object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult ReplaceCompensation(String id, [FromBody]Compensation newCompensation)
        {
            _logger.LogDebug($"Recieved compensation update request for '{id}'");

            var existingCompensation = _compensationService.GetById(id);
            if (existingCompensation == null)
                return NotFound();

            _compensationService.Replace(existingCompensation, newCompensation);

            return Ok(newCompensation);
        }
    }
}
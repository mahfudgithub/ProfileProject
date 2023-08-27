using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalProfile.Interface;
using PersonalProfile.Model.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalProfile.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;

        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody] EmployeeRequest employeeRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _employee.Register(employeeRequest);
                //return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
                return StatusCode(StatusCodes.Status201Created, result);
            }

            return BadRequest("Some Properties are not valid ");
        }
    }
}

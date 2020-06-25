
using System.Threading.Tasks;
using HollowService.Interfaces;
using HollowService.Model;
using HollowService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HollowService.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger _logger;
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            _logger.LogInformation("GetAllEmployee API was called");
            var employees = await _employeeService.GetAllEmployeeAsync();
            return Ok(employees);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            _logger.LogInformation("GetEmployeeById API was called");
            if(id < 0)
            {
                return BadRequest();

            }
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            int result;
            _logger.LogInformation("AddEmployee API was called");
            result= await _employeeService.AddEmployeeAsync(employee);
            if(result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeById(int id, [FromBody] Employee employee)
        {
            _logger.LogInformation("UpdateEmployeeById API was called");
            int result;
            if (id < 0)
            {
                return BadRequest();
            }
            employee.Id = id;
            result = await _employeeService.UpdateEmployeeByIdAsync(id, employee);
            switch (result)
            {
                case 2:
                    return NotFound();
                case 0:
                    return BadRequest();
                default:
                    break;
            }
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Delete API was called");
            int result;
            if (id < 0)
            {
                return BadRequest();
            }
            result = await _employeeService.DeleteEmployeeByIdAsync(id);
            if (result == 0)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}

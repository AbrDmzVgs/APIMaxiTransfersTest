using APIMaxiTransfersTest.Interface;
using APIMaxiTransfersTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace APIMaxiTransfersTest.Controllers
{
    [Route("Employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get([FromQuery] Employee employee)
        {
            return await _repository.GetAllEmployeesAsync(employee.Id);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Post([FromBody] Employee employee)
        {
            var id = await _repository.CreateEmployee(employee);
            employee.Id = id;

            return id > 0 ? new Response { IsSuccess = true, Message = "Employee saved successfully", Result = employee } : new Response { IsSuccess = false, Message = "Error trying to save employee", Result = employee };
        }

        [HttpPut]
        public async Task<ActionResult<Response>> Put([FromBody] Employee employee)
        {
            var id = await _repository.UpdateEmployee(employee);
            return id > 0 ? new Response { IsSuccess = true, Message = "Employee updated successfully", Result = employee } : new Response { IsSuccess = false, Message = "Error trying to update employee", Result = employee };
        }

        [HttpDelete]
        public async Task<ActionResult<Response>> Delete([FromBody] int id)
        {
            var idDeleted = await _repository.DeleteEmployee(id);
            return idDeleted > 0 ? new Response { IsSuccess = true, Message = "Employee deleted successfully", Result = id } : new Response { IsSuccess = false, Message = "Error trying to delete employee", Result = id };
        }
    }
}

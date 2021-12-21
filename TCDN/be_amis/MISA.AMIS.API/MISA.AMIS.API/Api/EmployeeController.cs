using AMIS.Core.Entities;
using AMIS.Core.Entities.Dto;
using AMIS.Core.Interfaces.Repository;
using AMIS.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMIS.Api.Api
{
    
    public class EmployeeController : AMISControlller<Employee>
    {
        IEmployeeService _employeeService;
        IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeService employeeService, IBaseRepository<Employee> baseRepository, IEmployeeRepository employeeRepository) : base(employeeService, baseRepository)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;
        }

        
        /*
         *  Override lại GetAll của Employee
         *  Trả về thêm Field DepartmentName
         */
        
        public override IActionResult Get()
        {
            var employeeDtos = _employeeRepository.GetAllField();
            return Ok(employeeDtos);
        }

        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            var employeeCode = _employeeRepository.GetEmployeeCode();
            return Ok(employeeCode);
        }

        [HttpGet("find")]
        public IActionResult GetByEmployeeCode(string EmployeeCode)
        {
            var employee = _employeeRepository.GetByEmployeeCode(EmployeeCode);
            return Ok(employee);
        }

        [HttpGet("search")]
        public IActionResult Search(string searchText)
        {
            var employees = _employeeRepository.SearchEmployee(searchText);
            return Ok(employees);
        }

        [HttpGet("totalrecord")]
        public IActionResult GetTotalRecord()
        {
            int count = _employeeRepository.GetTotalRecord();
            return Ok(count);
        }

        [HttpGet("pagingFilter")]
        public IActionResult GetEmployeePagingFilter(int recordNumber, int pageNumber)
        {
            var employees = _employeeRepository.GetEmployeePagingFilter(recordNumber, pageNumber);
            return Ok(employees);
        }
    }
}

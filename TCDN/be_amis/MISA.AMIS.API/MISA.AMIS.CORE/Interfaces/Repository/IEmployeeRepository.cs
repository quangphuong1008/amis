using AMIS.Core.Entities;
using AMIS.Core.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.Core.Interfaces.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        public List<EmployeeDto> GetAllField();
        public string GetEmployeeCode();

        public Employee GetByEmployeeCode(string EmployeeCode);

        public List<Employee> SearchEmployee(string text);

        public List<EmployeeDto> GetEmployeePagingFilter(int recordNumber, int pageNumber);

        public int GetTotalRecord();
    }
}

using AMIS.Core.Entities;
using AMIS.Core.Interfaces.Repository;
using AMIS.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.Core.Sevices
{
    public class EmployeeService:BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository):base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        protected override bool ValidateObjectCustom(Employee employee)
        {
            bool isValid = true;
            
            //2. Kiểm tra ngày sinh có lớn hơn ngày hiện tại hay không?
            DateTime today = DateTime.Today;
            if (employee.DateOfBirth > today)
            {
                _serviceResult.devMsg = Resource.NotValidDateOfBirth;
                _serviceResult.userMsg = Resource.ErrorUserMessenger;
                _serviceResult.errorCode = MISACode.NotValid;
                _serviceResult.data = new string[] { "DateOfBirth" };
                _serviceResult.Success = false;
                return false;
            }
            //3. Kiểm tra ngày cấp có lớn hơn ngày hiện tại hay không?
            if (employee.IdentityDate > today)
            {
                _serviceResult.devMsg = Resource.NotValidIdentityDate;
                _serviceResult.userMsg = Resource.ErrorUserMessenger;
                _serviceResult.errorCode = MISACode.NotValid;
                _serviceResult.data = new string[] { "IdentityDate" };
                _serviceResult.Success = false;
                return false;
            }
            //4. Kiểm tra email có @ hay không?
            if (String.IsNullOrEmpty(employee.Email) == false)
            {
                if(employee.Email.IndexOf('@') == -1)
                {
                    _serviceResult.devMsg = Resource.NotValidIdentityDate;
                    _serviceResult.userMsg = Resource.ErrorUserMessenger;
                    _serviceResult.errorCode = MISACode.NotValid;
                    _serviceResult.data = new string[] { "Email" };
                    _serviceResult.Success = false;
                    return false;
                }
            }
            return isValid;
        }
    }
}

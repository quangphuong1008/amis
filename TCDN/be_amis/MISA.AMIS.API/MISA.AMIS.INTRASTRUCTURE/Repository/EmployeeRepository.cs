using AMIS.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMIS.Core.Entities;
using Microsoft.Extensions.Configuration;
using AMIS.Core.Entities.Dto;
using Dapper;

namespace AMIS.Infrastruture.Repository
{
    public class EmployeeRepository:BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public List<EmployeeDto> GetAllField()
        {
            var employees = _sqlConnection.Query<EmployeeDto>($"SELECT Employee.*, DepartmentCode, DepartmentName FROM Employee LEFT JOIN Department ON Employee.DepartmentId = Department.DepartmentId ORDER BY Employee.CreatedDate DESC");
            return employees.ToList();
        }

        public Employee GetByEmployeeCode(string EmployeeCode)
        {
            var sqlCommand = $"SELECT * FROM Employee WHERE EmployeeCode = @EmployeeCode";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@EmployeeCode", EmployeeCode);
            // QueryFirstOrDefault - Lấy bản ghi đầu tiền từ câu lệnh truy vấn, nếu không có dữ liệu thì trả về null:
            var employee = _sqlConnection.QueryFirstOrDefault<Employee>(sqlCommand, param: parameters);
            return employee;
        }

        public string GetEmployeeCode()
        {
            var employeeCode = _sqlConnection.Query<string>($"SELECT CONCAT('NV', CONVERT(MAX(CONVERT(SUBSTRING(EmployeeCode,3,LENGTH(EmployeeCode)), int))+1, CHAR)) FROM Employee e;");
            return employeeCode.ToList()[0];
        }

        public List<EmployeeDto> GetEmployeePagingFilter(int recordNumber, int pageNumber)
        {
            var sqlCommand = $"SELECT Employee.*, Department.DepartmentName, Department.DepartmentCode FROM Employee LEFT JOIN Department ON Employee.DepartmentId = Department.DepartmentId ORDER BY Employee.CreatedDate DESC LIMIT @RECORDNUMBER OFFSET @INDEX;";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@RECORDNUMBER", recordNumber);
            parameters.Add("@INDEX", (pageNumber-1)*recordNumber);
            var employees = _sqlConnection.Query<EmployeeDto>(sqlCommand, param: parameters);
            return employees.ToList();
        }

        public List<Employee> SearchEmployee(string text)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Text", text);
            var sqlCommand = $"SELECT * FROM Employee WHERE(FullName LIKE CONCAT('%', @Text, '%') OR EmployeeCode LIKE CONCAT('%', @Text, '%'))";
            var employees = _sqlConnection.Query<Employee>(sqlCommand, param: parameters);
            return employees.ToList();
        }

        public int GetTotalRecord()
        {
            var sqlCommand = $"SELECT COUNT(*) FROM Employee";
            int count = _sqlConnection.QueryFirstOrDefault<int>(sqlCommand);
            return count;
        }
    }
}

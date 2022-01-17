
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AMIS.CORE.Entities;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using AMISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Modal;

namespace MISA.AMIS.INTRASTRUCTURE.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }
        /// <summary>
        /// hàm lấy tất cả bản ghi của Employee bao gồm cả thông tin tên vị trí
        /// </summary>
        /// <returns>Tất cả bản ghi của nhân viên</returns>
        /// @Author nmquang 19-12-2021
        public List<EmployeeDto> GetAllField()
        {
            var employees = _sqlConnection.Query<EmployeeDto>($"SELECT Employee.*, DepartmentCode, DepartmentName FROM Employee LEFT JOIN Department ON Employee.DepartmentId = Department.DepartmentId ORDER BY Employee.CreatedDate DESC");
            return employees.ToList();
        }
        /// <summary>
        /// tìm kiếm nhân viên theo mã code
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <returns>Trả về bản ghi đầu tiên được tìm thấy </returns>
        /// @Author nmquang 19-12-2021
        public Employee GetByEmployeeCode(string EmployeeCode)
        {
            var sqlCommand = $"SELECT * FROM Employee WHERE EmployeeCode = @EmployeeCode";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@EmployeeCode", EmployeeCode);
            // QueryFirstOrDefault - Lấy bản ghi đầu tiền từ câu lệnh truy vấn, nếu không có dữ liệu thì trả về null:
            var employee = _sqlConnection.QueryFirstOrDefault<Employee>(sqlCommand, param: parameters);
            return employee;
        }
        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới </returns>
        /// @Author nmquang 19-12-2021
        public string GetEmployeeCode()
        {
            var employeeCode = _sqlConnection.Query<string>($"SELECT CONCAT('NV', CONVERT(MAX(CONVERT(SUBSTRING(EmployeeCode,3,LENGTH(EmployeeCode)), int))+1, CHAR)) FROM Employee e;");
            return employeeCode.ToList()[0];
        }

        /// <summary>
        /// Method sử dụng để xóa nhiều bản ghi của bảng Employee
        /// </summary>
        /// <param name="listId">Danh sách id của tài sản</param>
        /// <returns>Trả về số bản ghi ảnh hưởng khi xóa</returns> 
        /// @Author nmquang 19-12-2021
        public int DeleteMultipleRecords(string listId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("listId", listId);

            var rowAffect = 0;
                rowAffect = _sqlConnection.Execute("Proc_DeleteMultipleEmployee", param: parameters, commandType: CommandType.StoredProcedure);
            return rowAffect;
        }
        /// <summary>
        /// Lọc dữ liệu kết hợp giữa search , filter, paging. 
        /// </summary>
        /// <param name="searchText">Ký tự muốn tìm kiếm</param>
        /// <param name="pageSize">Số lượng record trên 1 trang</param>
        /// <param name="m_PageIndex">Trang hiện tại</param>
        /// <returns>Danh sách nhân viên được tìm thấy</returns>
        /// @Author nmquang 19-12-2021
        public PagingData GetEmployeePaging(string searchText, int pageSize, int pageIndex)
        {
            //Tính lại pageIndex
            pageIndex = (pageIndex - 1) * pageSize;

            //Build tham số đầu vào cho store
            var input = searchText != null ? searchText : string.Empty;
            DynamicParameters parameters = new DynamicParameters();
            var totalRecord = 0;
            var totalPage = 0;
         
            parameters.Add("@m_EmployeeName", input);
            parameters.Add("@m_EmployeeCode", input);
            parameters.Add("@m_PageSize", pageSize);
            parameters.Add("@m_PageIndex", pageIndex);
            parameters.Add("@m_TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@m_TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var employees = _sqlConnection.Query<EmployeeDto>("Proc_GetEmployeePaging", param: parameters, commandType: CommandType.StoredProcedure).ToList();
            totalRecord = parameters.Get<int>("@m_TotalRecord");
            totalPage = parameters.Get<int>("@m_TotalPage");

       
            return new PagingData
            {
                Data = employees,
                TotalRecord = totalRecord,
                TotalPage = totalPage,
            };
        }

        /// <summary>
        /// Hàm tìm kiếm nhân viên theo tên và mã nhân viên
        /// </summary>
        /// <param name="text">kí tự tìm kiếm</param>
        /// <returns>danh sách nhân viên chứa kí tự tìm kiếm</returns>
        /// @Author nmquang 19-12-2021
        public List<EmployeeDto> SearchEmployee(string text)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Text", text);
            var sqlCommand = "" ;
            if (text == "" || text == null)
            {
                 sqlCommand = $"SELECT Employee.*, DepartmentCode, DepartmentName FROM Employee LEFT JOIN Department ON Employee.DepartmentId = Department.DepartmentId ORDER BY Employee.CreatedDate DESC";

            }
            else
            {
                sqlCommand = $"SELECT * FROM Employee WHERE(FullName LIKE CONCAT('%', @text, '%') OR EmployeeCode LIKE CONCAT('%', @text, '%'))";

            }
            var employees = _sqlConnection.Query<EmployeeDto>(sqlCommand, param: parameters);
            return employees.ToList();
        }

    }
}

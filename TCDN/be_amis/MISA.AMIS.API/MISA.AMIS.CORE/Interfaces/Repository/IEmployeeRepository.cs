
using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMISA.AMIS.CORE.Interfaces.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// hàm lấy tất cả bản ghi của Employee bao gồm cả thông tin tên vị trí
        /// </summary>
        /// <returns>Tất cả bản ghi của nhân viên</returns>
        /// @Author nmquang 19-12-2021
        public List<EmployeeDto> GetAllField();
        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới </returns>
        /// @Author nmquang 19-12-2021
        public string GetEmployeeCode();
        /// <summary>
        /// tìm kiếm nhân viên theo mã code
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <returns>Trả về bản ghi đầu tiên được tìm thấy </returns>
        /// @Author nmquang 19-12-2021
        public Employee GetByEmployeeCode(string EmployeeCode);

        /// <summary>
        /// Method sử dụng để xóa nhiều bản ghi của bảng Employee
        /// </summary>
        /// <param name="listId">Danh sách id của tài sản</param>
        /// <returns>Trả về số bản ghi ảnh hưởng khi xóa</returns>     
        /// @Author nmquang 19-12-2021
        int DeleteMultipleRecords(string listId);

        /// <summary>
        /// Lọc dữ liệu kết hợp giữa search , filter, paging. 
        /// </summary>
        /// <param name="searchText">Ký tự muốn tìm kiếm</param>
        /// <param name="pageSize">Số lượng record trên 1 trang</param>
        /// <param name="m_PageIndex">Trang hiện tại</param>
        /// <returns>Danh sách nhân viên được tìm thấy</returns>
        /// @Author nmquang 19-12-2021

        PagingData GetEmployeePaging(string searchText, int pageSize, int m_PageIndex);
        /// <summary>
        /// Hàm tìm kiếm nhân viên theo tên và mã nhân viên
        /// </summary>
        /// <param name="text">kí tự tìm kiếm</param>
        /// <returns>danh sách nhân viên chứa kí tự tìm kiếm</returns>
        /// @Author nmquang 19-12-2021
        public List<EmployeeDto> SearchEmployee(string textSearch);

    }


}

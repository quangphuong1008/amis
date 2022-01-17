using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.CORE.Interfaces.Services
{
    public interface IEmployeeService:IBaseService<Employee>
    {
        /// <summary>
        /// hàm lấy tất cả bản ghi của Employee bao gồm cả thông tin tên vị trí
        /// </summary>
        /// <returns>Tất cả bản ghi của nhân viên và thông tin trạng thái request</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult GetAllField();
        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới và thông tin trạng thái request</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult GetEmployeeCode();
        /// <summary>
        /// lấy dữ liệu đổ vào file excel
        /// </summary>
        /// <returns>dữ liệu nhân viên dạng IEnumerable(duyệt các phần tử) </returns>
        /// @Author nmquang 19-12-2021
        public IEnumerable<EmployeeDto> DataExcel(string textSearch);
        /// <summary>
        /// tìm kiếm nhân viên theo mã code
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <returns>Trả về bản ghi đầu tiên được tìm thấy và thông tin trạng thái request</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult GetByEmployeeCode(string EmployeeCode);

        /// <summary>
        /// Method sử dụng để xóa nhiều bản ghi của bảng Employee
        /// </summary>
        /// <param name="listId">Danh sách id của tài sản</param>
        /// <returns>Trả về số bản ghi ảnh hưởng khi xóa và thông tin trạng thái request</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult DeleteMultipleRecords(string listId);

        /// <summary>
        /// Lọc dữ liệu kết hợp giữa search , filter, paging. 
        /// </summary>
        /// <param name="searchText">Ký tự muốn tìm kiếm</param>
        /// <param name="pageSize">Số lượng record trên 1 trang</param>
        /// <param name="m_PageIndex">Trang hiện tại</param>
        /// <returns>Danh sách nhân viên được tìm thấy và thông tin trạng thái request</returns>
        /// @Author nmquang 19-12-2021

        public PagingData GetEmployeePaging(string searchText, int pageSize, int m_PageIndex);
        /// <summary>
        /// Hàm chuyển đổi tên giới tính phục vụ cho export Excel
        /// </summary>
        /// <param name="gender">id giới tính</param>
        /// <returns>name giới tính</returns>
        /// @Author nmquang 19-12-2021
        public String ChangeGender(string gender);
    }

    
}

using MISA.AMIS.CORE.Entities;
using AMISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Interfaces.Services;
using MISA.AMIS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AMIS.CORE.Modal;

namespace MISA.AMIS.CORE.Sevices
{
    public class EmployeeService:BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository):base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// hàm lấy tất cả bản ghi của Employee bao gồm cả thông tin tên vị trí
        /// </summary>
        /// <returns>Tất cả bản ghi của nhân viên và thông tin trạng thái request</returns>
        public ServiceResult GetAllField()
        {
            _serviceResult.Data = _employeeRepository.GetAllField();
            return _serviceResult;

        }
        /// <summary>
        /// lấy dữ liệu đổ vào file excel
        /// </summary>
        /// <returns>dữ liệu nhân viên dạng IEnumerable(duyệt các phần tử) </returns>
        public IEnumerable<EmployeeDto> DataExcel(string textSearch)
        {
            var data = _employeeRepository.SearchEmployee(textSearch);
            return data;
        }
        /// <summary>
        /// tìm kiếm nhân viên theo mã code
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <returns>Trả về bản ghi đầu tiên được tìm thấy và thông tin trạng thái request</returns>
        public ServiceResult GetByEmployeeCode(string EmployeeCode)
        {
            _serviceResult.Data = _employeeRepository.GetByEmployeeCode(EmployeeCode);
            return _serviceResult;
        }
        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới và thông tin trạng thái request</returns>
        public ServiceResult GetEmployeeCode()
        {
            _serviceResult.Data = _employeeRepository.GetEmployeeCode();
            return _serviceResult;
            
        }
        /// <summary>
        /// Method sử dụng để xóa nhiều bản ghi
        /// </summary>
        /// <param name="listId">Danh sách id</param>
        /// <returns>Trả về số bản ghi ảnh hưởng khi xóa và thông tin request</returns>
        /// CreatedBy : NMQuang (22/12/2021)
        public ServiceResult DeleteMultipleRecords(string listId)
        {
            _serviceResult.Data = _employeeRepository.DeleteMultipleRecords(listId);
            return _serviceResult;
        }
        /// <summary>
        /// Lọc dữ liệu kết hợp giữa search , filter, paging. 
        /// </summary>
        /// <param name="searchText">Ký tự muốn tìm kiếm</param>
        /// <param name="pageSize">Số lượng record trên 1 trang</param>
        /// <param name="m_PageIndex">Trang hiện tại</param>
        /// <returns>Danh sách nhân viên được tìm thấy và thông tin trạng thái request</returns>

        public PagingData GetEmployeePaging(string searchText, int pageSize, int m_PageIndex)
            {
            return _employeeRepository.GetEmployeePaging(searchText, pageSize, m_PageIndex);
        }



        public String ChangeGender(string gender)
        {
            if(gender == "0")
            {
                return MyResource.Resource.Male;
            }else if(gender == "1")
            {
                return MyResource.Resource.FeMale;
            };
            return MyResource.Resource.Other;
        }
    }
}

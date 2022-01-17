
using AMISA.AMIS.CORE.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Interfaces.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Api
{

    /// <summary>
    /// Thiếu try catch
    /// </summary>
    public class EmployeeController : AMISControlller<Employee>
    {
        IEmployeeService _employeeService;
 
        public EmployeeController(IEmployeeService employeeService, IBaseRepository<Employee> baseRepository) : base(employeeService, baseRepository)
        {
            _employeeService = employeeService;
        
        }

        /*
         *  Override lại GetAll của Employee
         *  Trả về thêm Field DepartmentName
         *  @Author nmquang 19-12-2021
         */
        public override IActionResult Get()
        {
            try
            {
                var employeeDtos = _employeeService.GetAllField();
                return Ok(employeeDtos);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
          
        }
        /// <summary>
        /// Phương thức Get. Lấy Mã mới của đối tượng không chứa trong cơ sở dữ liệu
        /// </summary>
        /// <returns>Mã code mới. Nếu gặp lỗi trả về thông tin lỗi</returns>
        [HttpGet("NewCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var employeeCode = _employeeService.GetEmployeeCode();
                return Ok(employeeCode);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
            
        }
        /// <summary>
        /// Phương thức Get. Lấy thông tin của đối tượng trong cơ sở dữ liệu
        /// </summary>
        /// <returns>Thông tin của đối tượng. Nếu gặp lỗi trả về thông tin lỗi</returns>
        /// @Author nmquang 19-12-2021

        [HttpGet("GetByCode")]
        public IActionResult GetByEmployeeCode(string EmployeeCode)
        {
            try
            {
                //nếu null hoặc '' thì ko xử lí gì cả , trả về luon
                if (String.IsNullOrEmpty(EmployeeCode))
                {
                    return BadRequest();
                }
                var employee = _employeeService.GetByEmployeeCode(EmployeeCode);
                return Ok(employee);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
            
        }


        /*
        Không trả trực tiếp về ex => lộ lỗi ở BE và database => dễ bị khai thác
        code = 500;
        data = null;
        message = lỗi hệ thống.
        Tạo 1 bảng trong db, tên là LogExeption(id,method,detail)
        method = pagingFilter
        id tự sinh
        detail = ex
        */
        /// <summary>
        /// Phương thức Delete. Xóa nhiều nhân viên
        /// Truyền qua body, phải truyền vào kiểu đối tượng, ko được phép truyền kiểu dữ liệu nguyên thủy
        /// </summary>
        /// <param name="listId">danh sách id nhân viên cần xóa</param>
        /// <returns>trả về 0 nếu xóa thành công</returns>
        /// @Author nmquang 19-12-2021
        [HttpDelete("DeleteMulti/{listId}")]
        public IActionResult DeleteMultipleRecord(string listId)
        {
            //nếu xóa tất cả listId = list id = 0000-0000-0000-000-0000
            try
            {
                var rowAffect = _employeeService.DeleteMultipleRecords(listId);
                return Ok(rowAffect);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
            
        }

        /// <summary>
        /// Phương thức Get. Lọc dữ liệu kết hợp giữa search , filter, paging. 
        /// </summary>
        /// <param name="searchText">Ký tự muốn tìm kiếm</param>
        /// <param name="pageSize">Số lượng record trên 1 trang</param>
        /// <param name="pageIndex">Trang hiện tại</param>
        /// <returns>Danh nhân viên sản được tìm thấy</returns>
        ///  @Author nmquang 19-12-2021
        [HttpGet("Filter")]
        public object GetEmployeePaging(string searchText, int pageSize, int pageIndex)
        {
            try
            {
                return _employeeService.GetEmployeePaging(searchText, pageSize, pageIndex);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
            
        }
        /// <summary>
        /// Phương thức Get. Lấy dữ liệu export ra file excel
        /// </summary>
        /// <returns>fiel excel</returns>
        ///  @Author nmquang 19-12-2021
        [HttpGet("ExportExcel")]
        public IActionResult Export(string searchText)
        {
            //Tạo đối tượng memoryStream
            
            var stream = new MemoryStream();
            var result = _employeeService.DataExcel(searchText);
            // đường dẫn đến file gốc
            var filePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\MISA.AMIS.CORE\ExcelTemplate\Danh_sach_nhan_vien.xlsx"));
            FileInfo existingFile = new FileInfo(filePath);
            // bản quyền
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                
                ExcelWorksheet sheet = package.Workbook.Worksheets[0];
                // đổ dữ liệu vào sheet
                sheet.Cells[2, 1].Value = DateTime.Now.ToString("dd-MM-yyyy");
                int rowId = 4;
                int stt = 1;
                foreach (var row in result)
                {
                    sheet.Cells[rowId, 1].AutoFitColumns(10, 10);
                    for (int i = 1; i <= 9; i++)
                    {
                        // Thêm border cho cột
                        sheet.Cells[rowId, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[rowId, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[rowId, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[rowId, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        // Thêm width vs height cho cột
                        sheet.Cells[rowId, i + 1].AutoFitColumns(20, 20);
                        sheet.Cells[rowId, i + 1].Merge = true;
                    }
                    sheet.Cells[rowId, 1].Value = stt;
                    sheet.Cells[rowId, 2].Value = row.EmployeeCode;
                    sheet.Cells[rowId, 3].Value = row.FullName;
                    sheet.Cells[rowId, 4].Value = _employeeService.ChangeGender(row.Gender);
                    sheet.Cells[rowId, 5].Value = row.DateOfBirth;
                    sheet.Cells[rowId, 6].Value = row.PositionName;
                    sheet.Cells[rowId, 7].Value = row.DepartmentName;
                    sheet.Cells[rowId, 8].Value = row.BankAccount;
                    sheet.Cells[rowId, 9].Value = row.BankName;
                    stt++;
                    rowId++;
                }
                stream = new MemoryStream(package.GetAsByteArray());
            }
            stream.Position = 0;
            var fileName = $"DanhSachNhanVien_{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }
    }
}



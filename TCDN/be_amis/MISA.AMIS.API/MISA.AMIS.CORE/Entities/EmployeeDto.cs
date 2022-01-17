using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Entities
{
    /// <summary>
    /// Class chứa thông tin chi tiết nhân viên
    /// </summary>
    /// @Author nmquang 19-12-2021
    public class EmployeeDto : BaseEntity
    {
        /// <summary>
        /// id duy nhất của mỗi nhân viên
        /// </summary>
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [DisplayName("Tên Nhân Viên")]
        public string FullName { get; set; }
        /// <summary>
        /// Giới tính nhân viên
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// Ngày sinh nhân viên
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Số căn cước công dân của nhân viên
        /// </summary>
        public string IdentityNumber { get; set; }
        /// <summary>
        /// Tên Vị trí của nhân viên
        /// </summary>
        public string PositionName { get; set; }
        /// <summary>
        /// Id xác định phòng ban của nhân viên
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Tên phòng ban của nhân viên
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Email của nhân viên
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Ngày đăng ký căn cước công dân 
        /// </summary>
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp căn cước công dân
        /// </summary>
        public string IdentityPlace { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public string BankAccount { get; set; }
        /// <summary>
        /// Địa chỉ của nhân viên
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Số điện thoại bàn
        /// </summary>
        public string LandlinePhoneNumber { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// Tên chi nhánh ngân hàng
        /// </summary>
        public string BankBranch { get; set; }
    }
}

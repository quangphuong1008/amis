using MISA.AMIS.CORE.Modal;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Entities
{
    public class AccountObject:BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public Guid AccountObjectId { get; set; }
        /// <summary>
        /// Mã số thuế
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string TaxCode { get; set; }
        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string AccoubtObjectCode { get; set; }
        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string AccountObjectName { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string Phone { get; set; }
        /// <summary>
        /// Tên miền website
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string Website { get; set; }
        /// <summary>
        /// Danh sách id nhóm nhà cung cấp
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string AccountObjectGroupListId { get; set; }
        /// <summary>
        /// địa chỉ
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public List<DataAccountObjectAddress> Address { get; set; }
        public string AddressJson { get; set; }
        /// <summary>
        /// Id của nhân viên
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public Guid? EmployeeId { get; set; }
        /// <summary>
        /// Xưng hô
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string Prefix { get; set; }
        /// <summary>
        /// Họ và tên người liên hệ
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string EinvoiceContactName { get; set; }
        /// <summary>
        /// Số điện thoại người liên hệ
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string EinvoiceContactMobile { get; set; }
        /// <summary>
        /// Email người liên hệ
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string EinvoiceContactEmail { get; set; }
        /// <summary>
        /// Đại diện theo PL
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string LegalRepresentative { get; set; }
        /// <summary>
        /// Id của điều khoản
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public Guid? PaymentTermId { get; set; }
        /// <summary>
        /// Tài khoản nợ công phải trả
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string PayAccount { get; set; }
        /// <summary>
        /// NỢ tối đa
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string MaximizeDebtAmount { get; set; }
        /// <summary>
        /// Số ngày được nợ
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public int DueTime { get; set; }
        /// <summary>
        /// Danh sách tài khoản ngân hàng được nhận từ clien
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public List<DataAccountObjectBank> AccountObjectBankAccount { get; set; }
        /// <summary>
        /// Danh sách tài khoản ngân hàng gửi từ dtb lên client
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string AccountObjectBankAccountJson { get; set; }
        /// <summary>
        /// Danh sách địa chỉ
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string AccountObjectShippingAddress { get; set; }
        /// <summary>
        /// Đất Nước
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)

        public string Country { get; set; }
        /// <summary>
        /// Quận huyện
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string District { get; set; }
        /// <summary>
        /// Phường xã
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string WardOrCommune { get; set; }
        /// <summary>
        /// Thành phố
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string ProvinceOrCity { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string Description { get; set; }
        /// <summary>
        /// Người tạo bản ghi
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string ModifiedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public DateTime ModifiedDate { get; set; }
    }
}

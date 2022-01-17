
using MISA.AMIS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.CORE.Entities
{

    /// <summary>
    /// Class attribute đánh dấu thông tin bắt buộc nhập
    /// </summary>
    /// @Author nmquang 19-12-2021
    /// 
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute
    {
    }
    /// <summary>
    /// Attribute dùng để kiểm tra trùng lặp dữ liệu
    /// </summary>

    [AttributeUsage(AttributeTargets.Property)]
    public class CheckDuplicate : Attribute
    {
    }

    ///// <summary>
    ///// Attribute kiểm tra độ dài tối đa có thể nhập
    ///// </summary>
    //[AttributeUsage(AttributeTargets.Property)]
    //public class MaxLenght : Attribute
    //{
    //    public int Value { get; set; }
    //    public string ErrorMsg { get; set; }
    //    public MaxLenght(int lenght , string errorMsg)
    //    {
    //        this.Value = lenght;
    //        this.ErrorMsg = errorMsg;
    //    }
    //}

    /// <summary>
    /// Attribute mô tả tên trường
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayName : Attribute
    {
        public string PropertyName { get; set; }
        public DisplayName(string propertyName)
        {
            this.PropertyName = propertyName;
        }
    }

    /// <summary>
    /// Attribute dùng để check email hợp lệ hay không
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidEmail : Attribute
    {
    }
    /// <summary>
    /// Atribute dùng để check Date xem có vượt quá thời gian hiện tại hay không
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidDate : Attribute
    {
    }

    public class BaseEntity
    {
        #region Property
        /// <summary>
        /// Dùng để đánh dấu phục vụ cho việc thêm , sửa , xóa
        /// Thêm - 1
        /// Sửa - 2
        /// Xóa - 3
        /// </summary>
        public EntityState EntityState { get; set; } = EntityState.AddNew;
        #endregion
    }
}

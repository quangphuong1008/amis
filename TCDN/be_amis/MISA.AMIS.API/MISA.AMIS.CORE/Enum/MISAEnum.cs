using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.CORE.Entity
{
    /// <summary>
    /// Misa code để xác định trạng thái của việc validate
    /// </summary>
    /// @Author nmquang 19-12-2021
    public enum MISAEnum
    {
        /// <summary>
        /// Dữ liệu hợp lệ
        /// </summary>
        IsValid = 100,
        /// <summary>
        /// Dự liệu không hợp lệ
        /// </summary>
        NotValid = 900,
        /// <summary>
        /// Thành công
        /// </summary>
        Success = 200,
        /// <summary>
        /// Exception   
        /// </summary>
        Exception = 500
    }

    /// <summary>
    /// Xác định trạng thái của Object
    /// </summary>
    public enum EntityState
    {
        AddNew = 1,
        Update = 2,
        Delete = 3
    }
    
}

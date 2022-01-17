using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.CORE.Entities
{
    /// <summary>
    /// Class chứa thông tin Phòng ban
    /// </summary>
    /// @Author nmquang 19-12-2021
    /// 
    public class Department:BaseEntity
    {   
        /// <summary>
        /// Id xác định Vị trí
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Mã vị trí
        /// </summary>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string DepartmentName { get; set; }

    }
}

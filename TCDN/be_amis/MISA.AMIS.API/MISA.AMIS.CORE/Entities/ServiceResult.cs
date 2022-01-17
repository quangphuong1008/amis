
using MISA.AMIS.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.CORE.Entities
{
    public class ServiceResult
    {

        #region Property
        /// <summary>
        /// dữ liệu trả về trả về cho client sử dụng
        /// </summary>
        /// @Author nmquang 19-12-2021
        public object Data { get; set; }

        /// <summary>
        /// Thông tin mô tả dữ liệu trả về
        /// </summary>
        public string Messeger { get; set; }

        /// <summary>
        /// Mã code biểu diễn thông tin trả về
        /// </summary>
        public MISAEnum MisaCode { get; set; }
        #endregion
    }
}

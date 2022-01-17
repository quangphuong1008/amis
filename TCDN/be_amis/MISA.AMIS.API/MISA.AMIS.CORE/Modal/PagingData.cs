using MISA.AMIS.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Modal
{
    public class PagingData
    {
        public List<EmployeeDto> Data { get; set; }
        public int? TotalRecord { get; set; }
        public int? TotalPage { get; set; }

 

    }
}

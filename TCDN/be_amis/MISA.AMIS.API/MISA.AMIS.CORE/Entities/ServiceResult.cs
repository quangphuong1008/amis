using AMIS.Core.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.Core.Entities
{
    public class ServiceResult
    {
        public string devMsg { get; set; }
        public string userMsg { get; set; }
        public MISACode errorCode { get; set; }
        public string moreInfo { get; set; }
        public object data { get; set; }
        public bool Success { get; set; } = true;
    }
}

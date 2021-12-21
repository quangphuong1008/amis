using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.Core.Entities
{

    /// <summary>
    /// Class attribute đánh dấu thông tin bắt buộc nhập
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AMISRequired: Attribute
    {
        public string ErroMsg { get; set; }
        public AMISRequired(string errorMsg = null)
        {
            ErroMsg = errorMsg;
        }
    }
}

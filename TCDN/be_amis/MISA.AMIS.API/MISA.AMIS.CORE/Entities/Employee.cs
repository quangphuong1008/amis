using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.Core.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string IdentityNumber { get; set; }
        public string PositionName { get; set; }
        public Guid DepartmentId { get; set; }
        public string Email { get; set; }
        public DateTime? IdentityDate { get; set; }
        public string IdentityPlace { get; set; }
        public string PhoneNumber { get; set; }
        public string BankAccount { get; set; }
        public string Address { get; set; }
        public string LandlinePhoneNumber { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
    }
}

using AMIS.Core.Entities;
using AMIS.Core.Interfaces.Repository;
using AMIS.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.Core.Sevices
{
    public class DepartmentService: BaseService<Department>, IDepartmentService
    {
        IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository) :base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
    }
}

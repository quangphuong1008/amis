using MISA.AMIS.CORE.Entities;

using MISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.CORE.Sevices
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


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Api
{
    
    public class DepartmentController : AMISControlller<Department>
    {
        IDepartmentService _departmentService;
        //IDepartmentRepository departmentRepository;
        public DepartmentController(IDepartmentService departmentService, IBaseRepository<Department> baseRepository) : base(departmentService, baseRepository)
        {
            _departmentService = departmentService;
        } 

    }
}

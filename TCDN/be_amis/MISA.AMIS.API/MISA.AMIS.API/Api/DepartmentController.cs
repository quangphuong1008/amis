using AMIS.Core.Entities;
using AMIS.Core.Interfaces.Repository;
using AMIS.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMIS.Api.Api
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

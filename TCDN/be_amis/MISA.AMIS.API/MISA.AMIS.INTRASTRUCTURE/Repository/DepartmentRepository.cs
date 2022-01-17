
using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.INTRASTRUCTURE.Repository
{
    public class DepartmentRepository:BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}

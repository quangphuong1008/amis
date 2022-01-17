using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Api
{
    public class AccountObjectController: AMISControlller<AccountObject>
    {
        IAccountObjectService _accountObjectService;
        //IDepartmentRepository departmentRepository;
        public AccountObjectController(IAccountObjectService accountObjectService, IBaseRepository<AccountObject> baseRepository) : base(accountObjectService, baseRepository)
        {
            _accountObjectService = accountObjectService;
        }
    }
}

using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Sevices
{
    public class AccountObjectService: BaseService<AccountObject>, IAccountObjectService
    {
        IAccountObjectRepository _accountObjectRepository;
        public AccountObjectService(IAccountObjectRepository accountObjectRepository) : base(accountObjectRepository)
        {
            _accountObjectRepository = accountObjectRepository;
        }
    }
}

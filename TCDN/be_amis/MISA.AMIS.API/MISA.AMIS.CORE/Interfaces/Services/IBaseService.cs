using AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.Core.Interfaces.Services
{
    public interface IBaseService<TEntity>
    {
        public ServiceResult GetAll();
        public ServiceResult GetById(Guid entityId);
        public ServiceResult Insert(TEntity entity);
        public ServiceResult Update(TEntity entity, Guid entityId);
        public ServiceResult Delete(Guid entityId);
    }
}

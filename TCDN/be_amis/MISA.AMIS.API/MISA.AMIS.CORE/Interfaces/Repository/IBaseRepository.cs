using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.Core.Interfaces.Repository
{
    public interface IBaseRepository<TEntity>
    {
        public List<TEntity> GetAll();
        public TEntity GetById(Guid entityId);
        public int Insert(TEntity entity);
        public int Update(TEntity entity, Guid entityId);
        public int Delete(Guid entityId);
    }
}

using MISA.AMIS.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.CORE.Interfaces.Services
{
    public interface IBaseService<TEntity>
    {
        /// <summary>
        /// hàm lấy toàn bộ bản ghi của đối tượng
        /// </summary>
        /// <returns>trả về toàn bộ bản ghi của đối tượng</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult GetAll();
        /// <summary>
        /// lấy bản ghi của đối tượng theo ID
        /// </summary>
        /// <param name="entityId">ID đối tượng</param>
        /// <returns>trả về  đối tượng, nếu không thấy thì trả về null</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult GetById(Guid entityId);
        /// <summary>
        /// thêm mới 1 đối tượng
        /// </summary>
        /// <param name="entity">các thuộc tính của đối tượng </param>
        /// <returns>trả về số bản ghi bị ảnh hưởng</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult Insert(TEntity entity);
        /// <summary>
        /// sửa đối tượng
        /// </summary>
        /// <param name="entity">các thuộc tính của đối tượng</param>
        /// <param name="entityId">id của đối tượng</param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult Update(TEntity entity, Guid entityId);
        /// <summary>
        /// xóa đối tượng
        /// </summary>
        /// <param name="entityId">id của đối tượng</param>
        /// <returns>trả về số bản ghi bị ảnh hưởng</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult Delete(Guid entityId);
    }
}

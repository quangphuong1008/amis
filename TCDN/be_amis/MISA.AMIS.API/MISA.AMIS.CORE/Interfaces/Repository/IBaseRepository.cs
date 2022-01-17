using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.CORE.Interfaces.Repository
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// hàm lấy tất cả bản ghi của đối tượng
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// @Author nmquang 19-12-2021
        public List<TEntity> GetAll();
        /// <summary>
        /// Hàm lấy Đối tượng qua ID
        /// </summary>
        /// <param name="entityId">ID đối tượng</param>
        /// <returns>Đối tượng có ID tương ứng</returns>
        /// @Author nmquang 19-12-2021
        public TEntity GetById(Guid entityId);
        /// <summary>
        /// Hàm thêm mới đối tượng
        /// </summary>
        /// <param name="entity">Thông tin đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// @Author nmquang 19-12-2021
        public int Insert(TEntity entity);
        /// <summary>
        /// Hàm cập nhật thông tin của đối tượng
        /// </summary>
        /// <param name="entity">thông tin sau khi thay đổi của đối tương</param>
        /// <param name="entityId">Id của đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// @Author nmquang 19-12-2021
        public int Update(TEntity entity, Guid entityId);
        /// <summary>
        /// Hàm xóa đối tượng
        /// </summary>
        /// <param name="entityId">id của đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// @Author nmquang 19-12-2021
        public int Delete(Guid entityId);


        /// <summary>
        /// Lấy bản ghi dựa vào thuộc tính (Property)
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <param name="property">Thông tin thuộc tính</param>
        /// <returns>Trả về 1 bản ghi đầu tiên được tìm thấy</returns>
        /// @Author nmquang 19-12-2021
        object GetEntityByProperty(TEntity entity, PropertyInfo property);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMIS.Core.Interfaces.Services;
using AMIS.Core.Interfaces.Repository;
using AMIS.Core.Entities;

namespace AMIS.Core.Sevices
{
    public class BaseService<TEntity>:IBaseService<TEntity>
    {
        protected IBaseRepository<TEntity> _baseRepository;
        protected ServiceResult _serviceResult;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult();
        }
        public ServiceResult GetAll()
        {
            _serviceResult.data = _baseRepository.GetAll();
            return _serviceResult;
        }

        public ServiceResult GetById(Guid entityId)
        {
            _serviceResult.data = _baseRepository.GetById(entityId);
            return _serviceResult;
        }

        public ServiceResult Insert(TEntity entity)
        {
            // Xử lý nghiệp vụ:
            var isValid = ValidateObject(entity);
            if (isValid == true)
            {
                isValid = ValidateObjectCustom(entity);
            }
            // Thực hiện cập nhật dữ liệu:
            if (isValid == true)
                _serviceResult.data = _baseRepository.Insert(entity);
            else
            {
                _serviceResult.Success = false;
                _serviceResult.devMsg = Resource.InvalidData;
            }
            return _serviceResult;
        }

        public ServiceResult Update(TEntity entity, Guid entityId)
        {
            var isValid = ValidateObject(entity);
            if (isValid == true)
            {
                isValid = ValidateObjectCustom(entity);
            }
            // Thực hiện cập nhật dữ liệu:
            if (isValid == true)
                _serviceResult.data = _baseRepository.Update(entity, entityId);
            else
            {
                _serviceResult.Success = false;
                _serviceResult.devMsg = Resource.InvalidData;
            }
            return _serviceResult;
        }


        public ServiceResult Delete(Guid entityId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Hàm thực hiện validate dữ liệu - cho phép overrider
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool ValidateObject(TEntity entity)
        {
            // Validate chung:
            // 1. Bắt buộc nhập:
            var properties = entity.GetType().GetProperties();
            // 2. Duyệt các property:
            foreach (var prop in properties)
            {
                // value của prop:
                var propValue = prop.GetValue(entity);
                var propName = prop.Name;
                var propType = prop.PropertyType;
                var requiredAttributes = prop.GetCustomAttributes(typeof(AMISRequired), false);
                if (requiredAttributes.Length > 0)
                {
                    if (propType == typeof(string) && (propValue == null || string.IsNullOrEmpty(propValue.ToString())))
                    {
                        var attributeMsg = (requiredAttributes[0] as AMISRequired).ErroMsg;
                        attributeMsg = (attributeMsg == null ? $"{propName} is not null" : attributeMsg);
                        _serviceResult.Success = false;
                        _serviceResult.userMsg = $"{attributeMsg}";

                        return false;
                    }
                }
            }
            return true;
        }

        protected virtual bool ValidateObjectCustom(TEntity entity)
        {
            return true;
        }
    }
}

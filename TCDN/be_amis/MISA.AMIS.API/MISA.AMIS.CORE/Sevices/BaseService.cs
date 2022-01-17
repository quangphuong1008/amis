using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Entity;
using MISA.AMIS.CORE.Interfaces.Services;
using MISA.AMIS.CORE.Interfaces.Repository;
using System.ComponentModel.DataAnnotations;

namespace MISA.AMIS.CORE.Sevices
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        protected IBaseRepository<TEntity> _baseRepository;
        protected ServiceResult _serviceResult;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult() { MisaCode = MISAEnum.Success };
        }

        /// <summary>
        /// hàm lấy toàn bộ bản ghi của đối tượng
        /// </summary>
        /// <returns>trả về toàn bộ bản ghi của đối tượng</returns>
        public ServiceResult GetAll()
        {
            _serviceResult.Data = _baseRepository.GetAll();
            return _serviceResult;
        }
        /// <summary>
        /// lấy bản ghi của đối tượng theo ID
        /// </summary>
        /// <param name="entityId">ID đối tượng</param>
        /// <returns>trả về  đối tượng, nếu không thấy thì trả về null</returns>
        public ServiceResult GetById(Guid entityId)
        {
            _serviceResult.Data = _baseRepository.GetById(entityId);
            return _serviceResult;
        }
        /// <summary>
        /// thêm mới 1 đối tượng
        /// </summary>
        /// <param name="entity">các thuộc tính của đối tượng </param>
        /// <returns>trả về số bản ghi bị ảnh hưởng</returns>
        public ServiceResult Insert(TEntity entity)
        {
            entity.EntityState = EntityState.AddNew;
            // Xử lý nghiệp vụ:
            var isValid = ValidateObject(entity);
            if (isValid == true)
            {
                isValid = ValidateObjectCustom(entity);
            }
            // Thực hiện cập nhật dữ liệu:
            if (isValid == true)
                _serviceResult.Data = _baseRepository.Insert(entity);
            else
            {
                _serviceResult.MisaCode = MISAEnum.NotValid;

                _serviceResult.Messeger = MyResource.Resource.Msg_IsNotValid;
            }
            return _serviceResult;
        }
        /// <summary>
        /// sửa đối tượng
        /// </summary>
        /// <param name="entity">các thuộc tính của đối tượng</param>
        /// <param name="entityId">id của đối tượng</param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        public ServiceResult Update(TEntity entity, Guid entityId)
        {
            entity.EntityState = EntityState.Update;
            var isValid = ValidateObject(entity);
            if (isValid == true)
            {
                isValid = ValidateObjectCustom(entity);
            }
            // Thực hiện cập nhật dữ liệu:
            if (isValid == true)
                _serviceResult.Data = _baseRepository.Update(entity, entityId);
            else
            {
                _serviceResult.MisaCode = MISAEnum.NotValid;
                _serviceResult.Messeger = MyResource.Resource.Msg_IsNotValid;
            }
            return _serviceResult;
        }

        /// <summary>
        /// xóa đối tượng
        /// </summary>
        /// <param name="entityId">id của đối tượng</param>
        /// <returns>trả về số bản ghi bị ảnh hưởng</returns>
        public ServiceResult Delete(Guid entityId)
        {
            _serviceResult.Data = _baseRepository.Delete(entityId);
            return _serviceResult;
        }


 

        /// <summary>
        /// Hàm thực hiện validate dữ liệu 
        /// </summary>
        /// <param name="entity">Thông tin của nhân viên chứa các property</param>
        /// <returns>True: Dữ liệu hợp lý, False: Dữ liệu chưa hợp lý</returns>

        protected bool ValidateObject(TEntity entity)
        {
            var messArrayError = new List<string>();
            var isValidate = true;
            //Đọc các Property : 
            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(entity);
                var displayName = string.Empty; // chuỗi rỗng
                var displayNameAttributes = property.GetCustomAttributes(typeof(DisplayName), true);  // đọc atribute có attribute = DisplayName
                if (displayNameAttributes.Length > 0)
                {
                    displayName = (displayNameAttributes[0] as DisplayName).PropertyName; // get property name có atrr là DisplayName
                }
                //Kiểm tra xem có attribute cần phải validate không : 
                var requiredAtrribute = property.GetCustomAttributes(typeof(Required), true);
                if (requiredAtrribute.Length > 0)
                {
                    //Check bắt buộc nhập
                    if (String.IsNullOrEmpty(Convert.ToString(propertyValue)))
                    {
                        messArrayError.Add(String.Format(MyResource.Resource.Msg_Empty, displayName));
                        isValidate = false;

                    }
                }
                var duplicateAttribute = property.GetCustomAttributes(typeof(CheckDuplicate), true);
                if (duplicateAttribute.Length > 0)
                {
                    //Check trùng dữ liệu
                    var entityDuplicate = _baseRepository.GetEntityByProperty(entity, property);
                    if (entityDuplicate != null)
                    {
                        isValidate = false;
                        messArrayError.Add(String.Format(MyResource.Resource.Msg_Duplicate, displayName));
                    }
                }
                var valiDateAttribute = property.GetCustomAttributes(typeof(ValidDate), true);
                if (valiDateAttribute.Length > 0)
                {
                    // check ngày tháng vượt quá ngày hiện tại
                    DateTime today = DateTime.Today;
                   if (propertyValue!= null && Convert.ToDateTime(propertyValue) > today)
                    {
                        messArrayError.Add(String.Format(MyResource.Resource.Msg_DateNotValid, displayName));
                        isValidate = false;
                    }
                }

                var validEmail = property.GetCustomAttributes(typeof(ValidEmail), true);
                if (validEmail.Length > 0)
                {
                    // check email
                    Boolean isNullorEmpty = String.IsNullOrEmpty(Convert.ToString(propertyValue));
                    Boolean isEmail = new EmailAddressAttribute().IsValid(propertyValue); // thư viện
                    if (!isNullorEmpty && !isEmail)
                    {
                        messArrayError.Add(String.Format(MyResource.Resource.Msg_EmailNotValid, displayName));
                        isValidate = false;
                    }
                    
                }
                

            }

            if (messArrayError.Count >= 1)
            {
                _serviceResult.Data = new
                {
                    userMsg = messArrayError,
                    devMsg = messArrayError
                };
                _serviceResult.MisaCode = MISAEnum.NotValid;
                _serviceResult.Messeger = MyResource.Resource.Msg_IsNotValid;
            }
            if (isValidate == true)
                isValidate = ValidateObjectCustom(entity);
            return isValidate;
        }

        protected virtual bool ValidateObjectCustom(TEntity entity)
        {
            return true;
        }

       
    }
}

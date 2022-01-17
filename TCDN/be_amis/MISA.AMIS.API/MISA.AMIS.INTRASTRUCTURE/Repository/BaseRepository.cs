using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Entity;
using MISA.AMIS.CORE.Interfaces.Repository;
using MySql.Data.MySqlClient;
using MySqlConnector;
using MySqlConnection = MySql.Data.MySqlClient.MySqlConnection;

namespace MISA.AMIS.INTRASTRUCTURE.Repository
{
    // public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region DECLARE

        /// <summary>
        /// Phục vụ cho việc lấy chuỗi kết nỗi được lưu trữ trong file appsetting.json
        /// </summary>
        IConfiguration _configuration;

        /// <summary>
        /// Chuỗi kết nối
        /// </summary>
        string _connectionString = string.Empty;

        /// <summary>
        /// Phục vụ cho việc mở kết nối xuống database
        /// </summary>
        protected IDbConnection _sqlConnection = null;

        /// <summary>
        /// Tên table để biết được thao tác với table nào trong database
        /// </summary>
        protected string _tableName;
        #endregion



        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MISAAmisConnectionString");
            _sqlConnection = new MySqlConnection(_connectionString);
            _sqlConnection.Open();
            _tableName = typeof(TEntity).Name;
        }

        /// <summary>
        /// hàm lấy tất cả bản ghi của đối tượng
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// @Author nmquang 19-12-2021
        public virtual List<TEntity> GetAll()
        {
            var employees = _sqlConnection.Query<TEntity>($"SELECT * FROM {_tableName}");
            return employees.ToList();
        }
        /// <summary>
        /// Hàm lấy Đối tượng qua ID
        /// </summary>
        /// <param name="entityId">ID đối tượng</param>
        /// <returns>Đối tượng có ID tương ứng</returns>
        /// @Author nmquang 19-12-2021
        public TEntity GetById(Guid entityId)
        {
            var sqlCommand = $"SELECT * FROM {_tableName} WHERE {_tableName}Id = @{_tableName}Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{_tableName}Id", entityId);
            // QueryFirstOrDefault - Lấy bản ghi đầu tiền từ câu lệnh truy vấn, nếu không có dữ liệu thì trả về null:
            var employee = _sqlConnection.QueryFirstOrDefault<TEntity>(sqlCommand, param: parameters);
            return employee;
        }
        /// <summary>
        /// Hàm thêm mới đối tượng
        /// </summary>
        /// <param name="entity">Thông tin đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// @Author nmquang 19-12-2021
        public virtual int Insert(TEntity entity)
        {
            // Khai báo chuỗi SQL động:
            var sqlColumDynamic = "";
            var sqlParamDynamic = "";

            var dynamicParams = new DynamicParameters();

            // Lấy ra các properties của đối tượng:
            var props = entity.GetType().GetProperties();
            // Duyệt từng property:
            foreach (var prop in props)
            {
                if( prop.Name == "EntityState")
                {

                }
              
                else
                {
                    // Lấy tên của property:
                    var propName = prop.Name;
                    // Lấy ra giá trị của property:
                    var propValue = prop.GetValue(entity);

                    // Lấy ra kiểu dữ liệu của property:
                    var propType = prop.PropertyType;

                    if (propName == $"{_tableName}Id" && propType == typeof(Guid))
                    {
                        propValue = Guid.NewGuid();
                    }

                    // Bổ sung vào chuỗi Column động:
                    sqlColumDynamic += $"{propName},";
                    sqlParamDynamic += $"@{propName},";
                    dynamicParams.Add($"@{propName}", propValue);
                }
                
            };
            
            // Cắt phần tử dấu phẩy cuối cùng trong chuối:
            sqlColumDynamic = sqlColumDynamic.Substring(0, sqlColumDynamic.Length - 1);
            sqlParamDynamic = sqlParamDynamic.Substring(0, sqlParamDynamic.Length - 1);
            var className = entity.GetType().Name;
            var sqlDynamic = $"INSERT INTO {className}({sqlColumDynamic}) VALUES({sqlParamDynamic})";

            var res = _sqlConnection.Execute(sqlDynamic, param: dynamicParams, commandType: CommandType.Text);
            return res;
        }
        /// <summary>
        /// Hàm cập nhật thông tin của đối tượng
        /// </summary>
        /// <param name="entity">thông tin sau khi thay đổi của đối tương</param>
        /// <param name="entityId">Id của đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// @Author nmquang 19-12-2021
        public int Update(TEntity entity, Guid entityId)
        {
            // Khai báo chuỗi SQL động:
            var sqlValue = "";
            var dynamicParams = new DynamicParameters();
            // Lấy ra các properties của đối tượng:
            var props = entity.GetType().GetProperties();
            // Duyệt từng property:
            foreach (var prop in props)
            {
                if (prop.Name == "EntityState")
                {

                }
                else
                {
                    // Lấy tên của property:
                    var propName = prop.Name;
                    // Lấy ra giá trị của property:
                    var propValue = prop.GetValue(entity);

                    // Lấy ra kiểu dữ liệu của property:
                    var propType = prop.PropertyType;

                    if ((propName == $"{_tableName}Id") && propType == typeof(Guid))
                    {
                        continue;
                    }

                    sqlValue += $"{propName}=@{propName},";
                    dynamicParams.Add($"@{propName}", propValue);
                }
            };
            // bỏ dấu phẩy cuối cùng
            sqlValue = sqlValue.Substring(0, sqlValue.Length - 1);
            var className = entity.GetType().Name;

            var sqlDynamic = $"UPDATE {className} SET {sqlValue} WHERE {_tableName}Id = @{_tableName}Id";
            dynamicParams.Add($"@{_tableName}Id", entityId);
            var res = _sqlConnection.Execute(sqlDynamic, param: dynamicParams);
            return res;
        }
        /// <summary>
        /// Hàm xóa đối tượng
        /// </summary>
        /// <param name="entityId">id của đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// @Author nmquang 19-12-2021
        public int Delete(Guid entityId)
        {
            var sqlCommand = $"DELETE FROM {_tableName} WHERE {_tableName}Id = @{_tableName}Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{_tableName}Id", entityId);
            // QueryFirstOrDefault - Lấy bản ghi đầu tiền từ câu lệnh truy vấn, nếu không có dữ liệu thì trả về null:
            var res = _sqlConnection.Execute(sqlCommand, param: parameters);
            return res;
        }
        /// <summary>
        /// Lấy bản ghi dựa vào thuộc tính (Property)
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <param name="property">Thông tin thuộc tính</param>
        /// <returns>Trả về 1 bản ghi đầu tiên được tìm thấy</returns>
        /// @Author nmquang 19-12-2021
        public object GetEntityByProperty(TEntity entity, System.Reflection.PropertyInfo property)
        {
            var propertyName = property.Name;
            var propertyValue = property.GetValue(entity);
            var keyValue = entity.GetType().GetProperty($"{_tableName}Id").GetValue(entity);
            var query = String.Empty;
            DynamicParameters parameters = new DynamicParameters();
            if (entity.EntityState == EntityState.AddNew)
            {
                parameters.Add("@propertyValue", propertyValue);
                // lấy ra bản ghi có giá trị trùng với property nhập vào
                query = $"SELECT * FROM {_tableName} where {propertyName} = @propertyValue";
            }
            else if (entity.EntityState == EntityState.Update)
            {
                parameters.Add("@propertyValue", propertyValue);
                parameters.Add("@keyValue", keyValue);
                //// lấy ra bản ghi có giá trị trùng với property nhập vào *(đk: id khác id nhập vào)
                query = $"SELECT * FROM {_tableName} where {propertyName} = @propertyValue AND {_tableName}Id <> @keyValue";
            }
            else
            {
                return null;
            }
            // lấy ra bản ghi đầu tiên trong query
            var entityReturn = _sqlConnection.Query<TEntity>(query, param: parameters, commandType: CommandType.Text).FirstOrDefault();
            return entityReturn;
        }
        /// <summary>
        /// Hàm ngắt kết nối
        /// </summary>
        /// @Author nmquang 19-12-2021
        public void Dispose()
        {
            if (_sqlConnection.State == ConnectionState.Open)
                _sqlConnection.Close();
        }
    }
}

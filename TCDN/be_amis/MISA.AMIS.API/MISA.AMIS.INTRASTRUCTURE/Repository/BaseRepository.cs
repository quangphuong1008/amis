using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMIS.Core.Interfaces;
using AMIS.Core.Interfaces.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using MySqlConnector;
using MySqlConnection = MySql.Data.MySqlClient.MySqlConnection;

namespace AMIS.Infrastruture.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        #region Fields
        /// <summary>
        /// Chuỗi kết nối
        /// </summary>
      // public static string _connectionString;
        protected string _connectionString = "" +
              "Host=47.241.69.179; " +
              "Port=3306;" +
              "User Id= dev; " +
              "Password=manhmisa;" +
              "Database=MISA.WEB09.DHTOAN; " +
              "convert zero datetime=True;";
        string _tableName;

        /// <summary>
        /// MySqlConnection
        /// </summary>
        protected MySqlConnection _sqlConnection;
        #endregion


        public BaseRepository(IConfiguration configuration)
        {
         //   _connectionString = configuration.GetConnectionString("AmisConnection");
            _tableName = typeof(TEntity).Name;
            // Khởi tạo kết nối:
            _sqlConnection = new MySqlConnection(_connectionString);
        }


        public virtual List<TEntity> GetAll()
        {
            var employees = _sqlConnection.Query<TEntity>($"SELECT * FROM {_tableName}");
            return employees.ToList();
        }

        public TEntity GetById(Guid entityId)
        {
            var sqlCommand = $"SELECT * FROM {_tableName} WHERE {_tableName}Id = @{_tableName}Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{_tableName}Id", entityId);
            // QueryFirstOrDefault - Lấy bản ghi đầu tiền từ câu lệnh truy vấn, nếu không có dữ liệu thì trả về null:
            var employee = _sqlConnection.QueryFirstOrDefault<TEntity>(sqlCommand, param: parameters);
            return employee;
        }


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
            };

            // Cắt phần tử dấu phẩy cuối cùng trong chuối:
            sqlColumDynamic = sqlColumDynamic.Substring(0, sqlColumDynamic.Length - 1);
            sqlParamDynamic = sqlParamDynamic.Substring(0, sqlParamDynamic.Length - 1);
            var className = entity.GetType().Name;
            var sqlDynamic = $"INSERT INTO {className}({sqlColumDynamic}) VALUES({sqlParamDynamic})";

            var res = _sqlConnection.Execute(sqlDynamic, param: dynamicParams, commandType: System.Data.CommandType.Text);
            return res;
        }

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
            };
            sqlValue = sqlValue.Substring(0, sqlValue.Length - 1);
            var className = entity.GetType().Name;

            var sqlDynamic = $"UPDATE {className} SET {sqlValue} WHERE {_tableName}Id = @{_tableName}Id";
            dynamicParams.Add($"@{_tableName}Id", entityId);
            var res = _sqlConnection.Execute(sqlDynamic, param: dynamicParams);
            return res;
        }

        public int Delete(Guid entityId)
        {
            var sqlCommand = $"DELETE FROM {_tableName} WHERE {_tableName}Id = @{_tableName}Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{_tableName}Id", entityId);
            // QueryFirstOrDefault - Lấy bản ghi đầu tiền từ câu lệnh truy vấn, nếu không có dữ liệu thì trả về null:
            var employee = _sqlConnection.Execute(sqlCommand, param: parameters);
            return employee;
        }
    }
}

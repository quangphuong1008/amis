using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Modal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.AMIS.INTRASTRUCTURE.Repository
{
    public class AccountObjectRepository: BaseRepository<AccountObject>, IAccountObjectRepository
    {
        public AccountObjectRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public override int Insert(AccountObject param)
        {

            string[] strs = { "EntityState", "AccountObjectBankAccountJson", "Address" };

            // Khai báo chuỗi SQL động:
            var sqlColumDynamic = "";
            var sqlParamDynamic = "";

            var dynamicParams = new DynamicParameters();

            // Lấy ra các properties của đối tượng:
            var props = param.GetType().GetProperties();
            // Duyệt từng property:
            foreach (var prop in props)
            {
                if (!strs.Contains(prop.Name)) { 
               // if (prop.Name != "EntityState" || prop.Name !="AccountObjectBankAccountJson") { 

                    // Lấy tên của property:
                    var propName = prop.Name;
                    // Lấy ra giá trị của property:
                    var propValue = prop.Name == "AccountObjectBankAccount" || prop.Name == "Address" ? JsonConvert.SerializeObject(prop.GetValue(param)) : prop.GetValue(param);

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
            var className = param.GetType().Name;
            var sqlDynamic = $"INSERT INTO {className}({sqlColumDynamic}) VALUES({sqlParamDynamic})";

            var res = _sqlConnection.Execute(sqlDynamic, param: dynamicParams, commandType: CommandType.Text);
            return res;
        }
        public override List<AccountObject> GetAll()
        {
            var tableName = "AccountObject";
            var columns = "AccountObjectId,TaxCode,AccoubtObjectCode,AccountObjectName,Phone,Website,AccountObjectGroupListId,Address AS AddressJson,EmployeeId,Prefix,EinvoiceContactName,EinvoiceContactMobile,EinvoiceContactEmail,LegalRepresentative,PaymentTermId,MaximizeDebtAmount,DueTime,AccountObjectBankAccount AS AccountObjectBankAccountJson ,AccountObjectShippingAddress,Country,District,WardOrCommune,ProvinceOrCity,Description,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,PayAccount";
            var employees = _sqlConnection.Query<AccountObject>($"SELECT {columns} FROM {tableName}");
            return employees.ToList();
        }
    }
}

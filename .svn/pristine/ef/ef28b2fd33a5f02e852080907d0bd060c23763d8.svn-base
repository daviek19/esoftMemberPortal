
using EstateManagementMvc.Services.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

//Adapted from http://elegantcode.com/2012/01/26/sqlbulkcopy-for-generic-listt-useful-for-entity-framework-nhibernate/
namespace EstateManagementMvc.Services.Common
{
    public class BulkInsertClass
    {
        public static bool InsertList<T>(string tableName, IList<T> list)
        {
            var result = default(bool);
            try
            {
                string connection = System.Configuration.ConfigurationManager.ConnectionStrings["MainDbConnection"].ToString();

                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.BatchSize = list.Count;
                    bulkCopy.DestinationTableName = tableName;

                    var table = new DataTable();
                    var props = TypeDescriptor.GetProperties(typeof(T))
                        //Dirty hack to make sure we only have system data types
                        //i.e. filter out the relationships/collections
                                               .Cast<PropertyDescriptor>()
                                               .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
                                               .ToArray();

                    foreach (var propertyInfo in props)
                    {
                        bulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
                        table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
                    }

                    var values = new object[props.Length];
                    foreach (var item in list)
                    {
                        for (var i = 0; i < values.Length; i++)
                        {
                            values[i] = props[i].GetValue(item);
                        }

                        table.Rows.Add(values);
                    }

                    bulkCopy.WriteToServer(table);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("Bulk Insert " + tableName + " " + list.ToString() + " " + ex.ToString());
                result = false;
            }

            return result;
        }
    }
}
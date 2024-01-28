using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversionMethods
{
    public static class DataTableExtensions
    {
        // Extension method to convert DataTable to a list of objects

        public static List<T> ToList<T>(this DataTable dataTable)
        {
            List<T> result = new List<T>();

            foreach (DataRow row in dataTable.Rows)
            {
                T item = Activator.CreateInstance<T>();

                foreach (DataColumn column in dataTable.Columns)
                {
                    var property = typeof(T).GetProperty(column.ColumnName);

                    if (property != null && row[column] != DBNull.Value)
                    {
                        property.SetValue(item, row[column]);
                    }
                }

                result.Add(item);
            }

            return result;
        }


        // Extension method to convert a list of objects to DataTable
        public static DataTable ToDataTable<T>(this List<T> list)
        {
            DataTable dataTable = new DataTable();


                // add columns to DataTable
                foreach (var prop in list[0].GetType().GetProperties())
                {
                    dataTable.Columns.Add(prop.Name, prop.PropertyType);
                }

                // Add rows to DataTable 
                foreach (var item in list)
                {
                    DataRow row = dataTable.NewRow();

                    foreach (var prop in item.GetType().GetProperties())
                    {
                        row[prop.Name] = prop.GetValue(item)??DBNull.Value ;
                    }

                    dataTable.Rows.Add(row);
                }
            

            return dataTable;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Data;

namespace SimpleMVVMExample.Utility
{
    public static class DBUtilites
    {
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                var list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    var obj = new T();

                    // Loop through all Items
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            // Transfer Item to c# Type
                            var propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            // ignored
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
}
}

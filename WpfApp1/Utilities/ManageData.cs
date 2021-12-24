using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WpfApp1.Utilities
{
   class ManageData
   {
      public static void MySqlDataReaderToList<T>(T entity, MySqlDataReader reader, List<T> list)
      {
         PropertyInfo[] propertyArray = typeof(T).GetProperties();
      }

		public static void CreateList<T>(T entity, MySqlDataReader reader, List<T> list)
		{
			
			var properties = typeof(T).GetProperties();

			while (reader.Read())
			{
				var item = Activator.CreateInstance<T>();
				foreach (var property in typeof(T).GetProperties())
				{
					if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
					{
						Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
						property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
					}
				}
				list.Add(item);
			}
		}

	}
}
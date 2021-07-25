using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ANNA.Data
{
    public class DataSorting
    {
        public static List<T> Sort<T>(IList<T> unsortedList, string property, bool isAscending = true)
        {
            var type = typeof(T);
            var sortProperty = type.GetProperty(property);
            var spType = sortProperty.PropertyType;

            if (isAscending)
            {
                return unsortedList.OrderBy(p => sortProperty.GetValue(p, null)).ToList();
            }
            else
            {
                return unsortedList.OrderByDescending(p => sortProperty.GetValue(p, null)).ToList();
            }
        }
        public static List<T> Sort<T>(Array unsortedList, string property, bool isAscending = true)
        {
            List<T> UnsortedList = new();

            foreach (var item in unsortedList)
            {
                UnsortedList.Add((T)item);
            }

            var type = typeof(T);
            var sortProperty = type.GetProperty(property);

            if (isAscending)
            {
                return UnsortedList.OrderBy(p => sortProperty.GetValue(p, null)).ToList();
            }
            else
            {
                return UnsortedList.OrderByDescending(p => sortProperty.GetValue(p, null)).ToList();
            }
        }
    }
}

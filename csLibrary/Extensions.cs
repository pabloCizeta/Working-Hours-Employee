using System;
using System.Collections.Generic;
using System.Reflection;

namespace Extensions
{
   

    //public class SortDirection
    //{
    //    public string Ascending { get; set; }
    //    public string Descending { get; set; }
    //}

    public static class Extensions
    {
        enum SortDirection { Ascending, Descending };

        public static void Sort<T>(this List<T> list, string sortExpression)
        {
            string[] sortExpressions = sortExpression.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            List<GenericComparer> comparers = new List<GenericComparer>();
            
            foreach (string sortExpress in sortExpressions)
            {
                string sortProperty = sortExpress.Trim().Split(' ')[0].Trim();
                string sortDirection = sortExpress.Trim().Split(' ')[1].Trim();

                Type type = typeof(T);
                PropertyInfo PropertyInfo = type.GetProperty(sortProperty);
                if (PropertyInfo == null)
                {
                    PropertyInfo[] props = type.GetProperties();
                    foreach (PropertyInfo info in props)
                    {
                        if (info.Name.ToString().ToLower() == sortProperty.ToLower())
                        {
                            PropertyInfo = info;
                            break;
                        }
                    }
                    if (PropertyInfo == null)
                    {
                        throw new Exception(String.Format("{0} is not a valid property of type: \"{1}\"", sortProperty,type.Name));
                    }
                }

                SortDirection SortDirection = SortDirection.Ascending;
                if (sortDirection.ToLower() == "asc" || sortDirection.ToLower() == "ascending")
                {
                    SortDirection = SortDirection.Ascending;
                }
                else if (sortDirection.ToLower() == "desc" || sortDirection.ToLower() == "descending")
                {
                    SortDirection = SortDirection.Descending;
                }
                else
                {
                    throw new Exception("Valid SortDirections are: asc, ascending, desc and descending");
                }

                comparers.Add(new GenericComparer { SortDirection = SortDirection, PropertyInfo = PropertyInfo, comparers = comparers });
            }
            list.Sort(comparers[0].Compare);
        }
    }

    public class GenericComparer
    {
        public List<GenericComparer> comparers { get; set; }
        int level = 0;

        public SortDirection SortDirection { get; set; }
        public PropertyInfo PropertyInfo { get; set; }

        public int Compare<T>(T t1, T t2)
        {
            int ret = 0;
            
            if (level >= comparers.Count)
                return 0;

            object t1Value = comparers[level].PropertyInfo.GetValue(t1, null);
            object t2Value = comparers[level].PropertyInfo.GetValue(t2, null);
            
            if (t1 == null || t1Value == null)
            {
                if (t2 == null || t2Value == null)
                {
                    ret = 0;
                }
                else
                {
                    ret = -1;
                }
            }
            else
            {
                if (t2 == null || t2Value == null)
                {
                    ret = 1;
                }
                else
                {
                    ret = ((IComparable)t1Value).CompareTo(((IComparable)t2Value));
                }
            }
            if (ret == 0)
            {
                level += 1;
                ret = Compare(t1, t2);
                level -= 1;
            }
            else
            {
                if (comparers[level].SortDirection == SortDirection.Descending)
                {
                    ret *= -1;
                }
            }
            return ret;
        }
    }

    public class ExampleUser
    {
        public DateTime Birthday { get; set; }
        public string Firstname { get; set; }
    }

    public class ExampleClass
    {
        public static void Example()
        {
            List<ExampleUser> userlist = new List<ExampleUser>();
            userlist.Add(new ExampleUser { Birthday = new DateTime(1988, 10, 1), Firstname = "Bryan" });
            userlist.Add(new ExampleUser { Birthday = new DateTime(1986, 11, 4), Firstname = "Michael" });
            userlist.Add(new ExampleUser { Birthday = new DateTime(1977, 2, 2), Firstname = "Arjan" });
            userlist.Add(new ExampleUser { Birthday = new DateTime(1990, 6, 13), Firstname = "Pieter" });
            userlist.Add(new ExampleUser { Birthday = new DateTime(1988, 10, 1), Firstname = "Ruben" });
            userlist.Add(new ExampleUser { Birthday = new DateTime(1987, 8, 21), Firstname = "Bastiaan" });
            userlist.Add(new ExampleUser { Birthday = new DateTime(1987, 8, 21), Firstname = "Pieter" });

            string unsorted = "Unsorted: " + Environment.NewLine;
            foreach (ExampleUser user in userlist)
            {
                unsorted += String.Format("{0} / {1} {2}", user.Birthday.ToString("dd-MM-yyyy"), user.Firstname, Environment.NewLine);
            }

            userlist.Sort("Firstname asc");
            string sorted1 = "Sorted by Firstname ascending: " + Environment.NewLine;
            foreach (ExampleUser user in userlist)
            {
                sorted1 += String.Format("{0} / {1} {2}", user.Birthday.ToString("dd-MM-yyyy"), user.Firstname, Environment.NewLine);
            }

            userlist.Sort("Firstname asc, Birthday desc");
            string sorted2 = "Sorted by Firstname ascending, Birtday descending: " + Environment.NewLine;
            foreach (ExampleUser user in userlist)
            {
                sorted2 += String.Format("{0} / {1} {2}", user.Birthday.ToString("dd-MM-yyyy"), user.Firstname, Environment.NewLine);
            }

            userlist.Sort("Birthday asc, Firstname asc");
            string sorted3 = "Sorted by Birthday ascending, Firstname ascending: " + Environment.NewLine;
            foreach (ExampleUser user in userlist)
            {
                sorted3 += String.Format("{0} / {1} {2}", user.Birthday.ToString("dd-MM-yyyy"), user.Firstname, Environment.NewLine);
            }
        }
    }
}

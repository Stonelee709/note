using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    static class MyExtend
    {
        public static void print(this String ss)
        {
            Console.WriteLine(ss);
        }
        public static void toLog(this String ss, string path)
        {
            File.WriteAllText(path, ss);
        }
        public static string ToStringValue(this object inputValue)
        {
            return inputValue == null ? "" : inputValue.ToString();
        }
        public static bool IsInt(this object inputValue)
        {
            int num;
            return int.TryParse(inputValue.ToStringValue(), out num);
        }

        public static int ToInt(this object inputValue)
        {
            return inputValue.IsInt() ? int.Parse(inputValue.ToStringValue()) : 0;
        }
        public static void ForEach<T>(this IEnumerable<T> list,Action<T> func)
        {
            foreach(var item in list)
            {
                func(item);
            }
        }
        private static readonly Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");
        public static bool IsEmail(this object inputValue)
        {
            var match = RegEmail.Match(inputValue.ToStringValue());
            return match.Success;
        }
    }


    
}

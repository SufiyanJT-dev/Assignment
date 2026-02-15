using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignments_22_10_2025
{
    public static class CustomExtention
    {

        public static string ToTitleCase(this string str)
        {
            char[] chars = str.ToCharArray();
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            chars[0] = char.ToUpper(chars[0]);
            for (int i = 1; i < str.Length; i++) {
                if (chars[i-1] == ' ' && chars[i] != '\0') {
                    chars[i]=char.ToUpper(chars[i]);
                }
                
            }
            string newstr = new string(chars);
            return newstr;
        }
        public static bool IsUpper(this string str)
        {
            
            if (char.ToUpper(str[0]) == str[0])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static double AverageExceptZero(this List<int> list)
        {
            {
                double avg = 0;
                double Count = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    
                    if(list[i] > 0)
                    {
                        Count=Count + 1;
                        avg = avg + list[i];
                    }

                }
                avg = avg / Count;
                
                return avg;
            }
        }
    }
   
}


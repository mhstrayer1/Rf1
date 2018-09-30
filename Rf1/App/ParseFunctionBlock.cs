using System;
using System.Globalization;
using static Rf1.Parameters.Parameters;

namespace Rf1.App
{
   public static class ParseFunctionBlock
   {
       public static int ParseIt(string jsonTxt)
      {
         int errors = 0;

         // check if there is a combiners block
         int i1 = jsonTxt.IndexOf("functions:");
         if (i1 == -1)
         {
            return errors;
         }

         int i2 = jsonTxt.IndexOf("{", i1);
         if (i2 == -1)
         {
            Console.WriteLine("--> Malformed function block");
            errors += 1;
            return errors;
         }

         int i4 = jsonTxt.IndexOf("}", i2);
         if (i4 == -1 || i4 < i2)
         {
            Console.WriteLine("--> Malformed function block");
            errors += 1;
            return errors;       
         }

         i2 = i2 + 1;
         i2 = jsonTxt.IndexOf("\n",i2) + 1;
         
         int i3 = 0;
         char[] digits = {'0','1','2','3','4','5','6','7','8','9'};

         while (i2 < i4)
         {
            i3 = jsonTxt.IndexOf("\n", i2);
            if (i3 >= i4)
            {
               return errors;
            }
    
            int i5 = jsonTxt.IndexOf("\"", i2);
            int i6 = jsonTxt.IndexOf("\"", i5 + 1);
            string key = jsonTxt.Substring(i5 + 1, i6 - i5 - 1);

            i5 = jsonTxt.IndexOf(":", i6) + 1;
            string value = jsonTxt.Substring(i5, i3 - i5);
            i6 = value.LastIndexOfAny(digits);

            int offset = i6 + 1;
            if (offset < value.Length)
            {
               value = value.Remove(offset);
            }
            
            var(eFlag,dType) = GetIntParam(key);
            if (!eFlag)
            {
               Console.WriteLine($"--> Unknown parameter : ${key}");
               errors += 1;
            }

            bool flag;
            string msg;
            if (eFlag)
            {
               int iv = int.Parse(value, NumberStyles.Integer | NumberStyles.AllowThousands);
               (flag,msg) = UpdateParam(key, iv);
               if (!flag)
               {
                  Console.WriteLine($"--> Error updating {key} : {msg}");
                  errors += 1;
                }      
            }

            i2 = jsonTxt.IndexOf("\n",i3) + 1;
         }
         
      return errors;
      }     
   }
}
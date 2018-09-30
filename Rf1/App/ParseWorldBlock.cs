using System;
using System.Globalization;
using System.Text.RegularExpressions;
using static Rf1.Parameters.Parameters;

namespace Rf1.App
{
   public static class ParseWorldBlock
   {
      public static int ParseIt(string inputString)
      {
         int errors = 0;
         
         Regex rx = new Regex(@"(world):[\r\n]+ +{[\n\r]+( +""([a-z_.]+)""[ :]+([a-z0-9.+-eE]+),?[\n\r]+)* +},?");
         MatchCollection blocks = rx.Matches(inputString);

         foreach (Match m in blocks)
         {
            string name = m.Groups[1].Captures[0].Value;

            int capCnt = m.Groups[2].Captures.Count;
            if (capCnt == 0) // empty blockl
            {
               continue;
            }

            for (int i = 0; i < capCnt; i++)
            {
               string fname = m.Groups[3].Captures[i].Value;
               string value = m.Groups[4].Captures[i].Value;
            }
         }

//         Regex rx = new Regex(@"world:");
//         MatchCollection matches = rx.Matches(inputString);
//         foreach(Match match in matches)
//         {
//            int p1 = match.Index + match.Length;
//            string exp = "\"[a-z]+\" *: *[0-9.+-eE]+";
//            Regex r1 = new Regex(exp);
//            MatchCollection lines  = r1.Matches(inputString, p1);
//            foreach (Match line in lines)
//            {
//               string msg;
//               string type;
//               bool flag;
//               
//               string l = line.Value;
//               int l1 = l.IndexOf("\"", StringComparison.Ordinal);
//               int l2 = l.IndexOf("\"", l1 + 1, StringComparison.Ordinal);
//               string name= l.Substring(l1 + 1, l2 - l1 - 1);
//               l1 = l.IndexOf(":", StringComparison.Ordinal);
//               string value = l.Substring(l1+1);
//               value = value.Replace(',', ' ');
//               string key = "world." + name;
//               (flag,type) = GetStringParam(key);
//               if (!flag)
//               {
//                  Console.WriteLine($"--> Invalid World Parameter : ${name}");
//                  errors += 1;
//                  continue;
//               }
//
//
//               switch (type)
//               {
//                  case "int" :
//                     int iv = int.Parse(value, NumberStyles.Integer | NumberStyles.AllowThousands);
//                     (flag, msg) = UpdateParam(key, iv);
//                     if (!flag)
//                     {
//                        Console.WriteLine($"--> Error Updating parameter {key} : {msg}");
//                        errors += 1;
//                     }
//                     break;
//                  
//                  case "double" :
//                     double dv = double.Parse(value, NumberStyles.Float | NumberStyles.AllowThousands);
//                     (flag, msg) = UpdateParam(key, dv);
//                     if (!flag)
//                     {
//                        Console.WriteLine($"--> Error Updating parameter {key} : {msg}");
//                        errors += 1;
//                     }
//                     break;
//                 
//                  default:
//                     Console.WriteLine($"--> Unsupported World BLock Type : ${type}");
//                     errors += 1;
//                     break;
//               }
//            }
//         }
         
         return errors;
      }
   }
}
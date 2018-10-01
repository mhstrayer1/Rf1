using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using NCalc;
using static Rf1.Parameters.Parameters;

namespace Rf1.App
{
   public static class ParseWorldBlock
   {
      public static int ParseIt(string inputString)
      {
         int errors = 0;
         StringBuilder sb = new StringBuilder(64);
         
         Regex rx = new Regex(@"(world):[\r\n]+ +{[\n\r]+( +""([a-z_.]+)""[ :]+([a-z0-9.+-eE*/ ]+),?[\n\r]+)* +},?");
         Regex r1 = new Regex(@"[ *+-/a-z]+");
         MatchCollection blocks = rx.Matches(inputString);

         string type;
         bool flag;
        
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
               value = value.Replace(",", " ").Trim();
               sb.Clear();
               sb.Append(name);
               sb.Append(".");
               sb.Append(fname);
               string key = sb.ToString();
               (flag, type) = GetStringParam(key);
               if (!flag)
               {
                  Console.WriteLine($"--> Invalid world parameter : {fname}");
                  errors += 1;
                  continue;
               }

               switch (type)
               {
                  case "int":
                     int iv;
                     flag = int.TryParse(value, NumberStyles.Integer | NumberStyles.AllowThousands,
                                         CultureInfo.CurrentCulture, out iv);
                     if (!flag)
                     {
                        Console.WriteLine($"--> Error converting int in ParseWorldBlock {name} : {value}");
                        errors += 1;
                     }
                     else
                     {
                        (flag, _) = UpdateParam(key, iv);
                        if (!flag)
                        {
                           Console.WriteLine($"--> Error updating parameter : {name} ");
                           errors += 1;                          
                        }
                     }
                     break;
                  
                  case "double":
                     double dv = 0;
                     if (r1.IsMatch(value))
                     {
                        Expression e = new Expression(value);
                        if (value.Contains("pi"))
                        {
                           e.Parameters["pi"] = Math.PI;
                        }   
                        dv  = (double) e.Evaluate();
                        flag = true;
                     }
                     else
                     {
                        flag = double.TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands,
                                               CultureInfo.CurrentCulture, out dv);                      
                     }

                     if (!flag)
                     {
                        Console.WriteLine($"--> Error converting double in ParseWorldBlock {name} : {value}");
                        errors += 1;
                     }
                     else
                     {
                        (flag, _) = UpdateParam(key, dv);
                        if (!flag)
                        {
                           Console.WriteLine($"--> Error updating parameter : {name} ");
                           errors += 1;                          
                        }
                     }                     
                     break;
                  
                  case "bool" :
                     bool bv;
                     switch (value)
                     {
                        case "t":
                        case "true":
                           bv = true;
                           break;
                        
                        case "f":
                        case "false" :
                           bv = false;
                           break;
                        
                        default:
                           Console.WriteLine($"--> Invalid boolean value : {value}");
                           errors += 1;
                           continue;
                     }

                     (flag, _) = UpdateParam(key, bv);
                     if (!flag)
                     {
                        Console.WriteLine($"--> Error updating parameter : {name} ");
                        errors += 1;                          
                     }                    
                     break;
                  
                  default:
                     Console.WriteLine($"--> Type not implemented in world : {type}");
                     errors += 1;
                     break;
               }
            }
         }

         return errors;
      }
   }
}
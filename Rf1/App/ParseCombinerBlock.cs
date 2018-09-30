using System;
   using System.Globalization;
   using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
   using static Rf1.Parameters.Parameters;
   
   namespace Rf1.App
   {
      public static class ParseCombinerBlock
      {
         public static int ParseIt(string inputString)
         {
            int errors = 0;
   
            Regex rx = new Regex(@"combiners:");
            MatchCollection matches = rx.Matches(inputString);
            foreach (Match match in matches)
            {
               int p1 = match.Index + match.Length + 1;
               
               // check for empty block;
               string exp = "[ \n\r\t]+{[ \n\r\t]+},";
               Regex r1 = new Regex(exp);
               Match m = r1.Match(inputString, p1);
               if (m.Success)
               {
                  continue;
               }
   
               exp = @"([a-z]+):[ \n\r\t]+{[\n\r]+([ \t]+""([a-z_]+)""[ \t:]+([0-9a-z]+),?[\n\r]+)+[ \t]+},?";
               r1 = new Regex(exp);
               MatchCollection blocks = r1.Matches(inputString, p1 + 1);
               string msg;
               bool flag;
               foreach (Match block in blocks)
               {
                  string fname = block.Groups[1].Captures[0].Value;
                  for (int i = 0; i < 3; i++)
                  {
                     string pname = block.Groups[3].Captures[i].Value;
                     string value = block.Groups[4].Captures[i].Value;
                     string key = "cmb." + fname + "." + pname;
                    
                     if (value=="true" || value == "false")
                     {
                        (flag, msg) = UpdateParam(key, (value == "true"));
                        if (!flag)
                        {
                           Console.WriteLine($"--> Error updating parameter {key}");
                           errors += 1;
                        }
                     }
                     else
                     {
                        int iv;
                        if (!int.TryParse(value,out iv))
                        {
                           Console.WriteLine($"--> Integer conversion error : {value}");
                           errors += 1;
                           continue;
                        }

                        (flag, msg) = UpdateParam(key, iv);
                        if (!flag)
                        {
                           Console.WriteLine($"--> Error updating parameter {key}");
                           errors += 1;
                        }                       
                     }
                  }

               } 
   
            }
   
            return errors;
         }
   
      }
   
   }
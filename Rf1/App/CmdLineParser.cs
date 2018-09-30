using System;
using static Rf1.Parameters.Parameters;

namespace Rf1.App
{
   public static class CmdLineParser
   {
      public static bool Parse(string[] args)
      {
         if (args.Length % 2 != 0)
         {
            Console.WriteLine("--> Invalid number of command line arguments");
            return false;
         }

         bool errors = false;
         for (int i = 0; i < args.Length; i += 2)
         {
            switch (args[i])
            {
               case "-i":
                  UpdateParam("InputFile", args[i + 1]);
                  break;

               default:
                  Console.WriteLine($"--> Invalid argument : {args[i]}");
                  errors = true;
                  break;
            }
         }

         return errors;
      }
      
   }
}
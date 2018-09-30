using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static Rf1.Parameters.Parameters;

namespace Rf1.App
{
   public static class ProcessInputFile
   {
      public static bool ParseFile()
      
      {
         var(_,fileName) = Parameters.Parameters.GetStringParam("InputFile");
         if (!File.Exists(fileName))
         {
            Console.WriteLine($"-- Input file not found.");
            return false;
         }

         string inputString;
         using (StreamReader sr = new StreamReader(fileName))
         {
            inputString = sr.ReadToEnd();
         }

         inputString = inputString.Trim();
         inputString = inputString.Replace("\t", "");
         
         int errors = 0;
         if ( !(inputString.StartsWith("{") && inputString.EndsWith("}")) )
         {
            Console.WriteLine("--> Malformed input file");
            return false;
         }
         
         
         // process world blocks
         errors += ParseWorldBlock.ParseIt(inputString);
         
         // process combiner blocks
         errors += ParseCombinerBlock.ParseIt(inputString);
         
         return errors == 0;
      }
   }
}
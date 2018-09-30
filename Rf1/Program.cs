
using System;
using System.Security.Cryptography;
using System.Numerics;
using Rf1.App;
using Rf1.Random;
using Rf1.Combiners;
using static Rf1.App.App;

namespace Rf1
{
   internal class Program
   {
      public static void Main(string[] args)
      {
         InitializeDefaults();
         
         //Parse Command Line Arguments
         if (CmdLineParser.Parse(args))
         {
            Console.WriteLine(" --> Error parsing command line - quiting.");
            Environment.Exit(1);
         }
         
         // Process Input File
         if (!ProcessInputFile.ParseFile())
         {
            Console.WriteLine(" --> Error parsing input file - quiting.");
            Environment.Exit(2);      
         }
         
         // Create and Initialize RNG
         byte[] byteArray = new byte[1024];
         using (var provider = new RNGCryptoServiceProvider())
         {
            provider.GetBytes(byteArray);
         }
         StaticRng.Rng =  new Gjrand(ref byteArray);
         
         Complex[] values = new Complex[4];
         values[0] = new Complex(1.1, 1.1);         
         values[1] = new Complex(2.1, 2.1);
         values[2] = new Complex(-3.3, -2.2);
         values[3] = new Complex(4.4, -1.1);

         Combiner c = CombinerFactory.Create();
         var r = c.Evaluate(ref values);
         Console.WriteLine($"{r}");
      }
   }
}
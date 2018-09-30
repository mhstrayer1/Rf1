using System;
using System.Numerics;
using System.Diagnostics;
using Rf1.Random;

namespace Rf1.Combiners
{
   public class RandomCombiner : Combiner
   {
      private Types[] _ops;
         
      public RandomCombiner(int numTerms) : base("random",numTerms) 
      {
         _ops = new Types[numTerms-1];
         Types last = Types.Random;
         int nops = (int) Convert.ChangeType(last, last.GetTypeCode()) - 1;
         for (int i = 0; i < numTerms - 1; i++)
         {
            _ops[i] = (Types) StaticRng.Rng.Next(0, nops);
         }
      }

      public override Complex Evaluate(ref Complex[] values)
      {
         Debug.Assert(values.Length >= NumTerms, "Not enough terms");
         
         Complex rslt = values[0];

         for (int i = 1; i < NumTerms; i++)
         {
           switch (_ops[i-1])
            {
               case Types.Add :
                  rslt += values[i];
                  break;
               
               case Types.Divide:
                  rslt /= values[i];
                  break;
               
               case Types.Multiply:
                  rslt *= values[i];
                  break;
               
               case Types.Subtract:
                  rslt -= values[i];
                  break;
               
               default:
                  throw new InvalidOperationException("Invalid Combiner Type - Should never have gotten here");
            }
         }

         return rslt;
      }        
   }
}
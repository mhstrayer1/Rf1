using System.Numerics;
using System.Diagnostics;

namespace Rf1.Combiners
{
   public class SubtractCombiner : Combiner
   {
      public SubtractCombiner(int numTerms) : base("subtract",numTerms)
      {
      }

      public override Complex Evaluate(ref Complex[] values)
      {
         Debug.Assert(values.Length >= NumTerms, "Not enough terms");
         
         Complex diff = values[0];

         for (int i = 1; i < NumTerms; i++)
         {
            diff -= values[i];
         }

         return diff;
      }
   }
}
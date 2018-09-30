using System.Numerics;
using System.Diagnostics;

namespace Rf1.Combiners
{
   public class MultiplyCombiner : Combiner
   {
      public MultiplyCombiner(int numTerms) : base("multiply",numTerms)
      {
      }

      public override Complex Evaluate(ref Complex[] values)
      {
         Debug.Assert(values.Length >= NumTerms, "Not enough terms");
         
         Complex rslt = values[0];

         for (int i = 1; i < NumTerms; i++)
         {
            rslt *= values[i];
         }

         return rslt;
      }      
   }
}
using System.Numerics;
using System.Diagnostics;

namespace Rf1.Combiners
{
   public class AddCombiner : Combiner
   {
      public AddCombiner(int numTerms) : base("add",numTerms)
      {
      }

      public override Complex Evaluate(ref Complex[] values)
      {
         Debug.Assert(values.Length >= NumTerms, "Not enough terms");
         Complex sum = new Complex(0.0,0.0);

         for (int i = 0; i < NumTerms; i++)
         {
            sum += values[i];
         }
         
         return sum;
      }
   }
}
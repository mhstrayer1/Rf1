using System.Numerics;
using Rf1.Combiners;

namespace Rf1.Functions
{
   public class RfMult : Function
   {
      public RfMult(int numTerms) : base("RfMult", numTerms)
      {
         _Combiner = new MultiplyCombiner(numTerms);
      }

      public override Complex Evaluate(double x, double y)
      {
         return new Complex(0.0,0.0);
      }

   }
}
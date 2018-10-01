using System.Numerics;
using Rf1.Combiners;

namespace Rf1.Functions
{
   public class RfAdd : Function
   {
      public RfAdd(int numTerms) : base("RfAdd", numTerms)
      {
         _Combiner = new AddCombiner(numTerms);
      }

      public override Complex Evaluate(double x, double y)
      {
         return new Complex(0.0,0.0);
      }

   }
}
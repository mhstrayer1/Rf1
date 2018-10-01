using System.Numerics;
using Rf1.Combiners;

namespace Rf1.Functions
{
   public class RfSub : Function
   {
      public RfSub(int numTerms) : base("RfSub", numTerms)
      {
         _Combiner = new SubtractCombiner(numTerms);
      }

      public override Complex Evaluate(double x, double y)
      {
         return new Complex(0.0,0.0);
      }

      
   }
}
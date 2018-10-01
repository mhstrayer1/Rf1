using System.Numerics;
using Rf1.Combiners;

namespace Rf1.Functions
{
   public class RfDiv : Function
   {
      public RfDiv
         (int numTerms) : base("RfDiv", numTerms)
      {
         _Combiner = new DivideCombiner(numTerms);
      }

      public override Complex Evaluate(double x, double y)
      {
         return new Complex(0.0,0.0);
      }
     
   }
}
using System.Collections.Generic;
using System.Numerics;
using Rf1.Combiners;

namespace Rf1.Functions
{
   public abstract class Function
   {
      public int NumTerms { get; }
      public string Name { get; }

      protected List<Function> _Terms;
      protected List<Complex> _Values;
      protected Combiner _Combiner;
      
      protected Function(string name, int numTerms)
      {
         NumTerms = numTerms;
         Name = name;
         
         _Terms = new List<Function>(numTerms);
         _Values = new List<Complex>(numTerms);
      }

      public abstract Complex Evaluate(double x, double y);
   }
}
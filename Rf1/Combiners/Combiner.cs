using System.Numerics;

namespace Rf1.Combiners
{   
   public abstract class Combiner
   {
      public enum Types
      {
         Add,
         Subtract,
         Multiply,
         Divide,
         Random
      }
      
      public int NumTerms { get; protected set; }
      public string Name { get; protected set; }
      
      protected Combiner(string name, int numTerms)
      {
         Name = name;
         NumTerms = numTerms;
      }

      public abstract Complex Evaluate(ref Complex[] values);
   }
}
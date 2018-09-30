using System;
using Rf1.Random;
using static Rf1.Parameters.Parameters;

namespace Rf1.Combiners
{
   public static class CombinerFactory
   {
      private static int _NumCombiners;

      static CombinerFactory()
      {
         _NumCombiners = (int) Combiner.Types.Random + 1;
      }

      public static Combiner Create()
      {
         int isel = StaticRng.Rng.Next(0, _NumCombiners-1);
         Combiner.Types iwant = (Combiner.Types) isel;

         int minTerms;
         int maxTerms;
         int numTerms;
         
         switch (iwant)
         {
            case Combiner.Types.Add:
               (_,minTerms) = GetIntParam("cmb.add.min_terms");
               (_,maxTerms) = GetIntParam("cmb.add.max_terms");
               numTerms = StaticRng.Rng.Next(minTerms, maxTerms);
               return new AddCombiner(numTerms);
           
            
            case Combiner.Types.Divide:
               (_,minTerms) = GetIntParam("cmb.div.min_terms");
               (_,maxTerms) = GetIntParam("cmb.div.max_terms");
               numTerms = StaticRng.Rng.Next(minTerms, maxTerms);
               return new DivideCombiner(numTerms);
            
            case Combiner.Types.Multiply :
               (_,minTerms) = GetIntParam("cmb.mult.min_terms");
               (_,maxTerms) = GetIntParam("cmb.mult.max_terms");
               numTerms = StaticRng.Rng.Next(minTerms, maxTerms);
               return new MultiplyCombiner(numTerms);
         
            
            case Combiner.Types.Random :
               (_,minTerms) = GetIntParam("cmb.rand.min_terms");
               (_,maxTerms) = GetIntParam("cmb.rand.max_terms");
               numTerms = StaticRng.Rng.Next(minTerms, maxTerms);
               return new RandomCombiner(numTerms);
               
            
            case Combiner.Types.Subtract:
               (_,minTerms) = GetIntParam("cmb.sub.min_terms");
               (_,maxTerms) = GetIntParam("cmb.sub.max_terms");
               numTerms = StaticRng.Rng.Next(minTerms, maxTerms);
               return new SubtractCombiner(numTerms);
            
            
            default:
               throw new InvalidOperationException("Invalid Combiner Type - Should never have gotten here");   
         }
      }

   }
}
   
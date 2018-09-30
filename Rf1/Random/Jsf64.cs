//-------------------------------------------------------------------------------
// A small fast random number generator by Bob Jenkins
// http://burtlebrutle.net/bob/index.html
// Based on C code in the paper describing this generator
// The three shift generator is implemented
//
// Bob placed his code in the public domain
//-------------------------------------------------------------------------------

using System;

namespace Rf1.Random 
{
   public sealed class Jsf64 : Rng
   {
      private Ranctx64 _state;

      public Jsf64(ulong seed)
      {
         _state.A = 0xf1ea5eed;
         _state.B = seed;
         _state.C = seed;
         _state.D = seed;

         for (var i = 0; i < 20; ++i)
            Next64();
      }

      private ulong Rot(ulong x, int k)
      {
         return (x << k) | (x >> (64 - k));
      }

      public override ulong Next64()
      {
         Calls += 1;
         var e = _state.A - Rot(_state.B, 7);
         _state.A = _state.B ^ Rot(_state.C, 13);
         _state.B = _state.C + Rot(_state.D, 37);
         _state.C = _state.D + e;
         _state.D = e + _state.A;
         return _state.D;
      }

      public override uint Next32()
      {
         throw new NotImplementedException("32 bit output not available from 64 bit generator");
      }

      public override double Next01()
      {
         return (double) Next64() / ulong.MaxValue;
      }

      public override int Next(int min, int max)
      {
         return min + (int)((max - min + 1) * Next01());
      }

      private struct Ranctx64
      {
         public ulong A;
         public ulong B;
         public ulong C;
         public ulong D;
      }
   }
}
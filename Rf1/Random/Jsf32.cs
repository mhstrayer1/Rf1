namespace Rf1.Random
{
   //-------------------------------------------------------------------------------
   // A small fast random number generator by Bob Jenkins
   // http://burtlebrutle.net/bob/index.html
   // Based on C code in the paper describing this generator
   // The three shift generator is implemented
   //
   // Bob placed his code in the public domain
   //-------------------------------------------------------------------------------
   public sealed class Jsf32 : Rng
   {
      private Ranctx _state;

      public Jsf32(uint seed)
      {
         _state.A = 0xf1ea5eed;
         _state.B = seed;
         _state.C = seed;
         _state.D = seed;

         for (var i = 0; i < 20; ++i)
            Next32();
      }

      private uint Rot(uint x, int k)
      {
         return (x << k) | (x >> (32 - k));
      }

      public override uint Next32()
      {
         Calls += 1;
         var e = _state.A - Rot(_state.B, 23);
         _state.A = _state.B ^ Rot(_state.C, 16);
         _state.B = _state.C + Rot(_state.D, 11);
         _state.C = _state.D + e;
         _state.D = e + _state.A;
         return _state.D;
      }

      public override ulong Next64()
      {
         return (ulong) Next32();
      }
      
      public override int Next(int min, int max)
      {
         return min + (int)((max - min + 1) * Next01());
      }

      public override double Next01()
      {
         return (double) Next32() / uint.MaxValue;
      }

      private struct Ranctx
      {
         public uint A;
         public uint B;
         public uint C;
         public uint D;
      }
   }
}
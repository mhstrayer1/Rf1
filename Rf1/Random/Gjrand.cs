using System;
using System.Collections.Generic;


namespace Rf1.Random
{
   //  Adapted from the gjrand random number library version 3.4.0.0 or later. 
   //  Original Copyright (C) 2004-2011 G. Jones. 
   // Adapted by Michael Strayer
   // Licensed under the GNU General Public License version 2 or 3. 
   public class Gjrand : Rng
   {
      struct Gjstate
      {
         public ulong A;
         public ulong B;
         public ulong C;
         public ulong D;
      }

      private Gjstate _state;

      public Gjrand(uint seed)
      {
         _state.A = seed;
         _state.B = 0;
         _state.D = 0;
         _state.C = 1000001;
         GjrandInteMixstate();
      }

      public Gjrand(ulong seed)
      {
         _state.A = seed;
         _state.B = 0;
         _state.C = 2000001;
         _state.D = 0;
         GjrandInteMixstate();
      }

      public Gjrand(ref byte[] buf)
      {
         _state.A = 0;
         _state.B = 0;
         _state.C = 0;
         _state.D = 0;

         int offset = 0;
         int l = buf.Length;
         while (l >= 24)
         {
            _state.A ^= GjrandInteL64(ref buf, offset+0);
            _state.B ^= GjrandInteL64(ref buf, offset+8);
            _state.C ^= GjrandInteL64(ref buf, offset+16);
            l -= 24;
            offset += 24;
            GjrandInteMixstate();
         }

         if (l > 0)
         {
            byte[] b2 = new byte[24];

            for (int i = 0; i < l; i++)
            {
               b2[i] = buf[offset + i];
            }
            
            _state.A ^= GjrandInteL64(ref b2, 0);
            _state.B ^= GjrandInteL64(ref b2, 8);
            _state.C ^= GjrandInteL64(ref b2, 16);
            GjrandInteMixstate();
         }

         _state.A ^= (ulong)buf.Length;
         _state.C = 3000001;
         GjrandInteMixstate();
      }

      public Gjrand(IReadOnlyList<uint> seed)
      {
         _state.A = ((ulong) seed[3] << 32) | seed[2];
         _state.B = ((ulong) seed[1] << 32) | seed[0];
         _state.C = 5000001;
         _state.D = 0;
         GjrandInteMixstate();
      }

      public Gjrand(Tuple<uint, uint, uint, uint> seeds)
      {
         var (x0, x1, x2, x3) = seeds;
         _state.A = ((ulong) x3 << 32) | x2;
         _state.B = ((ulong) x1 << 32) | x0;
         _state.C = 5000001;
         _state.D = 0;
         GjrandInteMixstate();
      }

      private void GjrandInteMixstate()
      {
         var ( a, b, c, d) = GjrandInteLoadState();
         for (var j = 14; j != 0; j--) (a, b, c, d) = GjrandInteCrank(a, b, c, d);
         GjrandStoreState(a, b, c, d);
      }

      private (ulong, ulong, ulong, ulong) GjrandInteCrank(ulong a, ulong b, ulong c, ulong d)
      {
         b += c;
         a = GjrandInteRot(a, 32);
         c ^= b;
         d += 0x55aa96a5;
         a += b;
         c = GjrandInteRot(c, 23);
         b ^= a;
         a += c;
         b = GjrandInteRot(b, 19);
         c += a;
         b += d;
         return (a, b, c, d);
      }

      private ulong GjrandInteRot(ulong x, int r)
      {
         return (x << r) | (x >> (64 - r));
      }

      private (ulong, ulong, ulong, ulong) GjrandInteLoadState()
      {
         var x = _state.B;
         var y = _state.C;
         var w = _state.A;
         var z = _state.D;
         return (w, x, y, z);
      }

      private void GjrandStoreState(ulong w, ulong x, ulong y, ulong z)
      {
         _state.A = w;
         _state.B = x;
         _state.C = y;
         _state.D = z;
      }

      private ulong GjrandInteL64(ref byte[] b, int offset)
      {
        return (   ((ulong) b[offset]  << 56)
                 | ((ulong) b[offset+1] << 48)
                 | ((ulong) b[offset+2] << 40)
                 | ((ulong) b[offset+3] << 32)
                 | ((ulong) b[offset+4] << 24)
                 | ((ulong) b[offset+5] << 16)
                 | ((ulong) b[offset+6] << 8)
                 | ((ulong) b[offset+7])
               );
      }

      public override ulong Next64()
      {
         Calls += 1;
         var (a, b, c, d) = GjrandInteLoadState();
         (a, b, c, d) = GjrandInteCrank(a, b, c, d);
         GjrandStoreState(a,b,c,d);
         return a;
      }
      
      public override uint Next32()
      {
         Calls += 1;
         throw new NotImplementedException("32 bit output not available from 64 bit generator");
      }

      public override int Next(int min, int max)
      {
         return min + (int)((max - min + 1) * Next01());
      }
      
      public override double Next01()
      {
         return (double) Next64() / ulong.MaxValue;
      }
   }
}
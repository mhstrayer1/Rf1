namespace Rf1.Random
{
   public abstract class Rng
   {
      public ulong Calls { get; protected set; } 
      
      public abstract uint Next32();
      public abstract ulong Next64(); 
      public abstract double Next01();
      public abstract int Next(int min, int max);
   }
}
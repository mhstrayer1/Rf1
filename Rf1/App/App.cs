using  static Rf1.Parameters.Parameters;

namespace Rf1.App
{
   public static class App
   {
      public static void InitializeDefaults()
      {
         AddParam("InputFile", "");
         AddParam("LogFile", "");
         
         AddParam("world.width", 19.0);
         AddParam("world.height", 13.0);
         AddParam("world.dpi", 300);
  
         AddParam("world.width", "double");
         AddParam("world.height", "double");
         AddParam("world.dpi", "int");

         AddParam("cmb.add.active", true);
         AddParam("cmb.sub.active", true);
         AddParam("cmb.mult.active", true);
         AddParam("cmb.div.active", true);
         AddParam("cmb.rand.active", true);
      
         AddParam("cmb.add.min_terms", 2);
         AddParam("cmb.add.max_terms", 4);
         AddParam("cmb.sub.min_terms", 2);
         AddParam("cmb.sub.max_terms", 4);
         AddParam("cmb.mult.min_terms", 2);
         AddParam("cmb.mult.max_terms", 4);
         AddParam("cmb.div.min_terms", 2);
         AddParam("cmb.div.max_terms", 4);
         AddParam("cmb.rand.min_terms", 2);
         AddParam("cmb.rand.max_terms", 4);
        
         AddParam("fcn.add.min_terms", 2);
         AddParam("fcn.add.max_terms", 4);
         AddParam("fcn.sub.min_terms", 2);
         AddParam("fcn.sub.max_terms", 4);
         AddParam("fcn.mult.min_terms", 2);
         AddParam("fcn.mult.max_terms", 4);
         AddParam("fcn.div.min_terms", 2);
         AddParam("fcn.div.max_terms", 4);
         AddParam("fcn.rand.min_terms", 2);
         AddParam("fcn.rand.max_terms", 4);
      }
   }
}
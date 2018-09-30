using System.Collections.Generic;

namespace Rf1.Parameters
{
   public static class Parameters
   {
      private static Dictionary<string, int> _IntParms;
      private static Dictionary<string, double> _DblParms;
      private static Dictionary<string, string> _StringParms;
      private static Dictionary<string, bool> _BoolParms;

      static Parameters()
      {
         _IntParms = new Dictionary<string, int>(128);
         _DblParms = new Dictionary<string, double>(1024);
         _StringParms = new Dictionary<string, string>(128);
         _BoolParms = new Dictionary<string, bool>(128);
      }

      public static (bool,string) AddParam(string key, int value)
      {
         if (_IntParms.ContainsKey(key))
         {
            return  (false, $"Key {key} already exists");
         }
         
         _IntParms.Add(key,value);
         return (true, "Ok");
      }
      
      public static (bool,string) AddParam(string key, double value)
      {
         if (_DblParms.ContainsKey(key))
         {
            return  (false, $"Key {key} already exists");
         }
         
         _DblParms.Add(key,value);
         return (true, "Ok");
      }

      
      public static (bool,string) AddParam(string key, string value)
      {
         if (_StringParms.ContainsKey(key))
         {
            return  (false, $"Key {key} already exists");
         }
         
         _StringParms.Add(key,value);
         return (true, "Ok");
      }
      
      public static (bool,string) AddParam(string key, bool value)
      {
         if (_BoolParms.ContainsKey(key))
         {
            return  (false, $"Key {key} already exists");
         }
         
         _BoolParms.Add(key,value);
         return (true, "Ok");
      }

      public static (bool, int) GetIntParam(string key)
      {
         return !_IntParms.ContainsKey(key) ? (false, -int.MaxValue) : (true, _IntParms[key]);
      }
      
      public static (bool, double) GetDblParam(string key)
      {
         return !_DblParms.ContainsKey(key) ? (false, -double.MaxValue) : (true, _DblParms[key]);
      } 
     
      public static (bool, string) GetStringParam(string key)
      {
         return !_StringParms.ContainsKey(key) ? (false, "") : (true, _StringParms[key]);
      } 
      
      public static (bool, bool) GetBoolParam(string key)
      {
         return !_BoolParms.ContainsKey(key) ? (false, false) : (true, _BoolParms[key]);
      } 
      
      public static (bool,string) UpdateParam(string key, int value)
      {
         if (!_IntParms.ContainsKey(key))
         {
            return  (false, $"Key {key} does not exists");
         }

         _IntParms[key] = value;
         return (true, "Ok");
      }
      
      public static (bool,string) UpdateParam(string key, double value)
      {
         if (!_DblParms.ContainsKey(key))
         {
            return  (false, $"Key {key} does not exists");
         }

         _DblParms[key] = value;
         return (true, "Ok");
      }
      
      public static (bool,string) UpdateParam(string key, string value)
      {
         if (!_StringParms.ContainsKey(key))
         {
            return  (false, $"Key {key} does not exists");
         }

         _StringParms[key] = value;
         return (true, "Ok");
      }
      
      public static (bool,string) UpdateParam(string key, bool value)
      {
         if (!_BoolParms.ContainsKey(key))
         {
            return  (false, $"Key {key} does not exists");
         }

         _BoolParms[key] = value;
         return (true, "Ok");
      }
   }
}
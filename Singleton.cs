using System;

public class Singleton
{
    public string Username { get; set; }
    public string Password { get; set; }
   private static Singleton instance;

   private Singleton() {}

   public static Singleton Instance
   {
      get 
      {
         if (instance == null)
         {
            instance = new Singleton();
         }
         return instance;
      }
   }
}
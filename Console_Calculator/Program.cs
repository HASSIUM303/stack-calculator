using System;

class Program
{
   public static Dictionary<char, char> Brackets { get; } = new Dictionary<char, char>
   {
     {'(', ')'},
     {'[', ']'},
     {'{', '}'}
   };

   static bool ValidateAllBrackets(string brackets)
   {

   }
   static bool IsStringBracketSequence(string str)
   {
      foreach (char s in str)
         if (!(Brackets.ContainsKey(s) || Brackets.ContainsValue(s)))
            return false;

      return true;
   }
}
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
      if (!IsStringBracketSequence(brackets)) return false;

      Stack<char> stack = new Stack<char>();

      foreach (char currentBracket in brackets)
      {
         if (Brackets.ContainsKey(currentBracket))
            stack.Push(currentBracket);
         else if (Brackets.ContainsValue(currentBracket))
         {
            if (stack.Count == 0)
               return false;

            if (Brackets[stack.Pop()] != currentBracket)
               return false;
         }
      }

      return stack.Count == 0;
   }
   static bool IsStringBracketSequence(string str)
   {
      foreach (char s in str)
         if (!(Brackets.ContainsKey(s) || Brackets.ContainsValue(s)))
            return false;

      return true;
   }
}
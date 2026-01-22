using System;
using System.Collections.Generic;

public delegate double MathOperationDelegate(double num1, double num2);

class Program
{
   public static Dictionary<char, char> Brackets { get; } = new Dictionary<char, char>
   {
     {'(', ')'},
     {'[', ']'},
     {'{', '}'}
   };

   static void Main()
   {
      while (true)
      {
         string input = Console.ReadLine() ?? "exit";

         if (input == "exit") break;

         if (!IsStringBracketSequence(input)) continue;

         if (!ValidateAllBrackets(input)) continue;
      }
   }

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
class Operation
{
   public string SymbolName { get; }
   public uint Priority;
   public bool IsDependenceOfOrder { get; }
   public event MathOperationDelegate OperationMeth
   {
      add { OperationMeth += value; }
      remove { }
   }

   public Operation(string SymbolName, int Priority, MathOperationDelegate meth, bool isOrder)
   {
      this.SymbolName = SymbolName;

      if (Priority >= 0) this.Priority = (uint)Priority;
      else this.Priority = 0;

      OperationMeth += meth;

      IsDependenceOfOrder = isOrder;
   }
}
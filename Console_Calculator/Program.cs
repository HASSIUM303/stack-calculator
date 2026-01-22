using System;

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

         Console.WriteLine("Ваш ввод с точки зрения скобок: " +
         (ValidateAllBrackets(input) ? "корректен" : "некорректен"));

         

         if (input == "exit") break;
      }
   }

   // static object[] InfixToPostfix(object[] infix)
   // {
   //    List<object> postfix = new List<object>();
   //    Stack<Operation> operations = new Stack<Operation>();

      

   //    return postfix.ToArray();
   // }
   static bool ValidateAllBrackets(string brackets)
   {
      brackets = StringToBracketSequence(brackets);

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

   static string StringToBracketSequence(string str)
   {
      string result = "";
      foreach (char s in str)
         if (Brackets.ContainsKey(s) || Brackets.ContainsValue(s))
            result += s;

      return result;
   }

   static object[] ParseInfixExpression(string expression)
   {
      List<object> tokens = new List<object>();
      string numberBuffer = "";

      foreach (char c in expression)
      {
         if (char.IsDigit(c) || c == '.')
         {
            numberBuffer += c;
         }
         else if (char.IsWhiteSpace(c))
         {
            if (numberBuffer != "")
            {
               tokens.Add(double.Parse(numberBuffer));
               numberBuffer = "";
            }
         }
         else if (Brackets.ContainsKey(c) || Brackets.ContainsValue(c) || "+-*/^".Contains(c))
         {
            if (numberBuffer != "")
            {
               tokens.Add(double.Parse(numberBuffer));
               numberBuffer = "";
            }
            tokens.Add(c);
         }
      }

      if (numberBuffer != "")
         tokens.Add(double.Parse(numberBuffer));

      return tokens.ToArray();
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
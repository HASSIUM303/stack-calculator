using System.Globalization;

partial class Program
{
   public static Dictionary<char, char> Brackets { get; } = new Dictionary<char, char>
   {
     {'(', ')'},
     {'[', ']'},
     {'{', '}'}
   };

   public static Dictionary<string, OperationBase> Operations { get; }

   static Program()
   {
      var power = new PowerOperation("^", 3);
      var divide = new DivideOperation("/", 2);

      Operations = new Dictionary<string, OperationBase>
      {
         { "+", new AddOperation("+", 1) },
         { "-", new SubtractOperation("-", 1) },
         { "*", new MultiplyOperation("*", 2) },
         { "/", divide },
         { ":", divide },
         { "^", power },
         { "**", power }
      };
   }

   static object[] ParseInfixExpression(string expression)
   {
      if (string.IsNullOrWhiteSpace(expression))
         throw new ArgumentException("Выражение не может быть пустым");

      expression = AllDotsToCommas(expression);

      List<object> tokens = new List<object>();
      string numberBuffer = "";
      Dictionary<OperationBase, string> usedAliases = new();

      for (int i = 0; i < expression.Length; i++)
      {
         char c = expression[i];

         if (char.IsDigit(c) || c == ',')
            numberBuffer += c;
         else if (c == '-' && IsUnaryMinus())
         {
            if (numberBuffer != "")
            {
               tokens.Add(double.Parse(numberBuffer));
               numberBuffer = "";
            }
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
         else if (Brackets.ContainsKey(c) || Brackets.ContainsValue(c))
         {
            if (numberBuffer != "")
            {
               tokens.Add(double.Parse(numberBuffer));
               numberBuffer = "";
            }
            tokens.Add(c.ToString());
         }
         else
         {
            string op = null;

            if (c == '*' && i + 1 < expression.Length && expression[i + 1] == '*')
            {
               op = "**";
               i++;
            }
            else
            {
               op = c.ToString();
            }

            if (Operations.ContainsKey(op))
            {
               if (numberBuffer != "")
               {
                  tokens.Add(double.Parse(numberBuffer));
                  numberBuffer = "";
               }
               OperationBase operation = Operations[op];

               if (usedAliases.TryGetValue(operation, out var alias) && alias != op)
                  throw new ArgumentException($"Оператор {op} нельзя смешивать с алиасом {alias} в одном выражении");

               usedAliases[operation] = op;
               tokens.Add(operation);
            }
         }
      }

      if (numberBuffer != "")
         tokens.Add(double.Parse(numberBuffer));

      return tokens.ToArray();


      static string AllDotsToCommas(string input)
      {
         if (input.Contains(',') && input.Contains('.'))
            throw new ArgumentException("Строка содержит одновременно запятые и точки");
         else if (input.Contains('.'))
            return input.Replace('.', ',');

         return input;
      }

      bool IsUnaryMinus()
      {
         bool hasTokens = tokens.Count > 0;

         if (!hasTokens && numberBuffer == "")
            return true;

         if (hasTokens)
         {
            object lastToken = numberBuffer == "" ? tokens[tokens.Count - 1] : numberBuffer;

            if (lastToken is string s && Brackets.ContainsKey(s[0]))
               return true;

            if (lastToken is OperationBase)
               return true;
         }

         return false;
      }
   }
   static object[] InfixToPostfix(object[] infix)
   {
      List<object> postfix = new List<object>();
      Stack<object> stack = new Stack<object>();

      for (int i = 0; i < infix.Length; i++)
      {
         if (infix[i] is double)
            postfix.Add(infix[i]);
         else if (infix[i] is OperationBase obj)
         {
            if (stack.Count == 0 || stack.Peek() is string)
               stack.Push(obj);
            else if (stack.Peek() is OperationBase)
            {
               while (((OperationBase)stack.Peek()).Priority >= obj.Priority)
               {
                  postfix.Add(stack.Pop());
                  if (stack.Count == 0 || stack.Peek() is not OperationBase) break;
               }
               stack.Push(obj);
            }
         }
         else if (infix[i] is string s && s.Length == 1)
         {
            if (Brackets.ContainsKey(s[0]))
               stack.Push(s);
            else if (Brackets.ContainsValue(s[0]))
            {
               while (stack.Count > 0 && stack.Peek() is OperationBase)
                  postfix.Add(stack.Pop());
               stack.Pop();
            }
         }
      }

      while (stack.Count > 0)
         postfix.Add(stack.Pop());

      return postfix.ToArray();
   }
   static double CalculatePostfix(object[] postfix)
   {
      Stack<double> stack = new();

      for (int i = 0; i < postfix.Length; i++)
      {
         if (postfix[i] is double num)
            stack.Push(num);
         else if (postfix[i] is OperationBase operation)
         {
            if (stack.Count < 2)
               throw new ArgumentException("Недостаточно операндов");

            double temp1 = stack.Pop();
            double temp2 = stack.Pop();
            stack.Push(operation.Apply(temp2, temp1));
         }
      }

      if (stack.Count != 1)
         throw new ArgumentException("В вычислительном стеке больше одного элемента");

      return stack.Pop();
   }
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
}

partial class Program
{
   public static Dictionary<char, char> Brackets { get; } = new Dictionary<char, char>
   {
     {'(', ')'},
     {'[', ']'},
     {'{', '}'}
   };
   public static Dictionary<string, Operation> Operations { get; } = new Dictionary<string, Operation>
   {
      { "+", new Operation("+", 1, (a, b) => a + b) },
      { "-", new Operation("-", 1, (a, b) => a - b) },
      { "*", new Operation("*", 2, (a, b) => a * b) },
      { "/", new Operation("/", 2, (a, b) => a / b) },
      { "^", new Operation("^", 3, (a, b) => Math.Pow(a, b)) }
   };

   static object[] ParseInfixExpression(string expression)
   {
      if (string.IsNullOrWhiteSpace(expression))
         throw new ArgumentException("Выражение не может быть пустым");

      List<object> tokens = new List<object>();
      string numberBuffer = "";

      for (int i = 0; i < expression.Length; i++)
      {
         char c = expression[i];

         if (char.IsDigit(c) || c == '.')
            numberBuffer += c;
         else if (c == '-' && IsUnaryMinus(tokens))
            numberBuffer += c;
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
         else if (Operations.ContainsKey(c.ToString()))
         {
            if (numberBuffer != "")
            {
               tokens.Add(double.Parse(numberBuffer));
               numberBuffer = "";
            }
            tokens.Add(Operations[c.ToString()]);
         }
      }

      if (numberBuffer != "")
         tokens.Add(double.Parse(numberBuffer));

      return tokens.ToArray();
   }
   static bool IsUnaryMinus(List<object> tokens)
   {
      if (tokens.Count == 0)
         return true;
      //Операция должна происходить после проверки!
      object lastToken = tokens[tokens.Count - 1];

      if (lastToken is string s && Brackets.ContainsKey(s[0]))
         return true;

      if (lastToken is Operation)
         return true;

      return false;
   }
   static object[] InfixToPostfix(object[] infix)
   {
      List<object> postfix = new List<object>();
      Stack<object> stack = new Stack<object>();

      for (int i = 0; i < infix.Length; i++)
      {
         if (infix[i] is double) //Операнд сразу попадает в ответ
            postfix.Add(infix[i]);
         else if (infix[i] is Operation obj)
         {
            if (stack.Count == 0 || stack.Peek() is string) //Если стек пуст или на его вершине скобка, мы кладём туда операцию
               stack.Push(obj);
            else if (stack.Peek() is Operation)
            {
               while (((Operation)stack.Peek()).Priority >= obj.Priority)
               {
                  postfix.Add(stack.Pop()); //Выталкиваем в ответ все операции с большим либо равным приоритетом
                  if (stack.Count == 0 || stack.Peek() is not Operation) break;
               }
               stack.Push(obj); //И в конце операция кладётся в стек
            }
         }
         else if (infix[i] is string s && s.Length == 1)
         {
            if (Brackets.ContainsKey(s[0]))
               stack.Push(s);
            else if (Brackets.ContainsValue(s[0]))
            {
               while (stack.Count > 0 && stack.Peek() is Operation)
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
      Stack<object> stack = new();

      for (int i = 0; i < postfix.Length; i++)
      {
         if (postfix[i] is double d)
            stack.Push(d);
         else if (postfix[i] is Operation o)
         {
            if (stack.Count < 2)
               throw new ArgumentException("Ошибка: недостаточно операндов");

            double temp1 = (double)stack.Pop();
            double temp2 = (double)stack.Pop();
            stack.Push(o.OperationMeth(temp2, temp1));
         }
      }

      if (stack.Count != 1)
         throw new ArgumentException("Ошибка: в вычислительном стеке больше одного элемента");

      return (double)stack.Pop();
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

class Operation
{
   public string SymbolName { get; }
   public uint Priority;
   public MathOperationDelegate OperationMeth { get; private set; }

   public Operation(string SymbolName, int Priority, MathOperationDelegate meth)
   {
      this.SymbolName = SymbolName;

      if (Priority >= 0) this.Priority = (uint)Priority;
      else this.Priority = 0;

      OperationMeth = meth;
   }
}

public delegate double MathOperationDelegate(double num1, double num2);
using System;

partial class Program
{
   static void Main()
   {
      while (true)
      {
         Console.Write(">");
         string input = Console.ReadLine() ?? "";

         if (input == "exit" || string.IsNullOrEmpty(input)) break;

         Console.WriteLine("\nВалидность скобочной последовательности: " + ValidateAllBrackets(input));

         Console.WriteLine("Вычисление: ");
         try
         {
            Console.WriteLine(" Разбиение строки на токены");
            object[] infix = ParseInfixExpression(input);
            Console.WriteLine($" Токены инфиксной записи:\t[ {string.Join(", ", infix)} ]");

            Console.WriteLine("\n Преобразование инфиксной записи в постфиксную");
            object[] postfix = InfixToPostfix(infix);
            Console.WriteLine($" Токены постфиксной записи:\t[ {string.Join(", ", postfix)} ]");

            Console.WriteLine("\n Вычисление постфиксной записи");
            double result = CalculatePostfix(postfix);
            Console.WriteLine(" Результат: " + result);
         }
         catch (Exception e)
         {
            Console.WriteLine();
            Console.WriteLine(e.GetType().Name);
            Console.WriteLine(e);
         }
      }
   }
}

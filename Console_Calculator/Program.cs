using System;

partial class Program
{
   static void Main()
   {
      while (true)
      {
         Console.Write(">");
         string input = Console.ReadLine() ?? "exit"; //TODO не возвращает null при вводе enter
         if (input == "exit") break;

         Console.WriteLine("\nВаш ввод с точки зрения скобок: " +
         (ValidateAllBrackets(input) ? "корректен" : "некорректен"));

         Console.WriteLine("Попытка парсинга строки в математическое выражение: ");
         try
         {
            object[] infix = ParseInfixExpression(input);
            Console.WriteLine("Парсинг успешен!");
            Console.WriteLine("Инфиксная запись: " + string.Join(", ", infix));

            Console.WriteLine("Преобразование инфиксной записи в постфиксную");
            object[] postfix = InfixToPostfix(infix);
            Console.WriteLine("Постфиксная запись: " + string.Join(", ", postfix));

            Console.WriteLine("Вычисление постфиксной записи");
            double result = CalculatePostfix(postfix);
            Console.WriteLine("Результат: " + result);
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

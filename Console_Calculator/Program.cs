using System;

partial class Program
{
   static void Main()
   {
      while (true)
      {
         Console.Write(">");
         string input = Console.ReadLine() ?? "exit";

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
            Console.WriteLine("Постфиксная запись: " + String.Join(", ", postfix));

            Console.WriteLine("Вычисление постфиксной записи");
            double result = CalculatePostfix(postfix);
            Console.WriteLine("Результат: " + result);
         }
         catch (Exception ex)
         {
            Console.WriteLine("Ошибка: " + ex.Message);
         }       

         if (input == "exit") break;
      }
   }
}

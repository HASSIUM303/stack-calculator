using System;

partial class Program
{
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
}

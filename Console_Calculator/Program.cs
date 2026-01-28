using System;

partial class Program
{
   static void Main()
   {
      while (true)
      {
         Console.Write(">>> ");
         string input = Console.ReadLine() ?? "";

         if (input == "exit" || string.IsNullOrEmpty(input)) break;

         Console.WriteLine("\nВалидность скобочной последовательности: " + ValidateAllBrackets(input));

         Console.WriteLine("Вычисление: ");
         try
         {
            Console.WriteLine(" Разбиение строки на токены");
            object[] infix = ParseInfixExpression(input);
            Console.WriteLine($" Токены инфиксной записи:\t[ {string.Join("; ", infix)} ]");

            Console.WriteLine("\n Преобразование инфиксной записи в постфиксную");
            object[] postfix = InfixToPostfix(infix);
            Console.WriteLine($" Токены постфиксной записи:\t[ {string.Join("; ", postfix)} ]");

            Console.WriteLine("\n Вычисление постфиксной записи");
            double result = CalculatePostfix(postfix);
            Console.WriteLine(" Результат: " + result);
         }
         catch (Exception e)
         {
            StylizeMessage(() =>
            {
               Console.WriteLine();
               Console.WriteLine(e.GetType().Name);
               Console.WriteLine(e.Message);

               Console.WriteLine();
               Console.WriteLine("Программа: " + e.Source);
               Console.WriteLine("Метод: " + e.TargetSite.Name);
               Console.WriteLine("Место ошибки:\n" + e.StackTrace);
               Console.WriteLine();

               Console.WriteLine("Нажмите Enter чтобы продолжить");
               Console.ReadKey();
            },
            ConsoleColor.Red);
         }
      }
   }
   static void StylizeMessage(Action meth, ConsoleColor color, bool CursorVisible = false)
   {
      ConsoleColor defaultColor = Console.ForegroundColor;
      Console.ForegroundColor = color;

      bool cursorSupported = true;
      bool defaultVisible = true;

      try { defaultVisible = Console.CursorVisible; }
      catch (PlatformNotSupportedException) { cursorSupported = false; }
      catch (IOException) { cursorSupported = false; }

      if (cursorSupported) Console.CursorVisible = CursorVisible;

      try
      {
         meth();
      }
      finally
      {
         Console.ForegroundColor = defaultColor;
         if (cursorSupported) Console.CursorVisible = defaultVisible;
      }
   }
}

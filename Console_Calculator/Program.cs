using System;

public delegate Double MathOperationDelegate(double num1, double num2);

class Program
{
   static void Main()
   {
      while (true)
      {
         string input = Console.ReadLine() ?? "exit";

         if (input == "exit") break;
      }
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
using System;

public interface IOperation
{
   string Symbol { get; }
   int Priority { get; }
   double Apply(double left, double right);
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

   public override string ToString()
   {
      return this.SymbolName;
   }
}

public delegate double MathOperationDelegate(double num1, double num2);
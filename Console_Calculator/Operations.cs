using System;

public abstract class OperationBase
{
   public string Symbol { get; }
   public int Priority { get; }
   protected OperationBase(string symbol, int priority)
   {
      Symbol = symbol;
      Priority = priority;
   }
   public abstract double Apply(double a, double b);
}

class AddOperation : OperationBase
{
   public AddOperation(string symbol, int priority) : base(symbol, priority) { }
   public override double Apply(double a, double b) => a + b;
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
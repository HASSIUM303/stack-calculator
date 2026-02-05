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
class SubtractOperation : OperationBase
{
   public SubtractOperation(string symbol, int priority) : base(symbol, priority) { }
   public override double Apply(double a, double b) => a - b;
}
class MultiplyOperation : OperationBase
{
   public MultiplyOperation(string symbol, int priority) : base(symbol, priority) { }
   public override double Apply(double a, double b) => a * b;
}

class DivideOperation : OperationBase
{
   public DivideOperation(string symbol, int priority) : base(symbol, priority) { }
   public override double Apply(double a, double b) => a / b;
}

class PowerOperation : OperationBase
{
   public PowerOperation(string symbol, int priority) : base(symbol, priority) { }
   public override double Apply(double a, double b) => Math.Pow(a, b);
}

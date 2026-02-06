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
   public virtual double Apply(double a, double b)
   {
      throw new NotSupportedException("Операция не поддерживает бинарное применение");
   }
}

interface IUnaryOperation
{
   double Apply(double a);
}
interface ITernaryOperation
{
   double Apply(double a, double b, double c);
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

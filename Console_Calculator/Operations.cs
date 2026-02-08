using System;

public abstract class OperationBase
{
   public string Symbol { get; }
   public int Priority { get; }
   public int Arity { get; }

   protected OperationBase(string symbol, int priority, int arity)
   {
      Symbol = symbol;
      Priority = priority;
      Arity = arity;
   }

   public double Apply(params double[] args)
   {
      if (args == null) throw new ArgumentNullException(nameof(args));
      if (args.Length != Arity)
         throw new ArgumentException($"Ожидалось {Arity} аргументов, получено {args.Length}");

      return DoApply(args.AsSpan());
   }

   protected abstract double DoApply(ReadOnlySpan<double> args);
}

class AddOperation : OperationBase
{
   public AddOperation(string symbol, int priority) : base(symbol, priority, 2) { }
   protected override double DoApply(ReadOnlySpan<double> args) => args[0] + args[1];
}
class SubtractOperation : OperationBase
{
   public SubtractOperation(string symbol, int priority) : base(symbol, priority, 2) { }
   protected override double DoApply(ReadOnlySpan<double> args) => args[0] - args[1];
}
class MultiplyOperation : OperationBase
{
   public MultiplyOperation(string symbol, int priority) : base(symbol, priority, 2) { }
   protected override double DoApply(ReadOnlySpan<double> args) => args[0] * args[1];
}

class DivideOperation : OperationBase
{
   public DivideOperation(string symbol, int priority) : base(symbol, priority, 2) { }
   protected override double DoApply(ReadOnlySpan<double> args) => args[0] / args[1];
}

class PowerOperation : OperationBase
{
   public PowerOperation(string symbol, int priority) : base(symbol, priority, 2) { }
   protected override double DoApply(ReadOnlySpan<double> args) => Math.Pow(args[0], args[1]);
}

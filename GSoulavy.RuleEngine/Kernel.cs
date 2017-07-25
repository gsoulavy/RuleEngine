using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace GSoulavy.RuleEngine
{
   public class Kernel
   {
      private readonly ICollection<(string Key, string Value)> _rules;

      public Kernel()
      {
         _rules = new List<(string Key, string Value)>();
      }

      public void AddRule(string key, string rule)
      {
         _rules.Add((key, rule));
      }

      public void RemoveRule(string key)
      {
         foreach (var rule in _rules.Where(r => r.Key.Equals(key)))
            _rules.Remove(rule);
      }

      public bool Validate<T>(T obj, string rule)
      {
         return Evaluate<T, bool>(obj, rule);
      }

      public bool ValidateAll<T>(T obj, string key = null)
      {
         return key != null
            ? _rules.Where(r => r.Key.Equals(key)).Select(r => r.Value).All(rule => Validate(obj, rule))
            : _rules.Select(r => r.Value).All(rule => Validate(obj, rule));
      }

      public bool ValidateAny<T>(T obj, string key = null)
      {
         return key != null
            ? _rules.Where(r => r.Key.Equals(key)).Select(r => r.Value).Any(rule => Validate(obj, rule))
            : _rules.Select(r => r.Value).Any(rule => Validate(obj, rule));
      }

      public TR Evaluate<T, TR>(T obj, string rule)
      {
         var parameter = Expression.Parameter(typeof(T), "o");
         var lambdaExpression = DynamicExpressionParser.ParseLambda(new[] {parameter}, null, rule);
         return (TR) lambdaExpression.Compile().DynamicInvoke(obj);
      }
   }
}
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace GSoulavy.RuleEngine
{
   public class Kernel : IKernel
   {
      private readonly List<(string Key, string Value)> _rules;

      public Kernel()
      {
         _rules = new List<(string Key, string Value)>();
      }

      public void AddRule(IRule rule)
      {
         _rules.Add((rule.Key, rule.Expression));
      }

      public void AddRules(IEnumerable<IRule> rules)
      {
         _rules.AddRange(rules.Select(r => (r.Key, r.Expression)));
      }

      public void RemoveRule(IRule rule)
      {
         _rules.Remove((rule.Key, rule.Expression));
      }

      public void RemoveRules(IEnumerable<IRule> rules)
      {
         foreach (var rule in rules)
            _rules.Remove((rule.Key, rule.Expression));
      }

      public bool Validate<T>(T fact, string rule)
      {
         return Evaluate<T, bool>(fact, rule);
      }

      public bool ValidateAll<T>(T fact, string key = null)
      {
         return key != null
            ? _rules.Where(r => r.Key.Equals(key)).Select(r => r.Value).All(rule => Validate(fact, rule))
            : _rules.Select(r => r.Value).All(rule => Validate(fact, rule));
      }

      public bool ValidateAny<T>(T fact, string key = null)
      {
         return key != null
            ? _rules.Where(r => r.Key.Equals(key)).Select(r => r.Value).Any(rule => Validate(fact, rule))
            : _rules.Select(r => r.Value).Any(rule => Validate(fact, rule));
      }

      public TR Evaluate<T, TR>(T fact, string rule)
      {
         var parameter = Expression.Parameter(typeof(T), "f");
         var lambdaExpression = DynamicExpressionParser.ParseLambda(new[] {parameter}, null, rule);
         return (TR) lambdaExpression.Compile().DynamicInvoke(fact);
      }
   }
}
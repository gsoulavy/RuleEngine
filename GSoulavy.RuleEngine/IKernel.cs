using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace GSoulavy.RuleEngine
{
   public interface IKernel
   {
      void AddRule(IRule rule);
      void AddRules(IEnumerable<IRule> rules);
      void RemoveRule(IRule rule);
      void RemoveRules(IEnumerable<IRule> rules);
      bool Validate<T>(T fact, string rule);
      bool ValidateAll<T>(T fact, string key = null);
      bool ValidateAny<T>(T fact, string key = null);
   }
}
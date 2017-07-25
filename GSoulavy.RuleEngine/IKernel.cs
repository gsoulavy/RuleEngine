namespace GSoulavy.RuleEngine
{
   public interface IKernel
   {
      void AddRule(string key, string rule);
      void RemoveRule(string key);
      bool Validate<T>(T obj, string rule);
      bool ValidateAll<T>(T obj, string key = null);
      bool ValidateAny<T>(T obj, string key = null);
   }
}
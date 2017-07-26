namespace GSoulavy.RuleEngine
{
   public interface IRule
   {
      string Key { get; set; }
      string Expression { get; set; }
   }
}
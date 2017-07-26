using GSoulavy.RuleEngine.Tests.Models;
using Xunit;

namespace GSoulavy.RuleEngine.Tests.Kernel
{
   public class EvaluateTests
   {
      [Fact(DisplayName = "Evaluate: false")]
      public void Evaluate_False()
      {
         // Arrange
         const string expression = @"(f.Age > 3 && f.Income > 100000) || f.NumberOfChildren > 5";
         var p = new Person {Age = 37, Income = 45000, NumberOfChildren = 3};
         var ruleEngine = new RuleEngine.Kernel();
         // Act
         var result = ruleEngine.Evaluate<Person, bool>(p, expression);
         // Assert
         Assert.False(result);
      }

      [Fact(DisplayName = "Evaluate: true")]
      public void Evaluate_True()
      {
         // Arrange
         const string expression = @"(f.Age > 3 && f.Income < 50000) || f.NumberOfChildren > 2";
         var p = new Person {Age = 37, Income = 45000, NumberOfChildren = 3};
         var ruleEngine = new RuleEngine.Kernel();
         // Act
         var result = ruleEngine.Evaluate<Person, bool>(p, expression);
         // Assert
         Assert.True(result);
      }
   }
}
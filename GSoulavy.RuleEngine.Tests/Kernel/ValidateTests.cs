using GSoulavy.RuleEngine.Tests.Models;
using Xunit;

namespace GSoulavy.RuleEngine.Tests.Kernel
{
   public class ValidateTests
   {
      [Fact(DisplayName = "Validate: false")]
      public void Validate_False()
      {
         // Arrange
         const string expression = @"(o.Age > 3 && o.Income > 100000) || o.NumberOfChildren > 5";
         var p = new Person {Age = 37, Income = 45000, NumberOfChildren = 3};
         var ruleEngine = new RuleEngine.Kernel();
         // Act
         var result = ruleEngine.Validate(p, expression);
         // Assert
         Assert.False(result);
      }

      [Fact(DisplayName = "Validate: true")]
      public void Validate_True()
      {
         // Arrange
         const string expression = @"(o.Age > 3 && o.Income < 50000) || o.NumberOfChildren > 2";
         var p = new Person {Age = 37, Income = 45000, NumberOfChildren = 3};
         var ruleEngine = new RuleEngine.Kernel();
         // Act
         var result = ruleEngine.Validate(p, expression);
         // Assert
         Assert.True(result);
      }
   }
}
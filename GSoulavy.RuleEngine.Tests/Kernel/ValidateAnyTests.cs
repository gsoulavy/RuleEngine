using GSoulavy.RuleEngine.Tests.Models;
using Xunit;

namespace GSoulavy.RuleEngine.Tests.Kernel
{
   public class ValidateAnyTests
   {
      [Fact(DisplayName = "ValidateAll[with param]: false")]
      public void ValidateAnyW_False()
      {
         // Arrange
         const string expression1 = @"(o.Age > 3 && o.Income < 50000) || o.NumberOfChildren > 2";
         const string expression2 = @"(o.Age > 3 && o.Income > 100000) || o.NumberOfChildren > 5";
         var p = new Person {Age = 37, Income = 45000, NumberOfChildren = 3};
         var ruleEngine = new RuleEngine.Kernel();
         ruleEngine.AddRule("1", expression1);
         ruleEngine.AddRule("2", expression2);
         // Act
         var result = ruleEngine.ValidateAny(p, "2");
         // Assert
         Assert.False(result);
      }

      [Fact(DisplayName = "ValidateAll[with param]: true")]
      public void ValidateAnyW_True()
      {
         // Arrange
         const string expression1 = @"(o.Age > 3 && o.Income < 50000) || o.NumberOfChildren > 2";
         const string expression2 = @"(o.Age > 3 && o.Income > 100000) || o.NumberOfChildren > 5";
         var p = new Person {Age = 37, Income = 45000, NumberOfChildren = 3};
         var ruleEngine = new RuleEngine.Kernel();
         ruleEngine.AddRule("1", expression1);
         ruleEngine.AddRule("2", expression2);
         // Act
         var result = ruleEngine.ValidateAny(p, "1");
         // Assert
         Assert.True(result);
      }

      [Fact(DisplayName = "ValidateAll[without param]: true")]
      public void ValidateAnyWp_False()
      {
         // Arrange
         const string expression1 = @"(o.Age > 3 && o.Income < 50000) || o.NumberOfChildren > 2";
         const string expression2 = @"(o.Age > 3 && o.Income > 100000) || o.NumberOfChildren > 5";
         var p = new Person {Age = 37, Income = 45000, NumberOfChildren = 3};
         var ruleEngine = new RuleEngine.Kernel();
         ruleEngine.AddRule("1", expression1);
         ruleEngine.AddRule("2", expression2);
         // Act
         var result = ruleEngine.ValidateAny(p);
         // Assert
         Assert.True(result);
      }
   }
}
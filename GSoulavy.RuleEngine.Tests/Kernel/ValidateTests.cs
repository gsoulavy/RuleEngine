using GSoulavy.RuleEngine.Tests.Models;
using Xunit;

namespace GSoulavy.RuleEngine.Tests.Kernel
{
   using RuleEngine;

   public class ValidateTests
   {
      [Fact(DisplayName = "Validate: true")]
      public void Evaluate_True()
      {
         // Arrange
         const string expression = @"(o.Age > 3 && o.Income < 50000) || o.NumberOfChildren > 2";
         var p = new Person { Age = 37, Income = 45000, NumberOfChildren = 3 };
         var ruleEngine = new Kernel();
         // Act
         var result = ruleEngine.Validate(p, expression);
         // Assert
         Assert.True(result);
      }

      [Fact(DisplayName = "Validate: false")]
      public void Evaluate_False()
      {
         // Arrange
         const string expression = @"(o.Age > 3 && o.Income > 100000) || o.NumberOfChildren > 5";
         var p = new Person { Age = 37, Income = 45000, NumberOfChildren = 3 };
         var ruleEngine = new Kernel();
         // Act
         var result = ruleEngine.Validate(p, expression);
         // Assert
         Assert.False(result);
      }

      [Fact(DisplayName = "ValidateAll[without param]: true")]
      public void EvaluateAllWp_True()
      {
         // Arrange
         const string expression = @"(o.Age > 3 && o.Income < 50000) || o.NumberOfChildren > 2";
         var p = new Person { Age = 37, Income = 45000, NumberOfChildren = 3 };
         var ruleEngine = new Kernel();
         ruleEngine.AddRule("1", expression);
         // Act
         var result = ruleEngine.ValidateAll(p);
         // Assert
         Assert.True(result);
      }

      [Fact(DisplayName = "ValidateAll[with param]: true")]
      public void EvaluateAllW_True()
      {
         // Arrange
         const string expression1 = @"(o.Age > 3 && o.Income < 50000) || o.NumberOfChildren > 2";
         const string expression2 = @"(o.Age > 3 && o.Income > 100000) || o.NumberOfChildren > 5";
         var p = new Person { Age = 37, Income = 45000, NumberOfChildren = 3 };
         var ruleEngine = new Kernel();
         ruleEngine.AddRule("1", expression1);
         ruleEngine.AddRule("2", expression2);
         // Act
         var result = ruleEngine.ValidateAll(p, "1");
         // Assert
         Assert.True(result);
      }

      [Fact(DisplayName = "ValidateAll[with param]: false")]
      public void EvaluateW_False()
      {
         // Arrange
         const string expression1 = @"(o.Age > 3 && o.Income < 50000) || o.NumberOfChildren > 2";
         const string expression2 = @"(o.Age > 3 && o.Income > 100000) || o.NumberOfChildren > 5";
         var p = new Person { Age = 37, Income = 45000, NumberOfChildren = 3 };
         var ruleEngine = new Kernel();
         ruleEngine.AddRule("1", expression1);
         ruleEngine.AddRule("2", expression2);
         // Act
         var result = ruleEngine.ValidateAll(p, "2");
         // Assert
         Assert.False(result);
      }

      [Fact(DisplayName = "ValidateAll[without param]: false")]
      public void EvaluateWp_False()
      {
         // Arrange
         const string expression1 = @"(o.Age > 3 && o.Income < 50000) || o.NumberOfChildren > 2";
         const string expression2 = @"(o.Age > 3 && o.Income > 100000) || o.NumberOfChildren > 5";
         var p = new Person { Age = 37, Income = 45000, NumberOfChildren = 3 };
         var ruleEngine = new Kernel();
         ruleEngine.AddRule("1", expression1);
         ruleEngine.AddRule("2", expression2);
         // Act
         var result = ruleEngine.ValidateAll(p);
         // Assert
         Assert.False(result);
      }

   }
}
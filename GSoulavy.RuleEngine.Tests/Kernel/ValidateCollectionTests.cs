using System.Collections.Generic;
using GSoulavy.RuleEngine.Tests.Models;
using Xunit;

namespace GSoulavy.RuleEngine.Tests.Kernel
{
   using RuleEngine;
   public class ValidateCollectionTests
   {
      [Fact(DisplayName = "ValidateCollection: true")]
      public void ValidateCollection_True()
      {
         // Arrange
         var people = new List<Person>
         {
            new Person {Name = "John", Gender = Gender.Male, Age = 23, Income = 24000, NumberOfChildren = 0},
            new Person {Name = "Mary", Gender = Gender.Female, Age = 22, Income = 23000, NumberOfChildren = 1}
         };

         const string expression = "o.Any(p => p.Name.Equals(\"John\") && p.Age < 24)";
         var ruleEngine = new Kernel();
         // Act
         var result = ruleEngine.Validate<IEnumerable<Person>>(people, expression);

         // Assert
         Assert.True(result);
      }
   }
}
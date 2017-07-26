<link rel="stylesheet" href="/highlight/styles/default.css">

# RuleEngine

### Purpose

This simple rule engine is a .NET Standard library 1.4, which uses Microsoft's DLR DynamicExpressionParser in the background. The goal was to make a simple engine which is easy to use and compatible with many projects.

### Use

#### Instantiating the kernel

The IKernel interface is implemented with Kerner in order to support Inverson Of Control.

```cs
IKernel ruleEngine = new Kernel();
```

##### Simple Validation
String expressions can be simply against the object passed to the engine.<br/>
Creating the object:
```cs
var p = new Person {Age = 37, Income = 45000, NumberOfChildren = 3};
```

Validating the object:
```cs
const string expression = "(o.Age > 3 && o.Income > 100000) || o.NumberOfChildren > 5";
var result = ruleEngine.Validate(p, expression);
// result = false
```

##### Validate All
More than one expreession can be added to the engine
```cs
var rules = new List<Rule>
{
	new Rule {Key = "1", Expression = "(o.Age > 3 && o.Income < 50000) || o.NumberOfChildren > 2"},
	new Rule {Key = "2", Expression = "(o.Age > 3 && o.Income > 100000) || o.NumberOfChildren > 5"}
};

ruleEngine.AddRules(rules);

// Validate against all rules when no key passed
var result = ruleEngine.ValidateAll(p);
// result = false

// Only validate against rules with the matching key
var result = ruleEngine.ValidateAll(p, "1");
// result = true
```

##### Validate Any
The calls are the same as the case of validate all, however it returns true if any case is true.
```cs
var rules = new List<Rule>
{
	new Rule {Key = "1", Expression = "(o.Age > 3 && o.Income < 50000) || o.NumberOfChildren > 2"},
	new Rule {Key = "2", Expression = "(o.Age > 3 && o.Income > 100000) || o.NumberOfChildren > 5"}
};

ruleEngine.AddRules(rules);

// Validate against all rules when no key passed
var result = ruleEngine.ValidateAny(p);
// result = true

// Only validate against rules with the matching key
var result = ruleEngine.ValidateAny(p, "1");
// result = true
```
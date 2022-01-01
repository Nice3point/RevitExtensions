Extensions minimize the writing of repetitive code, add new methods not included in RevitApi, and also allow you to write chained methods without worrying about API versioning:

```c#
new ElementId(123469)
.ToElement(document)
.GetParameter(BuiltInParameter.DOOR_HEIGHT)
.AsDouble()
.Round()
.ToMillimeters()
```

Extensions include annotations to help ReShaper parse your code and signal when a method may return null or the value returned by the method is not used in your code.
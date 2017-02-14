Boomlagoon JSON

This package is a lightweight C# JSON implementation.
Simply import it into your Unity project and it's ready to use.

Boomlagoon JSON doesn't throw exceptions so it's compatible with Unity's
'Fast but no Exceptions' script call optimization.

There is no casting involved, instead all valid JSON values are
accessible as the correct C# types.

Usage:

Parsing a string into a JSONObject:
--
using Boomlagoon.JSON;

string text = "{ \"sample\" : 123 }";
JSONObject json = JSONObject.Parse(text);
double number = json.GetNumber("sample");
--

Creating a JSONObject:
--
var obj = new JSONObject();
obj.Add("key", "value");
obj.Add("otherKey", 1234);
obj.Add("bool", true);

//Alternative method:
var obj = new JSONObject {
	{"key", "value"}, 
	{"otherKey", 1234}, 
	{"bool", true}
};
--

Check out TestScene/JSONObjectTester.cs for more examples.

Version 1.2 Changes:
	-Replaced usage of Stack<T> with List<T> to remove dependency to system.dll as per Mike Weldon's suggestion.

Send feedback and questions to feedback@boomlagoon.com
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, Implicit Usings demo!");

Person ctorInstance = new();
Person defaultInstance = default;

Console.WriteLine($"Ctor instance content\n {ctorInstance}");
Console.WriteLine($"Default instancecontent\n {defaultInstance}");

Console.WriteLine($"Parameterless constructor and default are equal: {ctorInstance == defaultInstance}");
Console.WriteLine();

var ford = ctorInstance with { LastName = "Prefect" };

Console.WriteLine($"Ford instance created by with expression produces\n {ford}");
Console.WriteLine();

var anonymous = new
{
    FirstName = "Zaphod",
    LastName = "Beeblebrox"
};

Console.WriteLine($"Anonymous instance content\n {anonymous}");

var otherAnonymous = anonymous with { LastName = "Trillian" };

Console.WriteLine($"Other anonymus created by with expression produces\n {otherAnonymous}");

Console.ReadLine();


# Whats new in .NET 6.0 and C# 10

This notebook will help us to get a glance and first impression on the changes to .NET 6 and C# 10 that are or at least could be relevant for the development of the CCO. This book will contains links (Timestamps to be more precise) to the recording of the conference over on [YouTube](https://youtu.be/oPyTZ-HGdn4).

Everything that is new in VS 2022 will not be covered here. Follow up the [launch event](https://visualstudio.microsoft.com/launch/) if you want to learn more about it.

## Global usings and Implicite Usings

All code files must declare the usings for it. Which leads to a lot of duplicated stuff in your files. Sure the IDE supports you in various ways, like importing the usings on past or tell you which usings you are missing but you have to do this for all files. Often, or even more likely you will have a lot of the same usings across your files. Global Usings will change that.

### Sample Global usings 'class'

With global usings you define a file eg. `GlobalUsings.cs` and all usings which are similar or needed in your application can be located there. 
Your `GlobalUsings.cs` file could look somthing like this

``` CSharp
global using System;
global using System.Collections.Generic;
global using MyAwesomeApp.Model;

```
By using the `global` keyword in front of the using, .NET knows that you want to use this in your whole project.

Everything you can do with the `using` keyword can be done with the `gloabl` keyword as well.

``` csharp
global using static System.Console;
global using Env = System.Environment;
```

### Sample usings in *.csproj file

You can also diffine such usings in the *.csproj file of which would look something like this.

To do so `ImplicitUsings` must be enabled in your project

``` xml
<PropertyGroup>
    <!-- Other properties for your project -->
    <ImplicitUsings>enable</ImplicitUsings>
</PropertyGroup>
```

Define your usings like

``` xml
<ItemGroup>
  <Using Include="System" />
  <Using Include="System.Collections.Generic" />
  <Using Include="MyAwesomeApp.Model" />
</ItemGroup>
```
As of now it appears that this config could not be used in a Directory.build.props file.  
Find all details in the [docu](https://docs.microsoft.com/en-us/dotnet/core/compatibility/sdk/6.0/implicit-namespaces-rc1)

# File-scoped namespaces

A typical C# Class looks something like this

``` CSharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleNamespace
{
    public class AwesomeClass
    {
        // all your awesome stuff
    }
}

```

We already tackled the using statements in [Global usings and Implicite Usings](#Global-usings-and-Implicite-Usings). Now we can remove a bit more code by getting rid of the indentation caused by the `namespace` keyword. We can write our class like

``` CSharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleNamespace;

public class AwesomeClass
{
    // all your awesome stuff
}
```

As you can see we terminate the keyword `namespace` with a `;` and remove the curly braces which are coming with it. A guide on how to add this to your .editorconfig can be found [here](https://ardalis.com/dotnet-format-and-file-scoped-namespaces/)

## Some new magic with records

### Record Struct
The keyword `record` can now be applied to structs. Until now `record`s are are always a `class` in the background. With .NET 6 structs can also become records. But what can I do with this now?

Now you can use the short syntax to create a struct.

```csharp
public record struct Person(string FirstName, string Lastname)
{
    public string FirstName { get; init; } = FirstName ?? throw new ArgumentNullExceptions(nameof(FirstName))
    public string LastName { get; init; } = LastName ?? throw new ArgumentNullExceptions(nameof(LastName))
}
```

## Default/empty constructor in structs

```csharp
public struct Person
{
    public Person()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public string FirstName { get; init; }
    public string LastName { get; init; }
}

// usage
public void MyMethod()
{
    Person defaultStruct = default;
    Person emptyCtorStruct = new();

    defaultStruct == emptyCtorStruct // results to false
}
```

With this change you can also write a struct using initializers

```csharp
public struct Person
{
    public string FirstName { get; init; } = "Arthur";
    public string LastName { get; init; } = "Dent";
}
```

[Recording](https://www.youtube.com/watch?v=oPyTZ-HGdn4&t=6134s)

## With expression with anonymous types

With C# 10 we can now use the `with` keyword on anonymous types.

```csharp
var pers = new 
{
        FirstName = "Arthur";
        LastName = "Dent";
}

var otherPerson = pers with {ListName = "Conner"}
```

### Unwrapping records using the with- keyword

As of .NET 5 records can be deconstructed using

```csharp
public record struct Person(string FirstName, string Lastname)
{
    public string FirstName { get; init; } = FirstName ?? throw new ArgumentNullExceptions(nameof(FirstName))
    public string LastName { get; init; } = LastName ?? throw new ArgumentNullExceptions(nameof(LastName))
}

// usage
public void MyMethod()
{
    Person pers = new("Arthur", "Dent");

    // deconstructing
    (firstName, lastName) = pers;

    Consol.WriteLine($"{firstName}, {lastName}") // Prints Arthur, Dent

    var otherPerson = pers with { LastName = "Conner"};

    Console.WriteLine($"{otherPerson.FirstName}, {otherPerson.LastName}") // Prints Arthur, Conner
}
```

## Compiler can now infer Func from lamdas

Till now if you want a `Func` to use you would have to declare it like  

``` csharp
Func<string, int> parse = (string s) => int.Parse(s)
```

The new way to do this now is to write  

```csharp
var parse = (string s) => int.Parse(s)
```

You can also use `ref` keyword there

```charp
var parse = (ref string s) => int.Parse(s)
```

This also gives us the possibility to assign lambdas to objects or Delegates

``` csharp
object parse   = (string s) => int.Parse(s);
Delegate parse = (string s) => int.Parse(s);
```

## Optimization in string interpolation

Currently the compiler turn string interpolations

```csharp
$"Hello {FirstName}!"
```

into

```csharp
string.Format("Hello {0}!, FirstName")
```

This could lead to a lot of allocations for a simple string. In C# 10 a library pattern allows the API to handle the interpolation of string argument expressions.

``` csharp
var builder = new StringBuilder();
sb.Append($"Hello {FirstName}!");
```

The Append method now has an overload `Append(ref StringBuilder.AppendInterpolatedStringHandler handler)` which takes precedence over the string overload when an interpolated string is provided.

---

## Additional Stuff not as interesting for our work

## Testing

Not much news here beside that the testing and live unit test have gotten perf improvements.

## EF Core

### Migration bundles are introduced

Migration Bundles are build from your migration script(s), it will be a stand alone exe file which can be run in the CI/CD pipeline without any additional tooling. The exe will migrate your database like you use the `migrate-database` command.

### GraphQL

[GraphQL](https://graphql.org/) can be integrated into your EF Core Context. At least according to the demo, without any additional implementation besides the setup. I haven't taken a closer look at it by now, there is a lib called [Hotchoclate](https://github.com/ChilliCream/hotchocolate) to ease the setup.

## Blazor

A lot of overall perf and feature improvements. One that cought my eye was that we are able to bundle EF Core and SQLite into a webassembly and can run it on the client.
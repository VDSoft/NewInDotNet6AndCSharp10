namespace RecordsDemo;

public record struct Person(string FirstName, string LastName)
{
    public Person()
        : this("Arthur", "Dent")
    {
    }

    public string FirstName { get; init; } = FirstName ?? throw new ArgumentNullException(nameof(FirstName));

    public string LastName { get; init; } = LastName ?? throw new ArgumentNullException(nameof(LastName));
}

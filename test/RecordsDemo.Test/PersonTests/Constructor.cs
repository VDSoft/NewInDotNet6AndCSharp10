namespace RecordsDemo.Test.PersonTests
{
    public class Constructor
    {
        [Fact]
        public void Constructor_DefaulAndConstructorInstance_AreNotEqual()
        {
            // Arrange
            Person ctorInstance = new();
            Person defaultInstance = default;

            // Act
            var actual = ctorInstance.Equals(defaultInstance);

            // Assert
            actual.Should().BeFalse();
        }
    }
}

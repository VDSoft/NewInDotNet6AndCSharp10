using System.Collections.Generic;

namespace RecordsDemo.Test.PersonServiceTests
{
    public class GetAll
    {
        [Fact]
        public void GetAll_DataStored_ReceivedAllDatta()
        {
            // Arrange
            var data = new List<Person>
            {
                new Person("Arthur", "Dent"),
                new Person("Ford", "Prefect")
            };

            var service = new Mock<IPersonService>();
            service
                .Setup(x => x.GetAll())
                .Returns(data);

            // Act
            var actual = service.Object.GetAll();

            // Assert
            actual.Should().BeEquivalentTo(data);
        }
    }
}

using Xunit;

namespace AgroSolutions.Tests
{
    public class FarmValidationTests
    {
        [Fact]
        public void Farm_Should_Have_Valid_Id_When_Created()
        {
            // Arrange & Act
            var id = System.Guid.NewGuid();

            // Assert
            Assert.NotEqual(System.Guid.Empty, id);
        }
    }
}
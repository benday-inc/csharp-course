using NunitLab.Api;

namespace NunitLab.UnitTests;

[TestFixture]
public class BigImportantServiceTests
{
    public BigImportantService? _systemUnderTest;

    [SetUp]
    public void Setup()
    {
        _systemUnderTest = new BigImportantService();
    }

    public BigImportantService SystemUnderTest
    {
        get
        {
            if (_systemUnderTest == null)
            {
                _systemUnderTest = new BigImportantService();
            }

            return _systemUnderTest;
        }
    }

    [Test]
    public async Task DoSomethingImportant()
    {
        // Arrange
        var expected = 42;

        // Act
        var result = await SystemUnderTest.DoSomethingImportant();

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}



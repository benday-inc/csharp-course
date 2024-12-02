using NunitLab.Api;

namespace NunitLab.UnitTests;

[TestFixture]
public class CalculatorTests
{
    private Calculator? _systemUnderTest;

    public Calculator SystemUnderTest
    {
        get
        {
            if (_systemUnderTest == null)
            {
                _systemUnderTest = new Calculator();
            }

            Assert.That(_systemUnderTest, Is.Not.Null);

            return _systemUnderTest;
        }
    }

    [TearDown]
    public void TearDown()
    {
        _systemUnderTest = null;
    }

    [Test]
    public void Add_ShouldReturnCorrectSum()
    {
        // arrange 
        var value1 = 2;
        var value2 = 3;
        var expected = 5;

        // act
        var actual = SystemUnderTest.Add(value1, value2);

        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Subtract_ShouldReturnCorrectDifference()
    {
        // arrange 
        var value1 = 2;
        var value2 = 3;
        var expected = -1;

        // act
        var actual = SystemUnderTest.Subtract(value1, value2);

        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Multiply_ShouldReturnCorrectProduct()
    {
        // arrange 
        var value1 = 2;
        var value2 = 3;
        var expected = 6;

        // act
        var actual = SystemUnderTest.Multiply(value1, value2);

        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Divide_ShouldReturnCorrectQuotient()
    {
        // arrange 
        var value1 = 6;
        var value2 = 3;
        var expected = 2;

        // act
        var actual = SystemUnderTest.Divide(value1, value2);

        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}





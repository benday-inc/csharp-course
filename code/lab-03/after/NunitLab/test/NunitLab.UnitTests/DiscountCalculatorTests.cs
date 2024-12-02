using NunitLab.Api;

namespace NunitLab.UnitTests;

[TestFixture]
public class DiscountCalculatorTests
{
    private DiscountCalculator? _systemUnderTest;

    [SetUp]
    public void SetUp()
    {
        _systemUnderTest = null;
    }

    public DiscountCalculator SystemUnderTest
    {
        get
        {
            if (_systemUnderTest == null)
            {
                _systemUnderTest = new DiscountCalculator();
            }

            Assert.That(_systemUnderTest, Is.Not.Null);

            return _systemUnderTest;
        }
    }

    [TestCase(100, "Regular", 5)]
    [TestCase(200, "Premium", 20)]
    [TestCase(300, "VIP", 60)]
    [TestCase(150, "Unknown", 0)]
    public void CalculateDiscount_ShouldReturnExpectedDiscount(
        decimal totalAmount, string membershipType, decimal expectedDiscount)
    {
        // Act
        decimal actualDiscount = SystemUnderTest.CalculateDiscount(totalAmount, membershipType);

        // Assert
        Assert.That(actualDiscount, Is.EqualTo(expectedDiscount));
    }

    public static IEnumerable<object[]> DiscountTestCases()
    {
        yield return new object[] { 500m, "Regular", 25m };
        yield return new object[] { 400m, "Premium", 40m };
        yield return new object[] { 1000m, "VIP", 200m };
    }

    [Test, TestCaseSource(nameof(DiscountTestCases))]
    public void CalculateDiscount_WithTestCaseSource_ShouldReturnExpectedDiscount(
        decimal totalAmount, string membershipType, decimal expectedDiscount)
    {
        // Act
        decimal actualDiscount = SystemUnderTest.CalculateDiscount(
            totalAmount, membershipType);

        // Assert
        Assert.That(actualDiscount, Is.EqualTo(expectedDiscount));
    }
}

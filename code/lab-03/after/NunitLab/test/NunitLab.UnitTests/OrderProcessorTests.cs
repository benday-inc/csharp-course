using Moq;
using NunitLab.Api;

namespace NunitLab.UnitTests;

[TestFixture]
public class OrderProcessorTests
{

    private OrderProcessor? _systemUnderTest;
    private Mock<IEmailService>? _emailServiceMock;
    private Mock<IShippingService>? _shippingServiceMock;

    public OrderProcessor SystemUnderTest
    {
        get
        {
            if (_systemUnderTest == null)
            {
                _systemUnderTest = new OrderProcessor(
                    ShippingServiceMock.Object,
                    EmailServiceMock.Object);
            }

            Assert.That(_systemUnderTest, Is.Not.Null);
            Assert.That(_emailServiceMock, Is.Not.Null);

            return _systemUnderTest;
        }
    }

    public Mock<IEmailService> EmailServiceMock
    {
        get
        {
            if (_emailServiceMock == null)
            {
                _emailServiceMock = new Mock<IEmailService>();
            }

            Assert.That(_emailServiceMock, Is.Not.Null);

            return _emailServiceMock;
        }
    }

    public Mock<IShippingService> ShippingServiceMock
    {
        get
        {
            if (_shippingServiceMock == null)
            {
                _shippingServiceMock = new Mock<IShippingService>();
            }

            Assert.That(_shippingServiceMock, Is.Not.Null);

            return _shippingServiceMock;
        }
    }

    [SetUp]
    public void SetUp()
    {
        _systemUnderTest = null;
        _emailServiceMock = null;
        _shippingServiceMock = null;
    }

    [Test]
    public void ProcessOrder_WhenOrderIdIsValid_ShouldSendEmail()
    {
        // Arrange
        string orderId = "12345";
        string customerEmail = "customer@example.com";

        // Act
        var actual = SystemUnderTest.ProcessOrder(orderId, customerEmail);

        // Assert
        Assert.That(actual, Is.True);

        EmailServiceMock.Verify(
            x => x.SendEmail(
                customerEmail, 
                "Order Confirmation", 
                $"Your order {orderId} has been processed."),
            Times.Once);
    }

    [Test]
    public void ProcessOrder_WhenOrderIdIsInvalid_ShouldNotSendEmail()
    {
        // Arrange
        string orderId = "";
        string customerEmail = "customer@example.com";

        // Act
        var actual = SystemUnderTest.ProcessOrder(orderId, customerEmail);

        // Assert
        Assert.That(actual, Is.False);
        EmailServiceMock.Verify(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void ProcessOrderAndShip_ShouldCallCalculateShippingCost_WithCorrectParameters()
    {
        // Arrange
        var orderId = "12345";
        var email = "customer@email.com";
        var destination = "Decimal Point, RI";
        var weight = 5.0;
        var expectedShippingCost = 10.0m;

        ShippingServiceMock.Setup(s => s.CalculateShippingCost(destination, weight))
            .Returns(expectedShippingCost);
        ShippingServiceMock.Setup(s => s.ValidateShippingDetails(destination, weight))
            .Returns(true);

        // Act
        var actualShippingCost = SystemUnderTest.ProcessOrderAndShip(orderId, email, destination, weight);

        // Assert
        Assert.That(actualShippingCost, Is.EqualTo(expectedShippingCost));
        ShippingServiceMock.Verify(s => s.CalculateShippingCost(destination, weight), Times.Once);
    }

    [Test]
    public void ProcessOrderAndShip_ShouldSendEmail_WithShippingDetails()
    {
        // Arrange
        var orderId = "34562";
        var email = "customer@email.com";
        var destination = "Backflip, MT";
        double weight = 2.5;
        decimal shippingCost = 15.0m;

        ShippingServiceMock.Setup(s => s.CalculateShippingCost(destination, weight))
            .Returns(shippingCost);
        ShippingServiceMock.Setup(s => s.ValidateShippingDetails(destination, weight))
            .Returns(true);

        EmailServiceMock.Setup(e => e.SendEmail(email, It.IsAny<string>(), It.IsAny<string>()))
            .Callback<string, string, string>((recipient, subject, body) =>
            {
                Assert.That(subject, Is.EqualTo("Order Confirmation"));
                Assert.That(body, Contains.Substring($"shipping cost of {shippingCost:C}"));
                Assert.That(email, Is.EqualTo(recipient));                
            });

        // Act
        _ = SystemUnderTest.ProcessOrderAndShip(orderId, email, destination, weight);

        // Assert
        EmailServiceMock.Verify(e => e.SendEmail(email, It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}

using Moq;
using NunitLab.Api;

namespace NunitLab.UnitTests;

[TestFixture]
public class OrderProcessorTests
{

    private OrderProcessor? _systemUnderTest;
    private Mock<IEmailService>? _emailServiceMock;

    public OrderProcessor SystemUnderTest
    {
        get
        {
            if (_systemUnderTest == null)
            {
                _systemUnderTest = new OrderProcessor(EmailServiceMock.Object);
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

    [SetUp]
    public void SetUp()
    {
        _systemUnderTest = null;
        _emailServiceMock = null;
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
}

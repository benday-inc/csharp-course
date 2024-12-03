using NunitLab.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NunitLab.UnitTests;

[TestFixture]
public class BankAccountTests
{
    private const decimal INITIAL_BALANCE = 100;
    private BankAccount? _systemUnderTest;
    public BankAccount SystemUnderTest
    {
        get
        {
            if (_systemUnderTest == null)
            {
                _systemUnderTest = new BankAccount(INITIAL_BALANCE);
            }
            Assert.That(_systemUnderTest, Is.Not.Null);
            return _systemUnderTest;
        }
    }

    [SetUp]
    public void SetUp()
    {
        _systemUnderTest = null;
    }

    [Test]
    public void Deposit_WhenAmountIsPositive_ShouldIncreaseBalance()
    {
        // Arrange
        decimal depositAmount = 50;

        // Act
        SystemUnderTest.Deposit(depositAmount);

        // Assert
        Assert.That(SystemUnderTest.Balance, Is.EqualTo(INITIAL_BALANCE + depositAmount));
    }

    [Test]
    public void Deposit_WhenAmountIsNegative_ShouldThrowArgumentException()
    {
        // Arrange
        decimal depositAmount = -50;
        
        // Act & Assert
        Assert.That(() => SystemUnderTest.Deposit(depositAmount), Throws.ArgumentException);
    }

    [Test]
    public void Withdraw_WhenAmountIsPositive_ShouldDecreaseBalance()
    {
        // Arrange
        decimal withdrawalAmount = 50;
        
        // Act
        SystemUnderTest.Withdraw(withdrawalAmount);

        // Assert
        Assert.That(SystemUnderTest.Balance, Is.EqualTo(INITIAL_BALANCE - withdrawalAmount));
    }

    [Test]
    public void Withdraw_WhenAmountIsNegative_ShouldThrowArgumentException()
    {
        // Arrange        
        decimal withdrawalAmount = -50;
        
        // Act & Assert
        Assert.That(() => SystemUnderTest.Withdraw(withdrawalAmount), Throws.ArgumentException);
    }

    [Test]
    public void Withdraw_WhenAmountIsGreaterThanBalance_ShouldThrowInvalidOperationException()
    {
        // Arrange
        decimal withdrawalAmount = 150;
        
        // Act & Assert
        Assert.That(() => SystemUnderTest.Withdraw(withdrawalAmount), Throws.InvalidOperationException);
    }

    [Test]
    public void Withdraw_ShouldLeaveNonNegativeBalance()
    {
        SystemUnderTest.Withdraw(100);
        SystemUnderTest.AssertBalanceIsNonNegative();
    }

    [Test]
    public void Deposit_ShouldNotExceedUpperLimit()
    {
        SystemUnderTest.Deposit(900);
        SystemUnderTest.AssertBalanceIsWithinUpperLimit();
    }
}

using NunitLab.Api;

namespace NunitLab.UnitTests;

public static class BankAccountTestExtensionMethods
{
    public static void AssertBalanceIsNonNegative(
        this BankAccount bankAccount)
    {
        Assert.That(bankAccount.Balance, Is.GreaterThanOrEqualTo(0));
    }

    public static void AssertBalanceIsWithinUpperLimit(
        this BankAccount bankAccount,
        decimal upperLimit = 1000m)
    {
        Assert.That(bankAccount.Balance, Is.LessThanOrEqualTo(upperLimit));
    }
}


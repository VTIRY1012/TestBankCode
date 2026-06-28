namespace BankCode.Tests;

// User Story 2: 校驗和驗證 - (d1 + 2*d2 + ... + 9*d9) mod 11 == 0
public class CheckSumTests
{
    private readonly BankCodeService _service = new();

    [Theory]
    [InlineData("345882865")]
    [InlineData("457508000")]
    [InlineData("123456789")]
    [InlineData("000000000")]
    public void Should_Return_True_For_Valid_CheckSum(string account)
    {
        string[] lines = AccountRenderer.Render(account);
        Assert.True(_service.ParseNumber(lines).isValid);
    }

    [Theory]
    [InlineData("111111111")]
    [InlineData("664371495")]
    [InlineData("123456780")]
    public void Should_Return_False_For_Invalid_CheckSum(string account)
    {
        string[] lines = AccountRenderer.Render(account);
        Assert.False(_service.ParseNumber(lines).isValid);
    }
}

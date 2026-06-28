namespace BankCode.Tests;

// User Story 3: 錯誤處理 - 有效帳號 / ERR (校驗和錯誤) / ILL (無法識別)
public class ValidateAccountTests
{
    private readonly BankCodeService _service = new();

    [Fact]
    public void Should_Return_Account_When_Valid()
    {
        var lines = AccountRenderer.Render("457508000");
        Assert.Equal("457508000", _service.ParseNumber(lines).number);
    }

    [Fact]
    public void Should_Return_ERR_When_CheckSum_Invalid()
    {
        var lines = AccountRenderer.Render("664371495");
        Assert.Equal("ERR", _service.ParseNumber(lines).number);
    }

    [Fact]
    public void Should_Return_ILL_When_Contains_Unrecognized_Digit()
    {
        var lines = AccountRenderer.Render("86110??36");
        Assert.Equal("ILL", _service.ParseNumber(lines).number);
    }

    [Fact]
    public void Should_Prefer_ILL_Over_ERR_When_Both_Apply()
    {
        // 同時有無法識別 (?) 與校驗和問題時，應優先回報 ILL
        var lines = AccountRenderer.Render("1234?6789");
        Assert.Equal("ILL", _service.ParseNumber(lines).number);
    }
}

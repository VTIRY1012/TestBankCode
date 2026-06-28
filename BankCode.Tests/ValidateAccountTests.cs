using BankCode;

namespace BankCode.Tests;

// User Story 3: 錯誤處理 - 有效帳號 / ERR (校驗和錯誤) / ILL (無法識別)
public class ValidateAccountTests
{
    private readonly BankCodeService _service = new();

    [Fact]
    public void Should_Return_Account_When_Valid()
    {
        Assert.Equal("457508000", _service.ValidateAccount("457508000"));
    }

    [Fact]
    public void Should_Return_ERR_When_CheckSum_Invalid()
    {
        Assert.Equal("ERR", _service.ValidateAccount("664371495"));
    }

    [Fact]
    public void Should_Return_ILL_When_Contains_Unrecognized_Digit()
    {
        Assert.Equal("ILL", _service.ValidateAccount("86110??36"));
    }

    [Fact]
    public void Should_Prefer_ILL_Over_ERR_When_Both_Apply()
    {
        // 同時有無法識別 (?) 與校驗和問題時，應優先回報 ILL
        Assert.Equal("ILL", _service.ValidateAccount("1234?6789"));
    }
}

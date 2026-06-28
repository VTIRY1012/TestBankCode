using BankCode;

namespace BankCode.Tests;

// User Story 1: 數字識別 - 從 3x3 字元圖案解析出 9 位數帳號
public class ParseAccountTests
{
    private readonly BankCodeService _service = new();

    [Fact]
    public void Should_Parse_123456789()
    {
        string[] lines =
        {
            "    _  _     _  _  _  _  _ ",
            "  | _| _||_||_ |_   ||_||_|",
            "  ||_  _|  | _||_|  ||_| _|"
        };

        Assert.Equal("123456789", _service.ParseNumber(lines).number);
    }

    [Fact]
    public void Should_Parse_000000000()
    {
        string[] lines =
        {
            " _  _  _  _  _  _  _  _  _ ",
            "| || || || || || || || || |",
            "|_||_||_||_||_||_||_||_||_|"
        };

        Assert.Equal("000000000", _service.ParseNumber(lines).number);
    }

    [Theory]
    [InlineData("123456789")]
    [InlineData("000000000")]
    [InlineData("490067715")]
    [InlineData("345882865")]
    [InlineData("987654321")]
    public void Should_RoundTrip_Rendered_Account(string account)
    {
        string[] lines = AccountRenderer.Render(account);
        Assert.Equal(account, _service.ParseNumber(lines).number);
    }

    [Fact]
    public void Should_Return_Nine_Digits()
    {
        string[] lines = AccountRenderer.Render("000000000");
        Assert.Equal(9, _service.ParseNumber(lines).number.Length);
    }

    [Fact]
    public void Should_Use_QuestionMark_For_Unrecognized_Digit()
    {
        // 最後一個數字的圖案被破壞，無法對應任何數字
        string[] lines =
        {
            " _  _  _  _  _  _  _  _  _ ",
            "| || || || || || || || || |",
            "|_||_||_||_||_||_||_||_| _ "
        };

        Assert.Equal("00000000?", _service.ParseNumber(lines).number);
    }
}

namespace BankCode.Tests;

// User Story 1: 數字識別 - 從 3x3 字元圖案解析出 9 位數帳號
public class ParseAccountTests
{
    private readonly BankCodeService _service = new();

    // 反查表: 數字 -> 3x3 圖案 (由產品端字典反轉而來)
    private static readonly Dictionary<char, string> Glyphs =
        BankCodeDictionary.DigitPatterns.ToDictionary(kv => kv.Value, kv => kv.Key);

    // 把 9 位數帳號渲染成 3 行的 3x3 圖案，供 ParseAccount 解析回來 (round-trip)
    private static string[] Render(string account)
    {
        var rows = new string[3];
        for (int row = 0; row < 3; row++)
        {
            rows[row] = string.Concat(account.Select(c => Glyphs[c].Substring(row * 3, 3)));
        }
        return rows;
    }

    [Fact]
    public void Should_Parse_123456789()
    {
        string[] lines =
        {
            "    _  _     _  _  _  _  _ ",
            "  | _| _||_||_ |_   ||_||_|",
            "  ||_  _|  | _||_|  ||_| _|"
        };

        Assert.Equal("123456789", _service.ParseNumber(lines));
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

        Assert.Equal("000000000", _service.ParseNumber(lines));
    }

    [Theory]
    [InlineData("123456789")]
    [InlineData("000000000")]
    [InlineData("490067715")]
    [InlineData("345882865")]
    [InlineData("987654321")]
    public void Should_RoundTrip_Rendered_Account(string account)
    {
        string[] lines = Render(account);
        Assert.Equal(account, _service.ParseNumber(lines));
    }

    [Fact]
    public void Should_Return_Nine_Digits()
    {
        string[] lines = Render("000000000");
        Assert.Equal(9, _service.ParseNumber(lines).Length);
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

        Assert.Equal("00000000?", _service.ParseNumber(lines));
    }
}

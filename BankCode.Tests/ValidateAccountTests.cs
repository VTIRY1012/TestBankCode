namespace BankCode.Tests;

// Use case 3: 含無法辨識 (?) 的情況 (官方測試資料)
public class ValidateAccountTests
{
    private readonly BankCodeService _service = new();

    [Fact]
    public void Should_Parse_Valid_000000051()
    {
        string[] lines =
        {
            " _  _  _  _  _  _  _  _    ",
            "| || || || || || || ||_   |",
            "|_||_||_||_||_||_||_| _|  |"
        };

        var data = _service.ParseNumber(lines);

        Assert.Equal("000000051", data.number);
        Assert.True(data.isValid); // 校驗和有效
    }

    [Fact]
    public void Should_Parse_Illegible_49006771()
    {
        string[] lines =
        {
            "    _  _  _  _  _  _     _ ",
            "|_||_|| || ||_   |  |  | _ ",
            "  | _||_||_||_|  |  |  | _|"
        };

        var data = _service.ParseNumber(lines);

        Assert.Equal("49006771?", data.number);
        Assert.Equal("ILL", data.code);
        Assert.False(data.isValid);
    }

    [Fact]
    public void Should_Parse_Illegible_1234_678()
    {
        string[] lines =
        {
            "    _  _     _  _  _  _  _ ",
            "  | _| _||_| _ |_   ||_||_|",
            "  ||_  _|  | _||_|  ||_| _ "
        };

        var data = _service.ParseNumber(lines);

        Assert.Equal("1234?678?", data.number);
        Assert.Equal("ILL", data.code);
        Assert.False(data.isValid);
    }
}

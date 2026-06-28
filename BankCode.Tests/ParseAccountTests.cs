namespace BankCode.Tests;

// Use case 1: 基本解析 (官方測試資料)
public class ParseAccountTests
{
    private readonly BankCodeService _service = new();

    public static IEnumerable<object[]> UseCase1 => new List<object[]>
    {
        new object[]
        {
            new[]
            {
                " _  _  _  _  _  _  _  _  _ ",
                "| || || || || || || || || |",
                "|_||_||_||_||_||_||_||_||_|"
            },
            "000000000"
        },
        new object[]
        {
            new[]
            {
                new string(' ', 27),
                "  |  |  |  |  |  |  |  |  |",
                "  |  |  |  |  |  |  |  |  |"
            },
            "111111111"
        },
        new object[]
        {
            new[]
            {
                " _  _  _  _  _  _  _  _  _ ",
                " _| _| _| _| _| _| _| _| _|",
                "|_ |_ |_ |_ |_ |_ |_ |_ |_ "
            },
            "222222222"
        },
        new object[]
        {
            new[]
            {
                " _  _  _  _  _  _  _  _  _ ",
                " _| _| _| _| _| _| _| _| _|",
                " _| _| _| _| _| _| _| _| _|"
            },
            "333333333"
        },
        new object[]
        {
            new[]
            {
                new string(' ', 27),
                "|_||_||_||_||_||_||_||_||_|",
                "  |  |  |  |  |  |  |  |  |"
            },
            "444444444"
        },
        new object[]
        {
            new[]
            {
                " _  _  _  _  _  _  _  _  _ ",
                "|_ |_ |_ |_ |_ |_ |_ |_ |_ ",
                " _| _| _| _| _| _| _| _| _|"
            },
            "555555555"
        },
        new object[]
        {
            new[]
            {
                " _  _  _  _  _  _  _  _  _ ",
                "|_ |_ |_ |_ |_ |_ |_ |_ |_ ",
                "|_||_||_||_||_||_||_||_||_|"
            },
            "666666666"
        },
        new object[]
        {
            new[]
            {
                " _  _  _  _  _  _  _  _  _ ",
                "  |  |  |  |  |  |  |  |  |",
                "  |  |  |  |  |  |  |  |  |"
            },
            "777777777"
        },
        new object[]
        {
            new[]
            {
                " _  _  _  _  _  _  _  _  _ ",
                "|_||_||_||_||_||_||_||_||_|",
                "|_||_||_||_||_||_||_||_||_|"
            },
            "888888888"
        },
        new object[]
        {
            new[]
            {
                " _  _  _  _  _  _  _  _  _ ",
                "|_||_||_||_||_||_||_||_||_|",
                " _| _| _| _| _| _| _| _| _|"
            },
            "999999999"
        },
        new object[]
        {
            new[]
            {
                "    _  _     _  _  _  _  _ ",
                "  | _| _||_||_ |_   ||_||_|",
                "  ||_  _|  | _||_|  ||_| _|"
            },
            "123456789"
        }
    };

    [Theory]
    [MemberData(nameof(UseCase1))]
    public void Should_Parse_Account_From_Lines(string[] lines, string expected)
    {
        Assert.Equal(expected, _service.ParseNumber(lines).number);
    }
}

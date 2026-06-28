using BankCode;

namespace BankCode.Tests;

// Use case 4: 智能修正 (codingdojo.org Bank OCR User Story 4 官方測試資料)
public class SmartFixTests
{
    private readonly BankCodeService _service = new();

    [Fact]
    public void Should_Fix_711111111()
    {
        string[] lines =
        {
            "                           ",
            "  |  |  |  |  |  |  |  |  |",
            "  |  |  |  |  |  |  |  |  |"
        };

        var result = _service.ParseNumber(lines);

        Assert.Equal("711111111", result.number);
        Assert.True(result.isValid);
    }

    [Fact]
    public void Should_Fix_777777177()
    {
        string[] lines =
        {
            " _  _  _  _  _  _  _  _  _ ",
            "  |  |  |  |  |  |  |  |  |",
            "  |  |  |  |  |  |  |  |  |"
        };

        var result = _service.ParseNumber(lines);

        Assert.Equal("777777177", result.number);
        Assert.True(result.isValid);
    }

    [Fact]
    public void Should_Fix_200800000()
    {
        string[] lines =
        {
            " _  _  _  _  _  _  _  _  _ ",
            " _|| || || || || || || || |",
            "|_ |_||_||_||_||_||_||_||_|"
        };

        var result = _service.ParseNumber(lines);

        Assert.Equal("200800000", result.number);
        Assert.True(result.isValid);
    }

    [Fact]
    public void Should_Fix_333393333()
    {
        string[] lines =
        {
            " _  _  _  _  _  _  _  _  _ ",
            " _| _| _| _| _| _| _| _| _|",
            " _| _| _| _| _| _| _| _| _|"
        };

        var result = _service.ParseNumber(lines);

        Assert.Equal("333393333", result.number);
        Assert.True(result.isValid);
    }

    [Fact]
    public void Should_Report_Ambiguous_888888888()
    {
        string[] lines =
        {
            " _  _  _  _  _  _  _  _  _ ",
            "|_||_||_||_||_||_||_||_||_|",
            "|_||_||_||_||_||_||_||_||_|"
        };

        var result = _service.ParseNumber(lines);

        Assert.True(result.isAMB);
        Assert.Equal(new[] { "888886888", "888888880", "888888988" }, result.AMBList);
    }

    [Fact]
    public void Should_Report_Ambiguous_555555555()
    {
        string[] lines =
        {
            " _  _  _  _  _  _  _  _  _ ",
            "|_ |_ |_ |_ |_ |_ |_ |_ |_ ",
            " _| _| _| _| _| _| _| _| _|"
        };

        var result = _service.ParseNumber(lines);

        Assert.True(result.isAMB);
        Assert.Equal(new[] { "555655555", "559555555" }, result.AMBList);
    }

    [Fact]
    public void Should_Report_Ambiguous_666666666()
    {
        string[] lines =
        {
            " _  _  _  _  _  _  _  _  _ ",
            "|_ |_ |_ |_ |_ |_ |_ |_ |_ ",
            "|_||_||_||_||_||_||_||_||_|"
        };

        var result = _service.ParseNumber(lines);

        Assert.True(result.isAMB);
        Assert.Equal(new[] { "666566666", "686666666" }, result.AMBList);
    }

    [Fact]
    public void Should_Report_Ambiguous_999999999()
    {
        string[] lines =
        {
            " _  _  _  _  _  _  _  _  _ ",
            "|_||_||_||_||_||_||_||_||_|",
            " _| _| _| _| _| _| _| _| _|"
        };

        var result = _service.ParseNumber(lines);

        Assert.True(result.isAMB);
        Assert.Equal(new[] { "899999999", "993999999", "999959999" }, result.AMBList);
    }

    [Fact]
    public void Should_Report_Ambiguous_490067715()
    {
        string[] lines =
        {
            "    _  _  _  _  _  _     _ ",
            "|_||_|| || ||_   |  |  ||_ ",
            "  | _||_||_||_|  |  |  | _|"
        };

        var result = _service.ParseNumber(lines);

        Assert.True(result.isAMB);
        Assert.Equal(new[] { "490067115", "490067719", "490867715" }, result.AMBList);
    }

    [Fact]
    public void Should_Keep_Valid_123456789()
    {
        string[] lines =
        {
            "    _  _     _  _  _  _  _ ",
            "  | _| _||_||_ |_   ||_||_|",
            "  ||_  _|  | _||_|  ||_| _|"
        };

        var result = _service.ParseNumber(lines);

        Assert.Equal("123456789", result.number);
        Assert.True(result.isValid);
    }

    [Fact]
    public void Should_Fix_000000051()
    {
        string[] lines =
        {
            " _     _  _  _  _  _  _    ",
            "| || || || || || || ||_   |",
            "|_||_||_||_||_||_||_| _|  |"
        };

        var result = _service.ParseNumber(lines);

        Assert.Equal("000000051", result.number);
        Assert.True(result.isValid);
    }

    [Fact]
    public void Should_Fix_490867715()
    {
        string[] lines =
        {
            "    _  _  _  _  _  _     _ ",
            "|_||_|| ||_||_   |  |  | _ ",
            "  | _||_||_||_|  |  |  | _|"
        };

        var result = _service.ParseNumber(lines);

        Assert.Equal("490867715", result.number);
        Assert.True(result.isValid);
    }
}

using ManagerHelper.Extensions;

namespace ManagerHelperTests.Extensions;

public class StringExtensionsTests
{
    [Fact]
    public void ParseQuarterString_WhenInputIsQ12023_IsParsed()
    {
        var quarterString = "Q1 2023";

        Assert.True(quarterString.TryParseQuarterString(out int quarter, out int year));
        Assert.True(quarter == 1);
        Assert.True(year == 2023);
    }

    [Fact]
    public void ParseQuarterString_WhenInputIs2023Q1_IsParsed()
    {
        var quarterString = "2023 Q1";

        Assert.True(quarterString.TryParseQuarterString(out int quarter, out int year));
        Assert.True(quarter == 1);
        Assert.True(year == 2023);
    }

    [Fact]
    public void ParseQuarterString_WhenInputIsQ1Point2023_IsParsed()
    {
        var quarterString = "Q1.2023";

        Assert.True(quarterString.TryParseQuarterString(out int quarter, out int year));
        Assert.True(quarter == 1);
        Assert.True(year == 2023);
    }

    [Fact]
    public void ParseQuarterString_WhenInputIsQ41979_IsParsed()
    {
        var quarterString = "Q4.1979";

        Assert.True(quarterString.TryParseQuarterString(out int quarter, out int year));
        Assert.True(quarter == 4);
        Assert.True(year == 1979);
    }
}
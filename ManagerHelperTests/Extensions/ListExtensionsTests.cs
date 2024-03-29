using ManagerHelper.Extensions;

namespace ManagerHelperTests.Extensions;

public class ListExtensionsTests
{
    [Fact]
    public void IsNullOrEmpty_WhenNull_IsTrue()
    {
#pragma warning disable CS8600
        List<string> list = null;
        Assert.True(list.IsNullOrEmpty());
#pragma warning restore CS8600
    }

    [Fact]
    public void IsNullOrEmpty_WhenEmpty_IsTrue()
    {
        List<string> list = new List<string>();
        Assert.True(list.IsNullOrEmpty());
    }

    [Fact]
    public void IsNullOrEmpty_WhenOne_IsFalse()
    {
        List<string> list = new List<string>() { "Hello" };
        Assert.False(list.IsNullOrEmpty());
    }
}
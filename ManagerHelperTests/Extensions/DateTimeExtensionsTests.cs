using ManagerHelper.Extensions;

namespace ManagerHelperTests.Extensions;

public class DateTimeExtensionsTests
{
    [Fact]
    public void FindDateOfSprintDay_WhenInputIsFridayDay10_IsSecondThursday()
    {
        var date = DateTime.Parse("04-07-2023 10:00:00");
        var day10Date = date.FindDateOfSprintDay(10);

        Assert.True(day10Date.DayOfWeek == DayOfWeek.Thursday);
        Assert.True(day10Date.Day == 20);
    }

    [Fact]
    public void FindDateOfSprintDay_WhenInputIsMondayDay10_IsSecondThursday()
    {
        var date = DateTime.Parse("04-10-2023 10:00:00");
        var day10Date = date.FindDateOfSprintDay(10);

        Assert.True(day10Date.DayOfWeek == DayOfWeek.Friday);
        Assert.True(day10Date.Day == 21);
    }
}
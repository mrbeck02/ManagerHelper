namespace ManagerHelper.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FindNextWorkingDay(this DateTime date)
        {
            date = date.AddDays(1);

            while (true)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    date = date.AddDays(1);
                }
                else
                    return date;
            }
        }

        public static DateTime FindDateOfSprintDay(this DateTime sprintStart, int dayOfSprint)
        {
            var dateTime = sprintStart.Date;

            for (int day = 1; day < dayOfSprint; day++)
            {
                dateTime = FindNextWorkingDay(dateTime);
            }

            return dateTime;
        }
    }
}

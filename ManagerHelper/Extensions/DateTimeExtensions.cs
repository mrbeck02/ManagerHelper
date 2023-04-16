namespace ManagerHelper.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FindNextWorkingDay(this DateTime date)
        {
            // Add one day to the input date until we find a working day
            date = date.AddDays(1);

            while (true)
            {
                // Check if the current date is a weekend
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    // Add one day to the date
                    date = date.AddDays(1);
                }
                else
                {
                    // This is a working day, so return the date
                    return date;
                }
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

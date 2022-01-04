
public static class MyExtension
{
    public static DateTime GetWeekFirstDate(this DateTime BaseDate, DayOfWeek startOfWeek = DayOfWeek.Sunday)
    {
        int diff = (7 + (BaseDate.DayOfWeek - startOfWeek)) % 7;
        return BaseDate.AddDays(-1 * diff).Date;
    }
}

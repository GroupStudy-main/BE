namespace Utilities.ServiceExtensions.Scheduler.Lib;

public static class CronScheduleExpression
{
    /// <summary>
    ///     Day 1 of every months
    ///     Start at 00:00:00
    /// </summary>
    public const string StartMonthly = "0 0 0 1 1/1 ? *";

    /// <summary>
    ///     Last day of every months
    ///     Start at 00:00:00
    /// </summary>
    public const string EndMonthly = "0 0 0 L 1/1 ? *";

    public const string Weekly = "0 0 0 ? * WED *";
    //public const string Weekly = "0 0 0 ? * Mon*";

    /// <summary>
    ///     Every Day
    ///     Start at 00:00:00
    /// </summary>
    public const string Daily = "0 0 0 * * ?";

    public static string ThirtySecond = "0/30 0/1 * 1/1 * ? *";
}
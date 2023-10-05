namespace MessageBoards.Helper;

public static class FormatTimeStamp
{
    
    public static string FormatTimestamp(DateTime timestamp)
    {
        TimeSpan timeAgo = DateTime.Now - timestamp;

        if (timeAgo.TotalMinutes < 1)
        {
            return "just now";
        }
        else if (timeAgo.TotalMinutes < 60)
        {
            int minutes = (int)timeAgo.TotalMinutes;
            return $"{minutes} minute{(minutes > 1 ? "s" : "")} ago";
        }
        else if (timeAgo.TotalHours < 24)
        {
            int hours = (int)timeAgo.TotalHours;
            return $"{hours} hour{(hours > 1 ? "s" : "")} ago";
        }
        else
        {
            int days = (int)timeAgo.TotalDays;
            return $"{days} day{(days > 1 ? "s" : "")} ago";
        }
    }

}
namespace Audivia.Application.Utils.Helper
{
    public class TimeUtils
    {
        public static string GetTimeElapsed(DateTime createdAt)
        {
            DateTime now = DateTime.UtcNow;
            TimeSpan elapsed = now - createdAt;

            if (elapsed.TotalDays >= 1)
            {
                int days = (int)elapsed.TotalDays;
                return $"{days} days ago";
            }
            else if (elapsed.TotalHours >= 1)
            {
                int hours = (int)elapsed.TotalHours;
                return $"{hours} hours ago";
            }
            else if (elapsed.TotalMinutes >= 1)
            {
                int minutes = (int)elapsed.TotalMinutes;
                return $"{minutes} minutes ago";
            }
            else
            {
                int seconds = (int)elapsed.TotalSeconds;
                return $"{seconds} seconds ago";
            }
        }
    }
}

namespace Task_3 {
    public class DateService {
        public static string GetDay(string date) {
            return DateTime.Parse(date).DayOfWeek.ToString();
        }

        public static long GetDaysSpan(int day, int month, int year) {
            return Convert.ToInt64((DateTime.Now - new DateTime(year, month, day)).TotalDays);
        }
    }
}
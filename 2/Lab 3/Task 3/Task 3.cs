namespace Task_3 {
    public class DateService {
        public static string GetDay(string date) {
            return DateTime.ParseExact(date, "dd.MM.yyyy", null).DayOfWeek.ToString();
        }

        public static long GetDaysSpan(int day, int month, int year) {
            return (DateTime.Now - new DateTime(year, month, day)).Days;
        }
    }
}
// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
public class Task2 {
    public static string Location(double x, double y) {
        if (x > 0) {
            return "Нет";
        }
        if (x * x + y * y > 9 && x * x + y * y < 64) {
            return "Да";
        }
        if (x * x + y * y == 9 || x * x + y * y == 64) {
            return "На границе";
        }
        return "Нет";
    }
}
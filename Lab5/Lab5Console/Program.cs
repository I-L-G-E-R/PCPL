using System;
using LevenshteinLibrary; // Подключаем нашу библиотеку

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Лабораторная №5 (Адаптация для WSL) ---");
        
        Console.Write("Введите строку 1: ");
        string s1 = Console.ReadLine() ?? "";

        Console.Write("Введите строку 2: ");
        string s2 = Console.ReadLine() ?? "";

        Console.WriteLine("\nРезультаты:");
        
        int distLev = StringMetrics.LevenshteinDistance(s1, s2);
        Console.WriteLine($"Расстояние Левенштейна: {distLev}");

        int distDam = StringMetrics.DamerauLevenshteinDistance(s1, s2);
        Console.WriteLine($"Расстояние Дамерау-Левенштейна: {distDam}");
    }
}
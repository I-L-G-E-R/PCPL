using System;

namespace DelegatesPart
{
    // Объявляем делегат
    public delegate int MathOperation(int x, int y);

    class Program
    {
        // Метод, соответствующий делегату
        static int Add(int x, int y) => x + y;
        static int Multiply(int x, int y) => x * y;

        // Метод, принимающий делегат как параметр
        static void Calculate(int a, int b, MathOperation op)
        {
            Console.WriteLine($"Результат операции: {op(a, b)}");
        }

        // То же самое, но с обобщенным делегатом Func
        static void CalculateFunc(int a, int b, Func<int, int, int> op)
        {
            Console.WriteLine($"Результат Func: {op(a, b)}");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("--- Делегаты ---");
            
            // 1. Использование именованного метода
            Calculate(10, 20, Add);

            // 2. Использование лямбда-выражения
            Calculate(5, 5, (x, y) => x - y);

            Console.WriteLine("\n--- Обобщенные делегаты (Func) ---");
            
            // 3. Func с лямбдой
            CalculateFunc(6, 7, (x, y) => x * y);
            
            // 4. Action (ничего не возвращает)
            Action<string> print = msg => Console.WriteLine($"[LOG]: {msg}");
            print("Тест Action завершен.");
        }
    }
}
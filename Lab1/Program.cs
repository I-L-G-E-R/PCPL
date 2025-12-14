using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // ВАРИАНТ 1: Если числа передали сразу при запуске (dotnet run -- 1 -5 4)
        if (args.Length == 3) 
        {
            Console.WriteLine("--- Режим командной строки ---");
            double a, b, c;
            // Пробуем превратить текст в числа
            if (double.TryParse(args[0], out a) && 
                double.TryParse(args[1], out b) && 
                double.TryParse(args[2], out c))
            {
                Solve(a, b, c);
            }
            else
            {
                Console.WriteLine("Ошибка: Вы ввели не числа!");
            }
        }
        // ВАРИАНТ 2: Если запустили просто так (dotnet run)
        else 
        {
            Console.WriteLine("--- Интерактивный режим ---");
            Console.WriteLine("Программа ждет вашего ввода.");
            
            Console.Write("Введите A: ");
            double a = ReadNumber();

            Console.Write("Введите B: ");
            double b = ReadNumber();

            Console.Write("Введите C: ");
            double c = ReadNumber();

            Solve(a, b, c);
        }
    }

    // Метод, который не отстанет, пока вы не введете число
    static double ReadNumber()
    {
        while(true)
        {
            string txt = Console.ReadLine();
            if (double.TryParse(txt, out double number))
            {
                return number;
            }
            Console.Write("Это не число! Попробуйте еще раз: ");
        }
    }

    // Математика решения
    static void Solve(double a, double b, double c)
    {
        Console.WriteLine($"\nРЕШЕНИЕ ДЛЯ: {a}x^4 + ({b})x^2 + ({c}) = 0");

        if (a == 0)
        {
            Console.WriteLine("Ошибка: A не может быть равно 0 для биквадратного уравнения.");
            return;
        }

        double D = b * b - 4 * a * c;
        Console.WriteLine($"Дискриминант D = {D}");

        if (D < 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Корней нет.");
            Console.ResetColor();
        }
        else
        {
            List<double> roots = new List<double>();
            
            // Находим y1 и y2
            double y1 = (-b + Math.Sqrt(D)) / (2 * a);
            double y2 = (-b - Math.Sqrt(D)) / (2 * a);

            // Превращаем y в x
            GetXfromY(y1, roots);
            GetXfromY(y2, roots);

            if (roots.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ответ (Корни X):");
                foreach (var x in roots) Console.WriteLine($"x = {x}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Действительных корней нет (так как y < 0).");
            }
            Console.ResetColor();
        }
    }

    static void GetXfromY(double y, List<double> roots)
    {
        if (y > 0)
        {
            roots.Add(Math.Sqrt(y));
            roots.Add(-Math.Sqrt(y));
        }
        else if (y == 0)
        {
            if (!roots.Contains(0)) roots.Add(0);
        }
    }
}
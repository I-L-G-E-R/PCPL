// Program.cs
using System;
using System.Collections.Generic;

// Пункт 7: Интерфейс IPrint
public interface IPrint
{
    void Print();
}

// Пункт 2: Абстрактный класс
public abstract class GeometricFigure
{
    // Виртуальный метод для вычисления площади
    public abstract double Area();
}

// Пункт 3: Класс Прямоугольник
public class Rectangle : GeometricFigure, IPrint
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public override double Area()
    {
        return Width * Height;
    }

    // Пункт 6: Переопределение ToString
    public override string ToString()
    {
        return $"Прямоугольник (Ширина: {Width}, Высота: {Height}), Площадь: {Area():F2}";
    }
    
    // Реализация метода из интерфейса IPrint
    public void Print()
    {
        Console.WriteLine(this.ToString());
    }
}

// Пункт 4: Класс Квадрат
public class Square : Rectangle
{
    // Вызов конструктора базового класса
    public Square(double side) : base(side, side) { }

    // Переопределяем ToString для более корректного вывода
    public override string ToString()
    {
        return $"Квадрат (Сторона: {Width}), Площадь: {Area():F2}";
    }
}

// Пункт 5: Класс Круг
public class Circle : GeometricFigure, IPrint
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public override double Area()
    {
        return Math.PI * Radius * Radius;
    }
    
    public override string ToString()
    {
        return $"Круг (Радиус: {Radius}), Площадь: {Area():F2}";
    }

    public void Print()
    {
        Console.WriteLine(this.ToString());
    }
}


class Program
{
    static void Main(string[] args)
    {
        var figures = new List<IPrint>
        {
            new Rectangle(10, 20),
            new Square(15),
            new Circle(10)
        };
        
        Console.WriteLine("Информация о геометрических фигурах:");
        foreach (var figure in figures)
        {
            figure.Print();
        }
    }
}
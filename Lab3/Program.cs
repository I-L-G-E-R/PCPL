using System;
using System.Collections;
using System.Collections.Generic;

#region Классы из Лаб. №2 (модифицированные)

// ИСПРАВЛЕНИЕ: Добавляем обычный интерфейс IComparable для работы ArrayList
public abstract class GeometricFigure : IComparable<GeometricFigure>, IComparable
{
    public abstract double Area();

    // Метод для List<GeometricFigure> (современный)
    public int CompareTo(GeometricFigure other)
    {
        if (other == null) return 1;
        return this.Area().CompareTo(other.Area());
    }

    // Метод для ArrayList (старый, принимает object)
    public int CompareTo(object obj)
    {
        if (obj == null) return 1;
        
        // Проверяем, является ли объект фигурой
        GeometricFigure otherFigure = obj as GeometricFigure;
        if (otherFigure != null)
        {
            return this.CompareTo(otherFigure); // Вызываем современный метод
        }
        else
        {
            throw new ArgumentException("Object is not a GeometricFigure");
        }
    }
}

public interface IPrint
{
    void Print();
}

public class Rectangle : GeometricFigure, IPrint
{
    public double Width { get; set; }
    public double Height { get; set; }
    public Rectangle(double w, double h) { Width = w; Height = h; }
    public override double Area() => Width * Height;
    public override string ToString() => $"Прямоугольник ({Width}x{Height}), S={Area():F2}";
    public void Print() => Console.WriteLine(this);
}

public class Square : Rectangle
{
    public Square(double side) : base(side, side) { }
    public override string ToString() => $"Квадрат ({Width}x{Width}), S={Area():F2}";
}

public class Circle : GeometricFigure, IPrint
{
    public double Radius { get; set; }
    public Circle(double r) { Radius = r; }
    public override double Area() => Math.PI * Radius * Radius;
    public override string ToString() => $"Круг (R={Radius}), S={Area():F2}";
    public void Print() => Console.WriteLine(this);
}

#endregion

#region Пункт 7: SimpleStack на основе односвязного списка

public class SimpleStack<T>
{
    private class Node
    {
        public T Data { get; }
        public Node Next { get; }
        public Node(T data, Node next) { Data = data; Next = next; }
    }

    // Инициализируем null, чтобы убрать предупреждение CS8618
    private Node _head = null;

    public void Push(T element)
    {
        _head = new Node(element, _head);
    }

    public T Pop()
    {
        if (_head == null)
        {
            throw new InvalidOperationException("Стек пуст.");
        }
        T data = _head.Data;
        _head = _head.Next;
        return data;
    }
    
    public bool IsEmpty => _head == null;
}

#endregion

class Program
{
    static void Main(string[] args)
    {
        var rect = new Rectangle(5, 10);   // S = 50
        var square = new Square(8);       // S = 64
        var circle = new Circle(5);       // S ~ 78.5

        // --- Пункт 4: Работа с необобщенной коллекцией ArrayList ---
        Console.WriteLine("--- Тест ArrayList (необобщенная коллекция) ---");
        var arrayList = new ArrayList { rect, circle, square };
        
        // Теперь это сработает, так как мы добавили CompareTo(object obj)
        arrayList.Sort(); 
        
        foreach (var item in arrayList)
        {
            ((IPrint)item).Print();
        }
        Console.WriteLine();

        // --- Пункт 5: Работа с обобщенной коллекцией List<T> ---
        Console.WriteLine("--- Тест List<GeometricFigure> (обобщенная коллекция) ---");
        var list = new List<GeometricFigure> { rect, circle, square };
        list.Sort();
        foreach (var figure in list)
        {
            ((IPrint)figure).Print();
        }
        Console.WriteLine();

        // --- Пункт 8: Пример работы SimpleStack ---
        Console.WriteLine("--- Тест SimpleStack ---");
        var stack = new SimpleStack<GeometricFigure>();
        stack.Push(rect);
        stack.Push(circle);
        stack.Push(square);

        Console.WriteLine("Извлекаем элементы из стека (LIFO):");
        while(!stack.IsEmpty)
        {
            var figure = stack.Pop();
            ((IPrint)figure).Print();
        }
        Console.WriteLine();
    }
}
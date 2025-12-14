using System;
using System.Reflection;

namespace ReflectionPart
{
    // Класс для исследования
    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        private string _secret = "12345";

        public User(string name, int age) { Name = name; Age = age; }
        public void SayHello() => Console.WriteLine($"Привет, я {Name}!");
        private void RevealSecret() => Console.WriteLine($"Секрет: {_secret}");
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Рефлексия ---");
            Type t = typeof(User);

            Console.WriteLine("Свойства класса:");
            foreach (var prop in t.GetProperties())
                Console.WriteLine($"- {prop.Name} ({prop.PropertyType.Name})");

            Console.WriteLine("\nМетоды класса:");
            foreach (var method in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                Console.WriteLine($"- {method.Name} (IsPrivate: {method.IsPrivate})");

            Console.WriteLine("\nДинамический вызов:");
            // Создаем объект на лету
            object? user = Activator.CreateInstance(t, new object[] { "Alex", 25 });
            
            // Вызываем публичный метод
            MethodInfo? sayHello = t.GetMethod("SayHello");
            sayHello?.Invoke(user, null);

            // Взламываем приватный метод
            MethodInfo? secret = t.GetMethod("RevealSecret", BindingFlags.NonPublic | BindingFlags.Instance);
            secret?.Invoke(user, null);
        }
    }
}
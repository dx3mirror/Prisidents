using System;
using System.Collections.Generic;

namespace Presidents
{
    // Абстрактный класс, представляющий президента страны
    public abstract class President
    {
        public abstract string GetPresidentName();
    }

    // Конкретный класс, представляющий президента России
    public class RussiaPresident : President
    {
        public override string GetPresidentName()
        {
            return "Владимир Путин";
        }
    }

    // Конкретный класс, представляющий президента США
    public class USPresident : President
    {
        public override string GetPresidentName()
        {
            return "Джо Байден";
        }
    }

    // Фабричный метод для создания объектов-президентов
    public static class PresidentFactory
    {
        // Словарь, содержащий соответствие между страной и классом президента этой страны
        private static Dictionary<string, Type> _presidentTypes = new Dictionary<string, Type>
        {
            { "Россия", typeof(RussiaPresident) },
            { "США", typeof(USPresident) }
        };

        public static President CreatePresident(string country)
        {
            if (_presidentTypes.TryGetValue(country, out var presidentType))
            {
                return (President)Activator.CreateInstance(presidentType);
            }
            else
            {
                throw new ArgumentException("Президент данной страны не найден");
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите название страны:");
                string country = Console.ReadLine();

                try
                {
                    President president = PresidentFactory.CreatePresident(country);
                    string presidentName = president.GetPresidentName();
                    Console.WriteLine($"Президент {country}: {presidentName}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}
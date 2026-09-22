using System;
using System.Collections.Generic;
using System.IO;

namespace task_7
{
    public interface IInventory
    {
        /// <summary>
        /// Метод добавления предмета в инвентарь
        /// </summary>
        /// <param name="item">Предмет</param>
        public void AddItem(string item);

        /// <summary>
        /// Метод удаления предмета из инвентаря
        /// </summary>
        /// <param name="item">Предмет</param>
        public void RemoveItem(string item);
    }

    /// <summary>
    /// Интерфейс ISaveToFile
    /// </summary>
    public interface ISaveToFile
    {
        // Метод сохранения в файл
        public void SaveToFile(Player player);
    }

    /// <summary>
    /// Интерфейс ILoadFile
    /// </summary>
    public interface ILoadFile
    {
        // Метод загрузки сохранения
        public void LoadFromFile(Player player);
    }

    public class Inventory : IInventory
    {
        public List<string> items = new List<string>();

        public void AddItem(string item)
        {
            items.Add(item);
            Console.WriteLine($"[UI] Предмет '{item}' добавлен в инвентарь. Всего предметов: {items.Count}");
        }

        public void RemoveItem(string item)
        {
            items.Remove(item);
            Console.WriteLine($"[UI] Предмет '{item}' удалён.");
        }
    }

    public class Player
    {
        // Поля (автосвойства)
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Score { get; private set; }

        // Инвентарь игрока
        public Inventory Inventory { get; private set; } = new Inventory();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Имя игрока</param>
        /// <param name="health">Здровье</param>
        /// <param name="score">Счёт</param>
        public Player(string name, int health = 100, int score = 0)
        {
            Name = name;
            Health = health;
            Score = score;
        }

        /// <summary>
        /// Метод получения урона
        /// </summary>
        /// <param name="value">Урон</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void TakeDemage(int value)
        {
            if (value < 0)
            {
                Health = 0;
            }
            else
            {
                Health -= value;
            }

            Console.WriteLine($"[UI] Здоровье игрока {Name}: {Health}");
        }

        /// <summary>
        /// Метод добавления очков
        /// </summary>
        /// <param name="value">Кол-во очков</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void AddScore(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException("Ошибка добавления очков");
            }
            else
            {
                Score += value;
                Console.WriteLine($"[UI] Очки: {Score}");
            }
        }

        /// <summary>
        /// Метод изменения имени
        /// </summary>
        /// <param name="name">Имя</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Ошибка изменения имени.");
            }
            else
            {
                Name = name;
                Console.WriteLine("Имя изменено");
            }
        }

        /// <summary>
        /// Метод изменения здоровья
        /// </summary>
        /// <param name="value">Значение здоровья</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SetHealth(int value)
        {
            if (value < 1)
            {
                throw new ArgumentNullException("Ошибка изменения здоровья.");
            }
            else
            {
                Health = value;
                Console.WriteLine("Здоровье изменено");
            }
        }

        // Внутренние методы для загрузки из файла
        public void RestoreName(string name) => Name = name;
        public void RestoreHealth(int health) => Health = health;
        public void RestoreScore(int score) => Score = score;
    }

    class SaveManger : ISaveToFile, ILoadFile
    {
        public void SaveToFile(Player player)
        {
            string data = $"{player.Name}|{player.Health}|{player.Score}|{string.Join(",", player.Inventory.items)}";
            File.WriteAllText("save.txt", data);
            Console.WriteLine("[UI] Игра сохранена!");
        }

        public void LoadFromFile(Player player)
        {
            if (!File.Exists("save.txt"))
            {
                Console.WriteLine("[UI] Файл сохранения не найден.");
                return;
            }

            string data = File.ReadAllText("save.txt");
            string[] parts = data.Split('|');

            player.RestoreName(parts[0]);
            player.RestoreHealth(int.Parse(parts[1]));
            player.RestoreScore(int.Parse(parts[2]));

            player.Inventory.items.Clear();
            if (parts[3].Length > 0)
            {
                player.Inventory.items.AddRange(parts[3].Split(','));
            }

            Console.WriteLine("[UI] Игра загружена!");
        }
    }

    /// <summary>
    /// Класс GameManager
    /// </summary>
    public class GameManager
    {
        // Автосвойство
        public Player Player { get; private set; }
        

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="player">Игрок</param>
        public GameManager(Player player)
        {
            Player = player;
        }

        /// <summary>
        /// Метод добавления предмета в инвентарь
        /// </summary>
        /// <param name="item">Предмет</param>
        public void AddItem(string item)
        {
            Player.Inventory.AddItem(item);
        }

        /// <summary>
        /// Метод удаления предмета из инвентаря
        /// </summary>
        /// <param name="item">Предмет</param>
        public void RemoveItem(string item)
        {
            Player.Inventory.RemoveItem(item);
        }

        /// <summary>
        /// Метод нанесения урона
        /// </summary>
        /// <param name="value">Значение урона</param>
        public void TakeDamage(int value)
        {
            Player.TakeDemage(value);
        }

        /// <summary>
        /// Метод добавления очков
        /// </summary>
        /// <param name="value">Очки</param>
        public void AddScore(int value)
        {
            Player.AddScore(value);
        }

        // Метод сохранения игры
        public void Save()
        {
            new SaveManger().SaveToFile(Player);
        }

        // Метод загрузки игры
        public void Load()
        {
            new SaveManger().LoadFromFile(Player);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("Герой");
            GameManager game = new GameManager(player);

            game.AddItem("Меч");
            game.AddItem("Щит");
            game.AddScore(50);
            game.TakeDamage(20);

            game.Save();

            game.TakeDamage(80);
            game.AddItem("Мусор");

            game.Load();

            Console.WriteLine($"\nИгрок: {player.Name}, HP: {player.Health}, Score: {player.Score}");
            Console.WriteLine($"Инвентарь: {string.Join(", ", player.Inventory.items)}");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace task_10
{
    /// <summary>
    /// Товар с валидацией. Свойства только для чтения.
    /// </summary>
    public class Product
    {
        // Автосвойства
        public string Name { get; }
        public decimal Price { get; }
        public int Quantity { get; }

        public Product(string name, decimal price, int quantity)
        {
            
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Название товара не может быть пустым.", nameof(name));
            }

            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Цена должна быть больше нуля.");
            }

            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Количество должно быть больше нуля.");
            }
                
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }

    /// <summary>
    /// Корзина покупок с защищённым внутренним состоянием.
    /// </summary>
    public class ShoppingCart
    {
        // Поля
        private List<Product> items = new List<Product>();
        public decimal TotalPrice => items.Sum(item => item.Price * item.Quantity);


        /// <summary>
        /// Метод добавления продукта
        /// </summary>
        /// <param name="product">Продукт</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddProduct(Product product)
        {

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            items.Add(product);
        }


        /// <summary>
        /// Метод удаления продукта
        /// </summary>
        /// <param name="product">Продукт</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void RemoveProduct(Product product)
        {

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            items.Remove(product);
        }

        // Метод очистки корзины
        public void Clear()
        {
            items.Clear();
        }

        // Метод печати чека
        public void PrintReceipt()
        {
            Console.WriteLine("=== ЧЕК ===");

            foreach (Product item in items)
            {
                Console.WriteLine($"{item.Name} x{item.Quantity} = {item.Price * item.Quantity}");
            }

            Console.WriteLine($"ИТОГО: {TotalPrice}");
        }

        // Метод получения списка продуктов
        public IReadOnlyList<Product> GetProducts()
        {
            return items.AsReadOnly();
        }
    }

    internal class Program
    {
        static void Main()
        {
            // Создаём корзину
            ShoppingCart cart = new ShoppingCart();

            // Добавляем товары
            cart.AddProduct(new Product("Хлеб", 50m, 2));
            cart.AddProduct(new Product("Молоко", 80m, 1));

            // Печатаем чек
            cart.PrintReceipt();

            // Удаляем первый товар и снова печатаем чек
            cart.RemoveProduct(cart.GetProducts()[0]);
            cart.PrintReceipt();

            // Очищаем корзину
            cart.Clear();
            cart.PrintReceipt();

            // Проверяем валидацию: товар с нулевой ценой должен бросить исключение
            try
            {
                cart.AddProduct(new Product("Брак", 0m, 1));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
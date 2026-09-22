using System;
using System.Collections.Generic;

namespace task_8
{

    /// <summary>
    /// Абстракнтный класс Discount
    /// </summary>
    public abstract class Discount
    {
        /// <summary>
        /// Метод расчёта скидки
        /// </summary>
        /// <param name="price">Цена</param>
        /// <param name="customerYears">Время использования магазина</param>
        /// <returns></returns>
        public abstract decimal CalculateDiscountAmount(decimal price, int customerYears);

        /// <summary>
        /// Метод применения скидки (здесь же проверка)
        /// </summary>
        /// <param name="price">Цена</param>
        /// <param name="customerYears">Время использования магазина</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public decimal Apply(decimal price, int customerYears)
        {
            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Цена не может быть отрицательной.");

            decimal discount = CalculateDiscountAmount(price, customerYears);
            decimal result = price - discount;

            return result < 0 ? 0 : result;
        }
    }

    /// <summary>
    /// Класс NewYearDiscount (Наследуется от Discount)
    /// </summary>
    public class NewYearDiscount : Discount
    {
        // Поля
        private const decimal Rate = 0.15m;

        public override decimal CalculateDiscountAmount(decimal price, int customerYears)
            => price * Rate;
    }

    /// <summary>
    /// Класс LoyaltyDiscount (Наследуется от Discount)
    /// </summary>
    public class LoyaltyDiscount : Discount
    {
        // Поля
        private const decimal RatePerYear = 0.02m;

        public override decimal CalculateDiscountAmount(decimal price, int customerYears)
            => price * (customerYears * RatePerYear);
    }

    /// <summary>
    /// Класс BlackFridayDiscount (Наследуется от Discount)
    /// </summary>
    public class BlackFridayDiscount : Discount
    {
        // Поля
        private const decimal Rate = 0.30m;

        public override decimal CalculateDiscountAmount(decimal price, int customerYears)
            => price * Rate;
    }

    /// <summary>
    /// Класс NoDiscount (Наследуется от Discount)
    /// </summary>
    public class NoDiscount : Discount
    {
        // Поля
        public override decimal CalculateDiscountAmount(decimal price, int customerYears)
            => 0m;
    }

    /// <summary>
    /// Класс BirthdayDiscount (Наследуется от Discount)
    /// </summary>
    public class BirthdayDiscount : Discount
    {
        private const decimal Rate = 0.20m;

        public override decimal CalculateDiscountAmount(decimal price, int customerYears)
            => price * Rate;
    }

    /// <summary>
    /// Класс DiscountCalculator
    /// </summary>
    public class DiscountCalculator
    {
       
        // Поля
        private Dictionary<string, Discount> discounts = new Dictionary<string, Discount>();

        /// <summary>
        /// Конструктор
        /// </summary>
        public DiscountCalculator()
        {
            discounts.Add("NewYear", new NewYearDiscount());
            discounts.Add("Loyalty", new LoyaltyDiscount());
            discounts.Add("BlackFriday", new BlackFridayDiscount());
        }

        /// <summary>
        /// Метод расчёта
        /// </summary>
        /// <param name="price">Цена</param>
        /// <param name="discountType">Тип скидки</param>
        /// <param name="customerYears">Время использования магазина</param>
        /// <returns></returns>
        public decimal Calculate(decimal price, string discountType, int customerYears)
        {
            Discount discount;

            if (discounts.ContainsKey(discountType))
            {
                discount = discounts[discountType];
            }
            else
            {
                discount = new NoDiscount();
            }

            return discount.Apply(price, customerYears);

        }

        /// <summary>
        /// Метод добавления типа скидки
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="discount">Скидка</param>
        public void RegisterDiscount(string name, Discount discount)
        {
            discounts.Add(name, discount);
        }
    }

    internal class Program
    {
        static void Main()
        {
            DiscountCalculator calculator = new DiscountCalculator();

            Console.WriteLine(calculator.Calculate(1000m, "NewYear", 0));
            Console.WriteLine(calculator.Calculate(1000m, "Loyalty", 5));
            Console.WriteLine(calculator.Calculate(1000m, "BlackFriday", 0));
            Console.WriteLine(calculator.Calculate(1000m, "Unknown", 0));
            Console.WriteLine(calculator.Calculate(50m, "BlackFriday", 0));

            calculator.RegisterDiscount("Birthday", new BirthdayDiscount());
            Console.WriteLine(calculator.Calculate(1000m, "Birthday", 0));
        }
    }
}
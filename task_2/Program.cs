namespace task_2
{
    /// <summary>
    /// Интерфейс IShippingCostCalculator
    /// </summary>
    public interface IShippingCostCalculator
    {
        // Метод расчёта стоимости
        public decimal CalculateCost(decimal weight);
    }


    /// <summary>
    /// Класс ShippingCostCalculatorStandard (расчёт стандартной доставки)
    /// </summary>
    public class ShippingCostCalculatorStandard : IShippingCostCalculator
    {
        // Поля
        private const decimal CostPerKg = 5.0m;

        // Метод расчёта
        public decimal CalculateCost(decimal weight)
        {
           return weight * CostPerKg;
        }
    }

    /// <summary>
    /// Класс ShippingCostCalculatorExpress (расчёт экспресс доставки)
    /// </summary>
    public class ShippingCostCalculatorExpress : IShippingCostCalculator
    {
        // Поля
        private const decimal CostPerKg = 10.0m;
        private const decimal BaseFee = 50.0m;

        public decimal CalculateCost(decimal weight)
        {
            return weight * CostPerKg + BaseFee;
        }
    }

   
    internal class Program
    {
        static void Main(string[] args)
        {
            IShippingCostCalculator[] calculators =
            {
                new ShippingCostCalculatorStandard(),
                new ShippingCostCalculatorExpress()
            };

            decimal weight = 10m;

            foreach (var calculator in calculators)
            {
                decimal cost = calculator.CalculateCost(weight);
                Console.WriteLine($"{calculator.GetType().Name}: {cost} руб.");
            }
        }
    }
}

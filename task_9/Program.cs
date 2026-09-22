namespace task_9
{
    /// <summary>
    /// Интерфейс IAirConditioner
    /// </summary>
    public interface IAirConditioner
    {
        // Автосвойтсво
        public int Temperature { get; set; }

        // Метод включения кондиционера
        public void TurnOnAC();
    }

    /// <summary>
    /// Интерфейс INavigator
    /// </summary>
    public interface INavigator
    {
        // Автосвойтсво
        public string Destination { get; set; }

        // Метод навигации
        public void Navigate();
    }

    /// <summary>
    /// Класс AirConditioner (наследуется от IAirConditioner)
    /// </summary>
    public class AirConditioner : IAirConditioner
    {
        // Автосвойство
        public int Temperature { get; set; } = 22;

        // Метод включения кондиционера
        public void TurnOnAC() => Console.WriteLine($"Кондиционер включён на {Temperature}°C");
    }

    /// <summary>
    /// Класс GpsNavigator (наследуется от INavigator)
    /// </summary>
    public class GpsNavigator : INavigator
    {
        // Автосвойство
        public string Destination { get; set; }
        
        // Метод навигации
        public void Navigate() => Console.WriteLine($"Прокладываем маршрут до {Destination}");
    }

    /// <summary>
    /// Класс Car
    /// </summary>
    public class Car
    {
        // Автосвойства
        public string Model { get; set; }
        public int Speed { get; set; }


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="speed">Скорость</param>
        public Car(string model, int speed = 0)
        {
            Model = model;
            Speed = speed;
        }

        // Метод езды
        public void Drive() => Console.WriteLine($"{Model} едет со скоростью {Speed} км/ч");
    }

    /// <summary>
    /// Автомобиль с опциями. Опции подключаются через композицию,
    /// а не через наследование — это позволяет комбинировать их в любом наборе.
    /// </summary>
    public class CarWithOptions : Car
    {
        // Автосвойства
        public IAirConditioner AirConditioner { get; set; }
        public INavigator Navigator { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="speed">Скорость</param>
        public CarWithOptions(string model, int speed = 0) : base(model, speed) { }
    }

    internal class Program
    {
        static void Main()
        {
            // Машина только с кондиционером
            CarWithOptions car1 = new CarWithOptions("Lada", 60)
            {
                AirConditioner = new AirConditioner()
            };

            car1.Drive();
            car1.AirConditioner.TurnOnAC();

            Console.WriteLine();

            // Машина с кондиционером и навигатором одновременно
            CarWithOptions car2 = new CarWithOptions("BMW", 120)
            {
                AirConditioner = new AirConditioner { Temperature = 18 },
                Navigator = new GpsNavigator { Destination = "Москва" }
            };
            car2.Drive();
            car2.AirConditioner.TurnOnAC();
            car2.Navigator?.Navigate();

            Console.WriteLine();

            // Машина без опций
            CarWithOptions car3 = new CarWithOptions("Oka", 40);
            car3.Drive();
        }
    }
}
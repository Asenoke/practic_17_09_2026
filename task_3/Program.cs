namespace task_3
{
    /// <summary>
    /// Интерфейс IFlyed (полёт птицы)
    /// </summary>
    interface IFlyed
    {
        public void Fly();
    }

    /// <summary>
    /// Класс Penguin (летать не может)
    /// </summary>
    public class Penguin
    {
      // Нет полёта.  
    }

    /// <summary>
    /// Класс Bird (наследуется от интерфейса IFlyed)
    /// </summary>
    public class Bird : IFlyed
    {
        // Метод полёта
        public void Fly()
        {
            Console.WriteLine("Птица летит высоко в небе!");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Bird bird = new Bird();
            bird.Fly();

        }
    }
}

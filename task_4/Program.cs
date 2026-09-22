namespace task_4
{
    /// <summary>
    /// Интерфейс ITurnOn
    /// </summary>
    public interface ITurnOn
    {
        // Метод включения
        public void TurnOn();
    }

    /// <summary>
    /// Интерфейс ITurnOff
    /// </summary>
    public interface ITurnOff
    {
        // Метод выключения
        public void TurnOff();
    }

    /// <summary>
    /// Интерфейс IPlayMusic
    /// </summary>
    public interface IPlayMusic
    {
        // Метод воспроизведения музыки
        public void PlayMusic();
    }

    /// <summary>
    /// Интерфейс IStartVideoRecording
    /// </summary>
    public interface IStartVideoRecording
    {
        // Метод начала записи видео
        public void StartVideoRecording();
    }

    /// <summary>
    /// Класс SmartBulb (наследуется от ITurnOn, ITurnOff)
    /// </summary>
    public class SmartBulb : ITurnOn, ITurnOff
    {
        public void TurnOn() => Console.WriteLine("Лампа включена");

        public void TurnOff() => Console.WriteLine("Лампа выключена");
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            SmartBulb bulb = new SmartBulb();
            bulb.TurnOn();
            bulb.TurnOff();
        }
    }
}

using System.Diagnostics.Metrics;
using static System.Console;
internal class Program
{
    interface ICar
    {
        public delegate void CarFinishEvent();
        public event CarFinishEvent ?Finish;
        public string NameCar { get; set; }//название транспорта
        public int MaxSpeed { get; set; }//максимальная скорость для транспорта в гонке
        public int Distance { get; set; }//пройденнгое расстояние во время гонки 
        public void Drive();//здесь будет вызываться событие Finish

        
    }
    class Bus : ICar
    {
        public int MaxSpeed { get ; set ; }
        public int Distance { get; set ; }
        public string NameCar { get; set; }

        public event ICar.CarFinishEvent? Finish;

        Random rand =  new Random();
        public void Drive()
        {
            int distancePerSec = rand.Next(1, MaxSpeed);

            Distance += distancePerSec;

            if (Distance >= 100)
            {
                Finish?.Invoke();
            }
        }

    }
    class Truck : ICar
    {
        public string NameCar { get; set; }
        public int MaxSpeed { get; set; }
        public int Distance { get; set; }

        public event ICar.CarFinishEvent? Finish;
        Random rand = new Random();
        public void Drive()
        {
            int distancePerSec = rand.Next(1, MaxSpeed);

            Distance += distancePerSec;

            if (Distance >= 100)
            {
                Finish?.Invoke();
            }
        }
    }

    class SportCar : ICar
    {
        public string NameCar { get; set ; }
        public int MaxSpeed { get; set; }
        public int Distance { get; set; }

        public event ICar.CarFinishEvent? Finish;
        Random rand = new Random();
        public void Drive()
        {
            int distancePerSec = rand.Next(1, MaxSpeed);

            Distance += distancePerSec;

            if (Distance >= 100)
            {
                Finish?.Invoke();
            }
        }
    }
    class PassengerCar : ICar
    {
        public string NameCar { get ; set; }
        public int MaxSpeed { get; set; }
        public int Distance { get; set; }

        public event ICar.CarFinishEvent? Finish;
        Random rand = new Random();
        public void Drive()
        {
            int distancePerSec = rand.Next(1, MaxSpeed);

            Distance += distancePerSec;

            if (Distance >= 100)
            {
                Finish?.Invoke();
            }
        }
    }

    class Game
    {
        public delegate void CarStartEvent();
        public event CarStartEvent? Start;

        List<ICar> _membersOfRace = new List<ICar> 
        {
           new Bus {MaxSpeed=60,Distance=0,NameCar="School Bus"},
           new Truck {MaxSpeed = 70, Distance=0,NameCar="Freightliner"},
           new SportCar {MaxSpeed= 100, Distance = 0,NameCar="Ferrari"},
           new PassengerCar {MaxSpeed = 80,Distance = 0, NameCar = "Toyota corolla" }
        };
        public void StartRace()
        {
            foreach (var item in _membersOfRace)
                Start += () => { WriteLine($"\t{item.NameCar} прибыл на старт гонки!"); }; 
            Start?.Invoke(); 
        }
        public void Race()
        {
            foreach (var item in _membersOfRace)
                item.Finish += () => { WriteLine($"\t{item.NameCar} финишировал первым!"); };
            int countSec = 1;
            while (true)
            {
                foreach (var item in _membersOfRace)
                {
                    item.Drive();
                    WriteLine("{0}\tпройденная дистанция\t{1}m\t{2}", countSec++, item.Distance, item.NameCar);

                }

                if (_membersOfRace[0].Distance >= 100 ||
                    _membersOfRace[1].Distance >= 100 ||
                    _membersOfRace[2].Distance >= 100 ||
                    _membersOfRace[3].Distance >= 100)
                {
                    break;
                }
            }
        }
    }

    private static void Main(string[] args)
    {
        Game game = new();
        game.StartRace();
        game.Race();

    }
}
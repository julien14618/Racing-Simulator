using Controller;
using System;

namespace Racing_Simulator
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.SetWindowSize(100, 60);
            Data.Initialize();
            Data.NextRace();
            Data.CurrentRace.SetParticipants();
            Data.CurrentRace.DriversChanged += Visualisatie.DriverChanged;
            Data.CurrentRace.Start();
            Console.ReadLine();
        }
    }
}
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
            //Visualisatie.DrawTrack(Data.CurrentRace.Track);

            //Visualisatie.DriverChanged(null, new Model.DriversChangedEventArgs(Data.CurrentRace.Track));
            Data.CurrentRace.Start();
            Console.ReadLine();
        }
    }
}
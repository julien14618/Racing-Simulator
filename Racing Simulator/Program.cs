using Controller;
using System;
using System.Runtime.CompilerServices;

namespace Racing_Simulator
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();
            Console.WriteLine(Data.CurrentRace.Track.Name);
            Data.NextRace();
            Console.WriteLine(Data.CurrentRace.Track.Name);
            Data.NextRace();
            Data.CurrentRace.SetParticipants();
            Visualisatie.DrawTrack(Data.CurrentRace.Track);
            //Visualisatie.DriverChanged(null, new Model.DriversChangedEventArgs(Data.CurrentRace.Track));
            Data.CurrentRace.start();
            Console.ReadLine();
        }
    }
}
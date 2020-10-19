using Controller;
using System;

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
        }
    }
}
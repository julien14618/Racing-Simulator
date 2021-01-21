using Controller;
using Model;
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
            Data.CurrentRace.RaceFinished += RaceFinished;
            Data.CurrentRace.Start();
            Console.ReadLine();
        }
        public static void RaceFinished(Object sender, RaceFinishedEventArgs raceFinishedEventArgs)
        {
            Console.Clear();
            Data.Competition.FillRecordAndEndRace(raceFinishedEventArgs.Participants);
            Data.CurrentRace.DisposeEventHandlers();
            Data.CurrentRace.DriversChanged += Visualisatie.DriverChanged;
            Data.CurrentRace.RaceFinished += RaceFinished;
            Data.CurrentRace.SetParticipants();
            Data.CurrentRace.Start();
        }
    }
}
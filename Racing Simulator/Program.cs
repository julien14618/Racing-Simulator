using Controller;
using System;
using Model;

namespace Racing_Simulator
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            Data.Initialize();
            Data.NextRace();
            Console.WriteLine(Data.CurrentRace.Track.Name);
            Data.NextRace();
            Console.WriteLine(Data.CurrentRace.Track.Name);
        }
    }
}

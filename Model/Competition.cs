using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }
        public Records<PointsPerParticipant> PointsPerParticipant = new Records<PointsPerParticipant>();
        public Records<TimePerParticipant> TimePerParticipant = new Records<TimePerParticipant>();
        

        public void RaceFinished(Object sender, RaceFinishedEventArgs raceFinishedEventArgs)
        {
            FillRecordAndEndRace(raceFinishedEventArgs.Participants);
       
        }
        public void printResults()
        {
            foreach (var v in PointsPerParticipant._list)
            {
                Console.WriteLine($"{v.Name} got {v.Points} points");
            }
            Console.WriteLine("Race has ended");
        }

        public void FillRecordAndEndRace(List<IParticipant> participants)
        {           
            int counter = participants.Count;
            for (int i = 0; i < counter; i++)
            {
                int maxPoints = 0;
                Driver driverBuffer = new Driver();
                foreach (var v in participants)
                {
                    if (v.Points > maxPoints)
                    {
                        driverBuffer = (Driver)v;
                        maxPoints = v.Points;
                    }                                     
                }
                PointsPerParticipant.AddToList(new Model.PointsPerParticipant(driverBuffer.Name, driverBuffer.Points));
                participants.Remove(driverBuffer);
            }
        }

        public Track NextTrack()
        {
            if (Tracks.Count() > 0)
            {
                return Tracks.Dequeue();
            }
            return null;
        }

        public Competition(List<IParticipant> participants, Queue<Track> tracks)
        {
            Tracks = new Queue<Track>();
            Participants = new List<IParticipant>();
            Participants = participants;
            Tracks = tracks;
        }

        public Competition()
        {
            Tracks = new Queue<Track>();
            Participants = new List<IParticipant>();
            PointsPerParticipant._list = new List<PointsPerParticipant>();
            TimePerParticipant._list = new List<TimePerParticipant>();
        }
    }
}
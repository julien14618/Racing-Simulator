using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }

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
        }
    }
}
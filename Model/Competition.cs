using System.Collections.Generic;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }
        public Track NextTrack()
        {
            Track returnTrack = null;
            if(Tracks.Count > 0)
            {
                returnTrack = Tracks.Dequeue();
            }
            return returnTrack;
        }
    }
}
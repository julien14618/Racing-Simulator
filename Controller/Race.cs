using Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Controller
{
    public class Race
    {
        public Race(Track track, List<IParticipant> participants)
        {
            Participants = participants;
            Track = track;
            _random = new Random(DateTime.Now.Millisecond);
        }

        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;
        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();
        public SectionData GetSectionData(Section section)
        {
            SectionData returnSection = null;
            if(!_positions.TryGetValue(section, out returnSection))
            {
                _positions.Add(section, new SectionData());
            }
            return returnSection;
        }
        public void RandomizeEquipment()
        {
            foreach(Driver d in Participants)
            {
                Random r1 = new Random();
                d.Equipment.Quality = r1.Next();
                d.Equipment.Performance = r1.Next();
            }
        }
    }
}

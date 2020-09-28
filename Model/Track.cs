using System.Collections.Generic;
using static Model.Section;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }
        public Track(string name, SectionTypes[] sections) { }
    }
}

using System.Collections.Generic;
using static Model.Section;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }
        public Track(string name, SectionTypes[] sections)
        {
            Name = name;
            Sections = new LinkedList<Section>();
            foreach(SectionTypes s in sections)
            {
                Section s1 = new Section();
                s1.SectionType = s;
                Sections.AddLast(s1);
            }
           
        }
    }
}

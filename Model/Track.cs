using System.Collections.Generic;

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
            if(sections != null)
                Sections = ConvertArrayToLinkedList(sections);
        }
        public LinkedList<Section> ConvertArrayToLinkedList(SectionTypes[] sectionTypes)
        {
            LinkedList<Section> sections = new LinkedList<Section>();
            foreach(var c in sectionTypes)
            {
                Section newSection = new Section(c);
                sections.AddLast(newSection);
            }
            return sections;
        }
    }
}
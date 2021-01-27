using Controller;
using Model;
using NUnit.Framework;
using System.Collections.Generic;
using WPFVisualisatie;

namespace WPFVisualisationTest
{
    public class RaceVisualisationTest
    {
        [SetUp]
        public void Setup()
        {
            WPFVisualisatie.RaceVisualisation.Initialize();
        }

        [Test]
        public void TestFillTrackPieces()
        {
            SectionTypes[] sectionst1 = { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner };
            Track t1 = new Track("test", sectionst1);
            List<IParticipant> testList = new List<IParticipant>();
            testList.Add(new Driver());
            Data.Competition = new Competition();
            Data.Competition.Participants = testList;
            Data.CurrentRace = new Race(t1,Data.Competition.Participants);
            
            WPFVisualisatie.RaceVisualisation.FillTrackPieces(t1.Sections);
            Assert.AreEqual(16, RaceVisualisation.trackPieces.Count);
        }
        
    }
}
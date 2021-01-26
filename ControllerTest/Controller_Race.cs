using NUnit.Framework;
using System;
using Model;
using Controller;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ControllerTest
{
    [TestFixture]
    public class Controller_Race
    {
        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
            Data.NextRace();
        }

        [Test]
        public void TestAwardPoints()
        {
            SectionData sd = new SectionData();
            sd.Left = new Driver();
            sd.Right = new Driver();
            Data.CurrentRace.AwardPoints(sd, "Left");
            Data.CurrentRace.AwardPoints(sd, "Right");
            Assert.AreEqual(100, sd.Left.Points);
            Assert.AreEqual(50, sd.Right.Points);
        }
        [Test]
        public void TestSetParticipants()
        {
            Data.CurrentRace.SetParticipants();
            SectionData sd = Data.CurrentRace.GetSectionData(Data.CurrentRace.Track.Sections.ElementAt(2));
            Assert.AreEqual("ADriver 1", sd.Right.Name);
        }
        [Test]
        public void TestGetSectionData()
        {
            Data.CurrentRace.Track = new Track("Test", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.Finish });
            Assert.IsNull(Data.CurrentRace.GetSectionData(Data.CurrentRace.Track.Sections.ElementAt(0)));
            Assert.IsNotNull(Data.CurrentRace.GetSectionData(Data.CurrentRace.Track.Sections.ElementAt(0)));
        }
    }
}

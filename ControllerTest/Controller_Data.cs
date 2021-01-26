using NUnit.Framework;
using System;
using Model;
using Controller;
using System.Collections.Generic;
using System.Text;

namespace ControllerTest
{
    [TestFixture]
    public class Controller_Data
    {
        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
        }
        [Test]
        public void TestInitialize()
        {
            Assert.NotNull(Data.Competition);
            Assert.NotNull(Data.Competition.Participants);
            Assert.NotNull(Data.Competition.Tracks);
        }
        [Test]
        public void TestAddparticipant()
        {
            Assert.AreEqual(Data.Competition.Participants.Count, 4);
            Assert.AreEqual(Data.Competition.Participants[2].Name, "CDriver 3");
        }
        [Test]
        public void TestAddTrack()
        {
            Assert.AreEqual(Data.Competition.Tracks.Count, 3);
            Assert.AreEqual(Data.Competition.Tracks.Peek().Name, "Zandvoort");
            Data.NextRace();
        }
        [Test]
        public void TestNextTrack()
        {
            Data.NextRace();
            Assert.AreEqual(Data.Competition.Tracks.Peek().Name, "Spa");
        }
    }
}

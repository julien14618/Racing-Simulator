using Model;
using NUnit.Framework;

namespace ControllerTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition;

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            var result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            Track t1 = new Track("berend", null);
            _competition.Tracks.Enqueue(t1);
            var result = _competition.NextTrack();
            Assert.AreEqual(t1, result);
        }
        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            
            Track t2 = new Track("Karel", null);
            Track t3 = new Track("Henk", null);
            _competition.Tracks.Enqueue(t2);
            _competition.Tracks.Enqueue(t3);
            var result = _competition.NextTrack();
            Assert.AreEqual(t2, result);
            var result2 = _competition.NextTrack();
            Assert.AreEqual(t3, result2);
        }
        
    }
}
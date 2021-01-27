using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using WPFVisualisatie;

namespace WPFVisualisationTest
{
    [TestFixture]
    
    public class TestCreateImage
    {
        [SetUp]
        public void SetUp()
        {
            CreateImages.Initialise();
        }
        [Test]
        public void TestGetImageFromCache()
        {
            Bitmap testBm = new Bitmap("C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images/Racetrack_Car_Blue.png");
            CreateImages.GetImageFromCache("C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images/Racetrack_Car_Blue.png");
            Assert.AreEqual(testBm.ToString(), CreateImages.imageCache["C:/Users/julie/source/repos/Racing Simulator/WPFVisualisatie/Images/Racetrack_Car_Blue.png"].ToString());
        }
    }
}

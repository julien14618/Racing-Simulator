using NUnit.Framework;
using System;
using Model;
using Controller;
using System.Collections.Generic;
using System.Text;

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
        //
        //[Test]
        //public void TestBrokenOrFixed()
        //{
        //    Data.CurrentRace
        //}
    }
}

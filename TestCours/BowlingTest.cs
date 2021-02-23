using Bowling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestCours
{
    [TestClass]
    public class BowlingTest
    {
        private IGenerator generator = Mock.Of<IGenerator>();
        //Tester premier lance
        [TestMethod]
        public void SimpleFrameFirstRoll()
        {
            //Arrange            
            Mock.Get(generator).Setup(g => g.RandomPins(10)).Returns(6);
            Frame frame = new Frame(generator);

            //Act
            frame.Roll();

            //Assert
            Assert.AreEqual(6, frame.Score);
        }

        [TestMethod]
        public void SimpleFrameSecondRoll()
        {
            //Arrange            
            Mock.Get(generator).Setup(g => g.RandomPins(4)).Returns(3);
            List<Roll> rolls = new List<Roll>() { new Roll(6) };
            Frame frame = new Frame(generator);
            frame.Rolls = rolls;

            //Act
            frame.Roll();

            //Assert
            Assert.AreEqual(9, frame.Score);
        }

        [TestMethod]
        public void SimpleFrameSecondRollAfterStrike()
        {
            //Arrange            
            Mock.Get(generator).Setup(g => g.RandomPins(4)).Returns(3);
            List<Roll> rolls = new List<Roll>() { new Roll(10) };
            Frame frame = new Frame(generator);
            frame.Rolls = rolls;

            //Act
            bool result = frame.Roll();

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SimpleFrameMoreRolls()
        {
            //Arrange           
            Mock.Get(generator).Setup(g => g.RandomPins(4)).Returns(3);
            List<Roll> rolls = new List<Roll>() { new Roll(9), new Roll(1) };
            Frame frame = new Frame(generator);
            frame.Rolls = rolls;

            //Act
            bool result = frame.Roll();

            //Assert
            Assert.IsFalse(result);
        }
    }

}

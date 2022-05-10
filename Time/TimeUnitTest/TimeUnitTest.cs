using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TimeLibrary;

namespace TimeUnitTest
{
    [TestClass]
    public class TimeUnitTest
    {
        #region ConstructorExceptions
        [DataTestMethod, TestCategory("ConstructorExceptions")]
        [DataRow(-1)]
        [DataRow(100000)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1paramInt_ArgumentOutOfRangeException(long seconds)
        {
            new Time(seconds);
        }

        [DataTestMethod, TestCategory("ConstructorExceptions")]
        [DataRow(-1)]
        [DataRow(24)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1paramInt_ArgumentOutOfRangeException(int hours)
        {
            new Time(hours);
        }

        [DataTestMethod, TestCategory("ConstructorExceptions")]
        [DataRow("24:0:0")]
        [DataRow("0:60:0")]
        [DataRow("0:0:60")]
        [DataRow("-1:0:0")]
        [DataRow("0:-1:0")]
        [DataRow("0:0:-1")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1paramString_ArgumentOutOfRangeException(string time)
        {
            new Time(time);
        }

        [DataTestMethod, TestCategory("ConstructorExceptions")]
        [DataRow(-1, 0)]
        [DataRow(0, -1)]
        [DataRow(24, 0)]
        [DataRow(0, 60)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2paramInt_ArgumentOutOfRangeException(int hours, int minutes)
        {
            new Time(hours, minutes);
        }

        [DataTestMethod, TestCategory("ConstructorExceptions")]
        [DataRow(-1, 0, 0)]
        [DataRow(0, -1, 0)]
        [DataRow(0, 0, -1)]
        [DataRow(24, 0, 0)]
        [DataRow(0, 60, 0)]
        [DataRow(0, 0, 60)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3paramInt_ArgumentOutOfRangeException(int hours, int minutes, int seconds)
        {
            new Time(hours, minutes, seconds);
        }

        [DataTestMethod, TestCategory("ConstructorExceptions")]
        [DataRow("000000")]
        [DataRow("0:0")]
        [DataRow("0:0:0:0")]
        [DataRow("::")]
        [ExpectedException(typeof(FormatException))]
        public void Constructor_1paramString_FormatException(string time)
        {
            new Time(time);
        }
        #endregion
        #region Constructors
        private void AssertTime(Time t, long expectedSSM, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            Assert.AreEqual(t.SecondsSinceMidnight, expectedSSM);
            Assert.AreEqual(t.Hours, expectedHours);
            Assert.AreEqual(t.Minutes, expectedMinutes);
            Assert.AreEqual(t.Seconds, expectedSeconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(100, 100, 0, 1, 40)]
        [DataRow(1, 1, 0, 0, 1)]
        [DataRow(0, 0, 0, 0, 0)]
        [DataRow(60, 60, 0, 1, 0)]
        [DataRow(3661, 3661, 1, 1, 1)]
        public void Constructor_1paramLong(long secondsToCreator, int expectedSSM, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            AssertTime(new Time(secondsToCreator), expectedSSM, expectedHours, expectedMinutes, expectedSeconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(0, 0, 0, 0, 0)]
        [DataRow(1, 3600, 1, 0, 0)]
        [DataRow(2, 7200, 2, 0, 0)]
        public void Constructor_1paramInt(int hoursToCreator, int expectedSSM, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            AssertTime(new Time(hoursToCreator), expectedSSM, expectedHours, expectedMinutes, expectedSeconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow("0:0:0", 0, 0, 0, 0)]
        [DataRow("00:00:00", 0, 0, 0, 0)]
        [DataRow("01:00:00", 3600, 1, 0, 0)]
        [DataRow("00:01:00", 60, 0, 1, 0)]
        [DataRow("00:00:01", 1, 0, 0, 1)]
        public void Constructor_1paramString(string stringToCreator, int expectedSSM, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            AssertTime(new Time(stringToCreator), expectedSSM, expectedHours, expectedMinutes, expectedSeconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(0, 0, 0, 0, 0, 0)]
        [DataRow(1, 0, 3600, 1, 0, 0)]
        [DataRow(0, 1, 60, 0, 1, 0)]
        [DataRow(1, 1, 3660, 1, 1, 0)]
        public void Constructor_2params(int hoursToCreator, int minutesToCreator, int expectedSSM, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            AssertTime(new Time(hoursToCreator, minutesToCreator), expectedSSM, expectedHours, expectedMinutes, expectedSeconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(0, 0, 0, 0, 0, 0, 0)]
        [DataRow(0, 0, 1, 1, 0, 0, 1)]
        [DataRow(0, 1, 0, 60, 0, 1, 0)]
        [DataRow(0, 1, 1, 61, 0, 1, 1)]
        [DataRow(1, 0, 0, 3600, 1, 0, 0)]
        [DataRow(1, 0, 1, 3601, 1, 0, 1)]
        [DataRow(1, 1, 0, 3660, 1, 1, 0)]
        [DataRow(1, 1, 1, 3661, 1, 1, 1)]
        public void Constructor_3params(int hoursToCreator, int minutesToCreator, int secondsToCreator, int expectedSSM, int expectedHours, int expecredMinutes, int expectedSeconds)
        {
            AssertTime(new Time(hoursToCreator, minutesToCreator, secondsToCreator), expectedSSM, expectedHours, expecredMinutes, expectedSeconds);
        }
        #endregion
        #region PublicMethods
        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("00:00:00", "00:00:00")]
        [DataRow("0:0:0", "00:00:00")]
        [DataRow("23:59:59", "23:59:59")]
        public void ToString_ExplicitToString(string input, string expectedOutput)
        {
            Assert.AreEqual(new Time(input).ToString(), expectedOutput);
        }

        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("00:00:00", "00:00:00")]
        [DataRow("00:01:00", "00:01:00")]
        public void Equals_ReturnsTrue(string input1, string input2)
        {
            Assert.AreEqual(new Time(input1), new Time(input2));
        }

        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("00:00:00", "00:00:01")]
        [DataRow("00:01:00", "00:01:01")]
        public void Equals_ReturnsFalse(string input1, string input2)
        {
            Assert.AreNotEqual(new Time(input1), new Time(input2));
        }

        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("00:00:00", 0)]
        [DataRow("1:1:1", 3661)]
        public void GetHashCode_Test(string input, int expectedHashCode)
        {
            Assert.AreEqual(new Time(input).GetHashCode(), expectedHashCode);
        }

        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("00:00:00", "00:00:00", 0)]
        [DataRow("00:1:00", "00:00:30", 1)]
        [DataRow("00:1:00", "00:1:30", -1)]
        public void CompareTo_Test(string input1, string input2, int expectedInt)
        {
            Assert.AreEqual(new Time(input1).CompareTo(new Time(input2)), expectedInt);
        }

        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("0:0:0", "0:0:0", "0:0:0")]
        [DataRow("0:1:30", "0:0:30", "0:2:0")]
        [DataRow("23:59:59", "0:0:1", "0:0:0")]
        public void Plus_Test(string input1, string input2, string input3)
        {
            Assert.AreEqual(new Time(input1).Plus(new Time(input2)), new Time(input3));
            Assert.AreEqual(Time.Plus(new Time(input1), new Time(input2)), new Time(input3));
        }
        #endregion
        #region Operators

        #endregion
    }
}
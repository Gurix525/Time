using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeLibrary;

namespace TimeUnitTest
{
    [TestClass]
    public class TimePeriodUnitTest
    {
        #region ConstructorExceptions
        [DataTestMethod, TestCategory("ConstructorExceptions")]
        [DataRow(-1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1paramInt_ArgumentOutOfRangeException(long seconds)
        {
            new TimePeriod(seconds);
        }

        [DataTestMethod, TestCategory("ConstructorExceptions")]
        [DataRow("0:60:0")]
        [DataRow("0:0:60")]
        [DataRow("-1:0:0")]
        [DataRow("0:-1:0")]
        [DataRow("0:0:-1")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1paramString_ArgumentOutOfRangeException(string time)
        {
            new TimePeriod(time);
        }

        [DataTestMethod, TestCategory("ConstructorExceptions")]
        [DataRow(-1, 0)]
        [DataRow(0, -1)]
        [DataRow(0, 60)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2paramInt_ArgumentOutOfRangeException(int hours, int minutes)
        {
            new TimePeriod(hours, minutes);
        }

        [DataTestMethod, TestCategory("ConstructorExceptions")]
        [DataRow(-1, 0, 0)]
        [DataRow(0, -1, 0)]
        [DataRow(0, 0, -1)]
        [DataRow(0, 60, 0)]
        [DataRow(0, 0, 60)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3paramInt_ArgumentOutOfRangeException(int hours, int minutes, int seconds)
        {
            new TimePeriod(hours, minutes, seconds);
        }

        [DataTestMethod, TestCategory("ConstructorExceptions")]
        [DataRow("000000")]
        [DataRow("0:0")]
        [DataRow("0:0:0:0")]
        [DataRow("::")]
        [ExpectedException(typeof(FormatException))]
        public void Constructor_1paramString_FormatException(string time)
        {
            new TimePeriod(time);
        }
        #endregion
        #region Constructors
        private void AssertTimePeriod(TimePeriod t, long expectedAllSeconds, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            Assert.AreEqual(t.AllSeconds, expectedAllSeconds);
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
        [DataRow(172800, 172800, 48, 0, 0)]
        public void Constructor_1paramLong(long secondsToCreator, int expectedSSM, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            AssertTimePeriod(new TimePeriod(secondsToCreator), expectedSSM, expectedHours, expectedMinutes, expectedSeconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow("0:0:0", 0, 0, 0, 0)]
        [DataRow("00:00:00", 0, 0, 0, 0)]
        [DataRow("01:00:00", 3600, 1, 0, 0)]
        [DataRow("00:01:00", 60, 0, 1, 0)]
        [DataRow("00:00:01", 1, 0, 0, 1)]
        [DataRow("48:00:00", 172800, 48, 0, 0)]
        public void Constructor_1paramString(string stringToCreator, int expectedSSM, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            AssertTimePeriod(new TimePeriod(stringToCreator), expectedSSM, expectedHours, expectedMinutes, expectedSeconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow(0, 0, 0, 0, 0, 0)]
        [DataRow(1, 0, 3600, 1, 0, 0)]
        [DataRow(0, 1, 60, 0, 1, 0)]
        [DataRow(1, 1, 3660, 1, 1, 0)]
        [DataRow(48, 0, 172800, 48, 0, 0)]
        public void Constructor_2params(int hoursToCreator, int minutesToCreator, int expectedSSM, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            AssertTimePeriod(new TimePeriod(hoursToCreator, minutesToCreator), expectedSSM, expectedHours, expectedMinutes, expectedSeconds);
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
        [DataRow(48, 0, 0, 172800, 48, 0, 0)]
        public void Constructor_3params(int hoursToCreator, int minutesToCreator, int secondsToCreator, int expectedSSM, int expectedHours, int expecredMinutes, int expectedSeconds)
        {
            AssertTimePeriod(new TimePeriod(hoursToCreator, minutesToCreator, secondsToCreator), expectedSSM, expectedHours, expecredMinutes, expectedSeconds);
        }
        #endregion
        #region PublicMethods
        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("00:00:00", "00:00:00")]
        [DataRow("0:0:0", "00:00:00")]
        [DataRow("23:59:59", "23:59:59")]
        [DataRow("48:00:00", "48:00:00")]
        [DataRow("100:00:00", "100:00:00")]
        public void ToString_Test(string input, string expectedOutput)
        {
            Assert.AreEqual(new TimePeriod(input).ToString(), expectedOutput);
        }

        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("10:30:00", "10:30:00")]
        [DataRow("100:0:0", "100:0:0")]
        public void ToString_ComplementaryCreator(string input, string expectedOutput)
        {
            Assert.AreEqual(new TimePeriod(new TimePeriod(input).ToString()), new TimePeriod(expectedOutput));
        }

        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("00:00:00", "00:00:00")]
        [DataRow("00:01:00", "00:01:00")]
        [DataRow("100:00:00", "100:0:0")]
        public void Equals_ReturnsTrue(string input1, string input2)
        {
            Assert.AreEqual(new TimePeriod(input1), new TimePeriod(input2));
        }

        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("00:00:00", "00:00:01")]
        [DataRow("00:01:00", "00:01:01")]
        public void Equals_ReturnsFalse(string input1, string input2)
        {
            Assert.AreNotEqual(new TimePeriod(input1), new TimePeriod(input2));
        }

        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("00:00:00", 0)]
        [DataRow("1:1:1", 3661)]
        public void GetHashCode_Test(string input, int expectedHashCode)
        {
            Assert.AreEqual(new TimePeriod(input).GetHashCode(), expectedHashCode);
        }

        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("00:00:00", "00:00:00", 0)]
        [DataRow("00:1:00", "00:00:30", 1)]
        [DataRow("00:1:00", "00:1:30", -1)]
        public void CompareTo_Test(string input1, string input2, int expectedInt)
        {
            Assert.AreEqual(new TimePeriod(input1).CompareTo(new TimePeriod(input2)), expectedInt);
        }

        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("0:0:0", "0:0:0", "0:0:0")]
        [DataRow("0:1:30", "0:0:30", "0:2:0")]
        [DataRow("23:59:59", "0:0:1", "24:0:0")]
        public void Plus_Test(string input1, string input2, string input3)
        {
            Assert.AreEqual(new TimePeriod(input1).Plus(new TimePeriod(input2)), new TimePeriod(input3));
            Assert.AreEqual(TimePeriod.Plus(new TimePeriod(input1), new TimePeriod(input2)), new TimePeriod(input3));
        }

        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("0:0:0", "0:0:0", "0:0:0")]
        [DataRow("0:1:30", "0:0:30", "0:1:0")]
        [DataRow("0:0:30", "1:0:0", "0:0:0")]
        public void Minus_Test(string input1, string input2, string input3)
        {
            Assert.AreEqual(new TimePeriod(input1).Minus(new TimePeriod(input2)), new TimePeriod(input3));
            Assert.AreEqual(TimePeriod.Minus(new TimePeriod(input1), new TimePeriod(input2)), new TimePeriod(input3));
        }

        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("0:0:0", "0:0:0", "0:0:0")]
        [DataRow("23:0:0", "2:0:0", "25:0:0")]
        public void Plus_DifferentTypes(string input1, string input2, string input3)
        {
            Assert.AreEqual(new TimePeriod(input1).Plus(new Time(input2)), new TimePeriod(input3));
            Assert.AreEqual(TimePeriod.Plus(new TimePeriod(input1), new Time(input2)), new TimePeriod(input3));
        }

        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("0:0:0", "0:0:0", "0:0:0")]
        [DataRow("1:0:0", "2:0:0", "0:0:0")]
        public void Minus_DifferentTypes(string input1, string input2, string input3)
        {
            Assert.AreEqual(new TimePeriod(input1).Minus(new Time(input2)), new TimePeriod(input3));
            Assert.AreEqual(TimePeriod.Minus(new TimePeriod(input1), new Time(input2)), new TimePeriod(input3));
        }
        #endregion
        #region Operators
        [TestMethod, TestCategory("Operators")]
        [DataRow("00:00:00", "0:0:0")]
        [DataRow("01:02:03", "1:2:3")]
        public void Operator_Equals(string input1, string input2)
        {
            Assert.IsTrue(new TimePeriod(input1) == new TimePeriod(input2));
            Assert.IsFalse(new TimePeriod(input1) != new TimePeriod(input2));
        }

        [TestMethod, TestCategory("Operators")]
        [DataRow("00:00:01", "0:0:0")]
        [DataRow("00:01:00", "0:0:1")]
        public void Operator_Greater_Lesser(string input1, string input2)
        {
            Assert.IsTrue(new TimePeriod(input1) > new TimePeriod(input2));
            Assert.IsFalse(new TimePeriod(input1) < new TimePeriod(input2));
        }

        [TestMethod, TestCategory("Operators")]
        [DataRow("00:00:00", "0:0:0")]
        [DataRow("1:1:1", "0:0:0")]
        public void Operator_GreaterOrEqual_LesserOrEqual(string input1, string input2)
        {
            Assert.IsTrue(new TimePeriod(input1) >= new TimePeriod(input2));
            Assert.IsTrue(new TimePeriod(input2) <= new TimePeriod(input1));
        }

        [TestMethod, TestCategory("Operators")]
        [DataRow("1:1:1", "1:1:1", "2:2:2")]
        public void Operator_Plus(string input1, string input2, string input3)
        {
            Assert.AreEqual(new TimePeriod(input1) + new TimePeriod(input2), new TimePeriod(input3));
        }

        [TestMethod, TestCategory("Operators")]
        [DataRow("1:1:1", "2:2:2", "0:0:0")]
        public void Operator_Minus(string input1, string input2, string input3)
        {
            Assert.AreEqual(new TimePeriod(input1) - new TimePeriod(input2), new TimePeriod(input3));
        }

        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("0:0:0", "0:0:0", "0:0:0")]
        [DataRow("23:0:0", "2:0:0", "25:0:0")]
        public void Operator_Plus_DifferentTypes(string input1, string input2, string input3)
        {
            Assert.AreEqual(new TimePeriod(input1) + new Time(input2), new TimePeriod(input3));
        }

        [TestMethod, TestCategory("PublicMethods")]
        [DataRow("0:0:0", "0:0:0", "0:0:0")]
        [DataRow("1:0:0", "2:0:0", "0:0:0")]
        public void Operator_Minus_DifferentTypes(string input1, string input2, string input3)
        {
            Assert.AreEqual(new TimePeriod(input1) - new Time(input2), new TimePeriod(input3));
        }
        #endregion
    }
}

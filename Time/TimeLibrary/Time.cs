using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLibrary
{
    public struct Time : IEquatable<Time>, IComparable<Time>, IFormattable
    {
        // Fields
        private readonly long _seconds;

        // Properties
        public byte Hours
        {
            get => Convert.ToByte(_seconds / 3600);
        }
        public byte Minutes
        {
            get => Convert.ToByte((_seconds % 3600) / 60);
        }
        public byte Seconds
        {
            get => Convert.ToByte(_seconds % 60);
        }
        public long SecondsSinceMidnight
        {
            get => _seconds;
        }

        // Creators
        public Time(long seconds)
        {
            if (seconds < 0 || seconds >= 24 * 3600) throw new ArgumentOutOfRangeException("Seconds must contain in range: 0 – 86399");
            _seconds = seconds;
        }
        public Time(int hours)
        {
            if (hours < 0 || hours >= 24) throw new ArgumentOutOfRangeException("Hours must contain in range: 0 – 23");
            _seconds = hours * 3600;
        }
        public Time(string time)
        {
            string[] numbersString = time.Split(':', StringSplitOptions.RemoveEmptyEntries);
            if (numbersString.Length != 3) throw new FormatException("Must be in correct format [hh:mm:ss].");
            int[] numbersInt = new int[3];
            for (int i = 0; i < 3; i++) numbersInt[i] = int.Parse(numbersString[i]);
            if (numbersInt[0] < 0 || numbersInt[0] >= 24) throw new ArgumentOutOfRangeException("Hours must contain in range: 0 – 23");
            if (numbersInt[1] < 0 || numbersInt[1] >= 60) throw new ArgumentOutOfRangeException("Minutes must contain in range: 0 – 59");
            if (numbersInt[2] < 0 || numbersInt[2] >= 60) throw new ArgumentOutOfRangeException("Seconds must contain in range: 0 – 59");
            _seconds = numbersInt[0] * 3600 + numbersInt[1] * 60 + numbersInt[2];
        }
        public Time(int hours, int minutes)
        {
            if (hours < 0 || hours >= 24) throw new ArgumentOutOfRangeException("Hours must contain in range: 0 – 23");
            if (minutes < 0 || minutes >= 60) throw new ArgumentOutOfRangeException("Minutes must contain in range: 0 – 59");
            _seconds = hours * 3600 + minutes * 60;
        }
        public Time(int hours, int minutes, int seconds)
        {
            if (hours < 0 || hours >= 24) throw new ArgumentOutOfRangeException("Hours must contain in range: 0 – 23");
            if (minutes < 0 || minutes >= 60) throw new ArgumentOutOfRangeException("Minutes must contain in range: 0 – 59");
            if (seconds < 0 || seconds >= 60) throw new ArgumentOutOfRangeException("Seconds must contain in range: 0 – 59");
            _seconds = hours * 3600 + minutes * 60 + seconds;
        }

        // Public methods
        public override string ToString()
        {
            return $"{string.Format("{0:00}", Hours)}:{string.Format("{0:00}", Minutes)}:{string.Format("{0:00}", Seconds)}";
        }
        public string ToString(string? format)
        {
            return $"{string.Format("{0:00}", Hours)}:{string.Format("{0:00}", Minutes)}:{string.Format("{0:00}", Seconds)}";
        }
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            return $"{string.Format("{0:00}", Hours)}:{string.Format("{0:00}", Minutes)}:{string.Format("{0:00}", Seconds)}";
        }
        public bool Equals(Time other)
        {
            if (SecondsSinceMidnight == other.SecondsSinceMidnight) return true;
            else return false;
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Time) return false;
            return Equals((Time)obj);
        }
        public static bool Equals(Time t1, Time t2)
        {
            return t1.Equals(t2);
        }
        public override int GetHashCode()
        {
            return _seconds.GetHashCode();
        }
        public int CompareTo(Time other)
        {
            return SecondsSinceMidnight.CompareTo(other.SecondsSinceMidnight);
        }
        public Time Plus(Time other)
        {
            long temporal = SecondsSinceMidnight + other.SecondsSinceMidnight >= 24 * 3600 ?
                SecondsSinceMidnight + other.SecondsSinceMidnight - 24 * 3600 :
                SecondsSinceMidnight + other.SecondsSinceMidnight;
            return new Time(temporal);
        }
        public static Time Plus(Time t1, Time t2)
        {
            return t1.Plus(t2);
        }
        

        // Operators
        public static bool operator ==(Time t1, Time t2)
        {
            return t1.Equals(t2);
        }
        public static bool operator !=(Time t1, Time t2)
        {
            return !(t1 == t2);
        }
        public static bool operator <(Time t1, Time t2)
        {
            return t1.SecondsSinceMidnight < t2.SecondsSinceMidnight;
        }
        public static bool operator >(Time t1, Time t2)
        {
            return t1.SecondsSinceMidnight > t2.SecondsSinceMidnight;
        }
        public static bool operator >=(Time t1, Time t2)
        {
            return t1.SecondsSinceMidnight >= t2.SecondsSinceMidnight;
        }
        public static bool operator <=(Time t1, Time t2)
        {
            return t1.SecondsSinceMidnight <= t2.SecondsSinceMidnight;
        }
        public static Time operator +(Time t1, Time t2)
        {
            return t1.Plus(t2);
        }
    }
}

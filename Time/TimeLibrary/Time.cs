using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLibrary
{
    /// <summary>
    ///  Struktura przechowująca czas w formacie hh:mm:ss.
    /// </summary>
    public struct Time : IEquatable<Time>, IComparable<Time>, IFormattable
    {
        #region Fields

        private readonly long _seconds;

        #endregion
        #region Properties

        /// <summary>
        /// Zwraca liczbę godzin.
        /// </summary>
        public byte Hours
            => Convert.ToByte(_seconds / 3600);

        /// <summary>
        /// Zwraca liczbę minut (modulo).
        /// </summary>
        public byte Minutes
            => Convert.ToByte((_seconds % 3600) / 60);

        /// <summary>
        /// Zwraca liczbę sekund (modulo).
        /// </summary>
        public byte Seconds
            => Convert.ToByte(_seconds % 60);

        /// <summary>
        /// Zwraca liczbę sekund od północy.
        /// </summary>
        public long SecondsSinceMidnight
            => _seconds;

        #endregion
        #region Constructors

        public Time(long seconds)
        {
            if (seconds < 0 || seconds >= 24 * 3600)
                throw new ArgumentOutOfRangeException("Seconds must contain in range: 0 – 86399");
            _seconds = seconds;
        }
        
        public Time(int hours)
        {
            if (hours < 0 || hours >= 24)
                throw new ArgumentOutOfRangeException("Hours must contain in range: 0 – 23");
            _seconds = hours * 3600;
        }
        
        public Time(string time)
        {
            string[] numbersString = time.Split(':', StringSplitOptions.RemoveEmptyEntries);
            if (numbersString.Length != 3)
                throw new FormatException("Must be in correct format [hh:mm:ss].");
            int[] numbersInt = new int[3];
            for (int i = 0; i < 3; i++)
                numbersInt[i] = int.Parse(numbersString[i]);
            if (numbersInt[0] < 0 || numbersInt[0] >= 24)
                throw new ArgumentOutOfRangeException("Hours must contain in range: 0 – 23");
            if (numbersInt[1] < 0 || numbersInt[1] >= 60)
                throw new ArgumentOutOfRangeException("Minutes must contain in range: 0 – 59");
            if (numbersInt[2] < 0 || numbersInt[2] >= 60)
                throw new ArgumentOutOfRangeException("Seconds must contain in range: 0 – 59");
            _seconds = numbersInt[0] * 3600 + numbersInt[1] * 60 + numbersInt[2];
        }
        
        public Time(int hours, int minutes)
        {
            if (hours < 0 || hours >= 24)
                throw new ArgumentOutOfRangeException("Hours must contain in range: 0 – 23");
            if (minutes < 0 || minutes >= 60)
                throw new ArgumentOutOfRangeException("Minutes must contain in range: 0 – 59");
            _seconds = hours * 3600 + minutes * 60;
        }
        
        public Time(int hours, int minutes, int seconds)
        {
            if (hours < 0 || hours >= 24)
                throw new ArgumentOutOfRangeException("Hours must contain in range: 0 – 23");
            if (minutes < 0 || minutes >= 60)
                throw new ArgumentOutOfRangeException("Minutes must contain in range: 0 – 59");
            if (seconds < 0 || seconds >= 60)
                throw new ArgumentOutOfRangeException("Seconds must contain in range: 0 – 59");
            _seconds = hours * 3600 + minutes * 60 + seconds;
        }

        #endregion
        #region Public methods

        /// <summary>
        /// Reprezentacja tekstowa struktury Time.
        /// </summary>
        /// <returns>String w formacie hh:mm:ss</returns>
        public override string ToString()
            => $"{string.Format("{0:00}", Hours)}:{string.Format("{0:00}", Minutes)}:{string.Format("{0:00}", Seconds)}";

        /// <summary>
        /// Reprezentacja tekstowa struktury Time.
        /// </summary>
        /// <returns>String w formacie hh:mm:ss</returns>
        public string ToString(string? format)
            => ToString();

        /// <summary>
        /// Reprezentacja tekstowa struktury Time.
        /// </summary>
        /// <returns>String w formacie hh:mm:ss</returns>
        public string ToString(string? format, IFormatProvider? formatProvider)
            => ToString();

        /// <summary>
        /// Metoda powrównawcza struktur Time.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true, jeśli obiekty są takie same.</returns>
        public bool Equals(Time other)
            => SecondsSinceMidnight == other.SecondsSinceMidnight ?
            true :
            false;
        
        /// <summary>
        /// Metoda powrównawcza struktur Time.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true, jeśli obiekty są takie same.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj is not Time)
                return false;
            return Equals((Time)obj);
        }

        /// <summary>
        /// Metoda powrównawcza struktur Time.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true, jeśli obiekty są takie same.</returns>
        public static bool Equals(Time t1, Time t2)
            => t1.Equals(t2);

        /// <summary>
        /// Metoda GetHashCode.
        /// </summary>
        /// <returns>HashCode na podstawie liczby sekund.</returns>
        public override int GetHashCode()
            => _seconds.GetHashCode();

        /// <summary>
        /// Metoda porównawcza do sortowania.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>-1, 0 lub 1</returns>
        public int CompareTo(Time other)
            => SecondsSinceMidnight.CompareTo(other.SecondsSinceMidnight);

        /// <summary>
        /// Metoda sumująca.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>sumę dwóch obiektów Time.</returns>
        public Time Plus(Time other)
        {
            long temporal = SecondsSinceMidnight + other.SecondsSinceMidnight >= 24 * 3600 ?
                SecondsSinceMidnight + other.SecondsSinceMidnight - 24 * 3600 :
                SecondsSinceMidnight + other.SecondsSinceMidnight;
            return new Time(temporal);
        }
        
        /// <summary>
        /// Metoda sumująca obiekty Time oraz TimePeriod.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>sumę obiektów Time i TimePeriod jako obiekt Time.</returns>
        public Time Plus(TimePeriod other)
        {
            long temporal = SecondsSinceMidnight + other.AllSeconds;
            while (temporal >= 24 * 3600)
                temporal -= 24 * 3600;
            return new Time(temporal);
        }
        
        /// <summary>
        /// Metoda odejmująca.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>różnicę dwóch obiektów Time.</returns>
        public Time Minus(Time other)
        {
            long temporal = SecondsSinceMidnight - other.SecondsSinceMidnight < 0 ?
                SecondsSinceMidnight - other.SecondsSinceMidnight + 24 * 3600 :
                SecondsSinceMidnight - other.SecondsSinceMidnight;
            
            return new Time(temporal);
        }
        
        /// <summary>
        /// Metoda odejmująca.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>różnicę obiektów Time i TimePeriod jako obiekt Time.</returns>
        public Time Minus(TimePeriod other)
        {
            long temporal = SecondsSinceMidnight - other.AllSeconds;
            while (temporal < 0)
                temporal += 24 * 3600;

            return new Time(temporal);
        }

        /// <summary>
        /// Metoda sumująca.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>sumę dwóch obiektów Time.</returns>
        public static Time Plus(Time t1, Time t2)
            => t1.Plus(t2);

        /// <summary>
        /// Metoda sumująca obiekty Time oraz TimePeriod.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>sumę obiektów Time i TimePeriod jako obiekt Time.</returns>
        public static Time Plus(Time t1, TimePeriod t2)
            => t1.Plus(t2);

        /// <summary>
        /// Metoda odejmująca.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>różnicę dwóch obiektów Time.</returns>
        public static Time Minus(Time t1, Time t2)
            => t1.Minus(t2);

        /// <summary>
        /// Metoda odejmująca.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns>różnicę obiektów Time i TimePeriod jako obiekt Time.</returns>
        public static Time Minus(Time t1, TimePeriod t2)
            => t1.Minus(t2);

        #endregion
        #region Operators

        public static bool operator ==(Time t1, Time t2)
            => t1.Equals(t2);

        public static bool operator !=(Time t1, Time t2)
            => !(t1 == t2);

        public static bool operator <(Time t1, Time t2)
            => t1.SecondsSinceMidnight < t2.SecondsSinceMidnight;

        public static bool operator >(Time t1, Time t2)
            => t1.SecondsSinceMidnight > t2.SecondsSinceMidnight;

        public static bool operator >=(Time t1, Time t2)
            => t1.SecondsSinceMidnight >= t2.SecondsSinceMidnight;

        public static bool operator <=(Time t1, Time t2)
            => t1.SecondsSinceMidnight <= t2.SecondsSinceMidnight;

        public static Time operator +(Time t1, Time t2)
            => t1.Plus(t2);

        public static Time operator +(Time t1, TimePeriod t2)
            => t1.Plus(t2);

        public static Time operator -(Time t1, Time t2)
            => t1.Minus(t2);

        public static Time operator -(Time t1, TimePeriod t2)
            => t1.Minus(t2);

        #endregion
    }
}

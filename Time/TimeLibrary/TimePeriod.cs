using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLibrary
{
    /// <summary>
    /// Struktura przechowująca odcinek czasu w formacie h:mm:ss.
    /// </summary>
    public struct TimePeriod : IFormattable, IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        #region Fields

        private readonly long _seconds;

        #endregion
        #region Properties

        /// <summary>
        /// Zwraca liczbę godzin.
        /// </summary>
        public long Hours
            => _seconds / 3600;

        /// <summary>
        /// Zwraca liczbę minut.
        /// </summary>
        public long Minutes
            => (_seconds % 3600) / 60;

        /// <summary>
        /// Zwraca liczbę sekund.
        /// </summary>
        public long Seconds
            => _seconds % 60;

        /// <summary>
        /// Zwraca liczbę sekund sumarycznie.
        /// </summary>
        public long AllSeconds
            => _seconds;

        /// <summary>
        /// Zwraca tekstową reprezentację obiektu TimePeriod.
        /// </summary>
        public string Representation
            => ToString();

        #endregion
        #region Constructors

        public TimePeriod(long allSeconds)
        {
            if (allSeconds < 0)
                throw new ArgumentOutOfRangeException(nameof(allSeconds));
            _seconds = allSeconds;
        }

        public TimePeriod(string timePeriod)
        {
            string[] numbersString = timePeriod.Split(':', StringSplitOptions.RemoveEmptyEntries);
            if (numbersString.Length != 3)
                throw new FormatException("Must be in correct format [h:mm:ss].");
            int[] numbersInt = new int[3];
            for (int i = 0; i < 3; i++)
                numbersInt[i] = int.Parse(numbersString[i]);
            if (numbersInt[0] < 0)
                throw new ArgumentOutOfRangeException("Hours must be greater or equal to 0");
            if (numbersInt[1] < 0 || numbersInt[1] >= 60)
                throw new ArgumentOutOfRangeException("Minutes must contain in range: 0 – 59");
            if (numbersInt[2] < 0 || numbersInt[2] >= 60)
                throw new ArgumentOutOfRangeException("Seconds must contain in range: 0 – 59");
            _seconds = numbersInt[0] * 3600 + numbersInt[1] * 60 + numbersInt[2];
        }

        public TimePeriod(long hours, long minutes)
        {
            if (hours < 0)
                throw new ArgumentOutOfRangeException(nameof(hours));
            if (minutes < 0 || minutes >= 60)
                throw new ArgumentOutOfRangeException(nameof(minutes));
            _seconds = hours * 3600 + minutes * 60;
        }

        public TimePeriod(long hours, long minutes, long seconds)
        {
            if (hours < 0)
                throw new ArgumentOutOfRangeException(nameof(hours));
            if (minutes < 0 || minutes >= 60)
                throw new ArgumentOutOfRangeException(nameof(minutes));
            if (seconds < 0 || seconds >= 60)
                throw new ArgumentOutOfRangeException(nameof(seconds));
            _seconds = hours * 3600 + minutes * 60 + seconds;
        }

        #endregion
        #region PublicMethods

        /// <summary>
        /// Tekstowa reprezentacja obiektu TimePeriod.
        /// </summary>
        /// <returns>string w formacie h:mm:ss.</returns>
        public override string ToString()
            => $"{string.Format("{0:D2}", Hours)}:{string.Format("{0:00}", Minutes)}:{string.Format("{0:00}", Seconds)}";

        /// <summary>
        /// Tekstowa reprezentacja obiektu TimePeriod.
        /// </summary>
        /// <returns>string w formacie h:mm:ss.</returns>
        public string ToString(string? format)
            => ToString();

        /// <summary>
        /// Tekstowa reprezentacja obiektu TimePeriod.
        /// </summary>
        /// <returns>string w formacie h:mm:ss.</returns>
        public string ToString(string? format, IFormatProvider? formatProvider)
            => ToString();

        /// <summary>
        /// Metoda porównawcza obiektów TimePeriod.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true, jeśli obiekty są takie same.</returns>
        public bool Equals(TimePeriod other)
            => AllSeconds == other.AllSeconds ? true : false;

        /// <summary>
        /// Metoda porównawcza obiektów TimePeriod.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true, jeśli obiekty są takie same.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not TimePeriod) return false;
            return Equals((TimePeriod)obj);
        }

        /// <summary>
        /// Metoda porównawcza obiektów TimePeriod.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true, jeśli obiekty są takie same.</returns>
        public static bool Equals(TimePeriod t1, TimePeriod t2)
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
        /// <returns>-1, 0, 1</returns>
        public int CompareTo(TimePeriod other)
            => AllSeconds.CompareTo(other.AllSeconds);

        /// <summary>
        /// Metoda sumująca obiekty TimePeriod.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>sumę dwóch obiektów TimePeriod.</returns>
        public TimePeriod Plus(TimePeriod other)
            => new TimePeriod(AllSeconds + other.AllSeconds);

        /// <summary>
        /// Metoda odejmująca obiekty TimePeriod.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>różnicę dwóch obiektów TimePeriod.</returns>
        public TimePeriod Minus(TimePeriod other)
            => AllSeconds - other.AllSeconds < 0 ?
            new TimePeriod(0) :
            new TimePeriod(AllSeconds - other.AllSeconds);

        /// <summary>
        /// Metoda sumująca obiekty TimePeriod i Time.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>sumę obiektów TimePeriod i Time jako obiekt Time.</returns>
        public TimePeriod Plus(Time other)
            => new TimePeriod(AllSeconds + other.SecondsSinceMidnight);

        /// <summary>
        /// Metoda odejmująca obiekty TimePeriod i Time.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>różnicę obiektów TimePeriod i Time jako obiekt Time.</returns>
        public TimePeriod Minus(Time other)
            => AllSeconds - other.SecondsSinceMidnight < 0 ?
            new TimePeriod(0) :
            new TimePeriod(AllSeconds - other.SecondsSinceMidnight);

        /// <summary>
        /// Metoda sumująca obiekty TimePeriod.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>sumę dwóch obiektów TimePeriod.</returns>
        public static TimePeriod Plus(TimePeriod t1, TimePeriod t2)
            => t1.Plus(t2);

        /// <summary>
        /// Metoda odejmująca obiekty TimePeriod.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>różnicę dwóch obiektów TimePeriod.</returns>
        public static TimePeriod Minus(TimePeriod t1, TimePeriod t2)
            => t1.Minus(t2);

        /// <summary>
        /// Metoda sumująca obiekty TimePeriod i Time.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>sumę obiektów TimePeriod i Time jako obiekt Time.</returns>
        public static TimePeriod Plus(TimePeriod t1, Time t2)
            => t1.Plus(t2);

        /// <summary>
        /// Metoda odejmująca obiekty TimePeriod i Time.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>różnicę obiektów TimePeriod i Time jako obiekt Time.</returns>
        public static TimePeriod Minus(TimePeriod t1, Time t2)
            => t1.Minus(t2);

        #endregion
        #region Operators

        public static bool operator ==(TimePeriod t1, TimePeriod t2)
            => Equals(t1, t2);

        public static bool operator !=(TimePeriod t1, TimePeriod t2)
            => !(t1 == t2);

        public static bool operator <(TimePeriod t1, TimePeriod t2)
            => t1.AllSeconds < t1.AllSeconds;

        public static bool operator >(TimePeriod t1, TimePeriod t2)
            => t1.AllSeconds > t2.AllSeconds;

        public static bool operator >=(TimePeriod t1, TimePeriod t2)
            => t1.AllSeconds >= t2.AllSeconds;

        public static bool operator <=(TimePeriod t1, TimePeriod t2)
            => t1.AllSeconds <= t2.AllSeconds;

        public static TimePeriod operator +(TimePeriod t1, TimePeriod t2)
            => Plus(t1, t2);

        public static TimePeriod operator -(TimePeriod t1, TimePeriod t2)
            => Minus(t1, t2);

        public static TimePeriod operator +(TimePeriod t1, Time t2)
            => t1.Plus(t2);

        public static TimePeriod operator -(TimePeriod t1, Time t2)
            => t1.Minus(t2);

        #endregion
    }
}
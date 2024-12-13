using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharp_lab6
{
    public class Fraction : IFraction, ICloneable
    {
        private int numerator;   // Числитель
        private int denominator; // Знаменатель
        private double? cachedDoubleValue; // Кэшированное вещественное значение

        public Fraction(int numerator, int denominator)
        {

            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }

            this.numerator = numerator;
            this.denominator = denominator;
            cachedDoubleValue = null;
            Simplify();
        }
        public override string ToString()
        {
            return $"{numerator}/{denominator}";
        }
        public static Fraction operator +(Fraction a, Fraction b)
        {
            int newNumerator = a.numerator * b.denominator + b.numerator * a.denominator;
            int newDenominator = a.denominator * b.denominator;
            return new Fraction(newNumerator, newDenominator);
        }
        public static Fraction operator +(Fraction a, int b)
        {
            return a + new Fraction(b, 1);
        }
        public static Fraction operator -(Fraction a, Fraction b)
        {
            int newNumerator = a.numerator * b.denominator - b.numerator * a.denominator;
            int newDenominator = a.denominator * b.denominator;
            return new Fraction(newNumerator, newDenominator);
        }
        public static Fraction operator -(Fraction a, int b)
        {
            return a - new Fraction(b, 1);
        }
        public static Fraction operator *(Fraction a, Fraction b)
        {
            int newNumerator = a.numerator * b.numerator;
            int newDenominator = a.denominator * b.denominator;
            return new Fraction(newNumerator, newDenominator);
        }
        public static Fraction operator *(Fraction a, int b)
        {
            return a * new Fraction(b, 1);
        }
        public static Fraction operator /(Fraction a, Fraction b)
        {
            int newNumerator = a.numerator * b.denominator;
            int newDenominator = a.denominator * b.numerator;
            return new Fraction(newNumerator, newDenominator);
        }
        public static Fraction operator /(Fraction a, int b)
        {
            return a / new Fraction(b, 1);
        }

        ////////
        ///

        public override bool Equals(object obj)
        {
            if (obj is Fraction other)
            {
                return this.numerator == other.numerator && this.denominator == other.denominator;
            }
            return false;
        }
        // ICloneable
        public object Clone()
        {
            return MemberwiseClone();
        }

        public double ToDouble()
        {
            if (!cachedDoubleValue.HasValue)
            {
                cachedDoubleValue = (double)numerator / denominator;
            }
            return cachedDoubleValue.Value;
        }

        
        public void SetNewValues(int numerator, int denominator)
        {
            cachedDoubleValue = null;
            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }
            this.numerator = numerator;
            this.denominator = denominator;
            Simplify();
        }


        private void Simplify()
        {
            int gcd = GCD(Math.Abs(numerator), denominator);
            numerator /= gcd;
            denominator /= gcd;
        }

        private int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }   
    }

}

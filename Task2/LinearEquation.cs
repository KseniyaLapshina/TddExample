﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task2
{
    public class LinearEquation
    {
        private List<double> coefficients;

        public LinearEquation(double[] coeff)
        {
            coefficients = new List<double>(coeff);
        }

        public LinearEquation(List<double> coeff)
        {
            coefficients = new List<double>(coeff);
        }

        public LinearEquation(string _coeff)
        {
            string[] coeff = Regex.Split(_coeff, @"[^\d.-]");
            coefficients = new List<double>();
            for (int i = 0; i < coeff.Length; i++)
            {
                if (coeff[i] != "")
                {
                    coeff[i] = coeff[i].Replace('.', ',');                                                          
                    coefficients.Add(double.Parse(coeff[i]));
                }
            }
        }

        public LinearEquation(IEnumerable<double> coeff)
        {
            coefficients = new List<double>(coeff);
        }

        public LinearEquation(int n)
        {
            if (n > 0)
            {
                coefficients = new List<double>();
                for (int i = 0; i <= n; i++)
                    coefficients.Add(0);
            }
            else throw new ArgumentException();
        }

        public int Size
        {
            get { return coefficients.Count; }
        }

        public void RandomInitialization()
        {
            Random rand = new Random();
            for (int i = 0; i < Size; i++)
                coefficients[i] = rand.Next(55) / 10;
        }

        public void SameInitialization(double value)
        {
            for (int i = 0; i < Size; i++)
                coefficients[i] = value;
        }

        public double this[int index]
        {
            get
            {
                if (index >= 0 && index < Size)
                    return coefficients[index];
                else throw new IndexOutOfRangeException();
            }
            set
            {
                if (index >= 0 && index < Size)
                    coefficients[index] = value;
                else throw new IndexOutOfRangeException();
            }
        }

        public static LinearEquation operator +(LinearEquation a, LinearEquation b)
        {
            var result = a.coefficients.Zip(b.coefficients, (first, second) => first + second);
            return new LinearEquation(result);
        }

        public static LinearEquation operator -(LinearEquation a, LinearEquation b)
        {
            var result = a.coefficients.Zip(b.coefficients, (first, second) => first - second);
            return new LinearEquation(result);
        }

        public static LinearEquation operator *(LinearEquation a, double r)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < a.Size; i++)
                result.Add(a[i] * r);
            return new LinearEquation(result);
        }

        public static LinearEquation operator *(double r, LinearEquation a)
        {
            return a * r;
        }

        public static LinearEquation operator -(LinearEquation a)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < a.Size; i++)
                result.Add(-a[i]);
            return new LinearEquation(result);
        }        

        public override string ToString()
        {
            string res = "";
            int i;
            for (i = 0; i < Size - 2; i++)            
                res += (coefficients[i + 1] >= 0) ? (coefficients[i].ToString() + 'x' + (i + 1).ToString() + '+') : (coefficients[i].ToString() + 'x' + (i + 1).ToString());            
            res += (coefficients[i].ToString() + 'x' + (i + 1).ToString());
            res += '=' + coefficients[Size - 1].ToString();
            return res;
        }

        public static bool operator ==(LinearEquation a, LinearEquation b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(LinearEquation a, LinearEquation b)
        {
            return !(a == b);
        }

        public static implicit operator double[] (LinearEquation a)
        {
            return a.coefficients.ToArray();
        }

        public static bool operator false(LinearEquation a)
        {
            if (a)
                return false;
            else
                return true;
        }

        public static bool operator true(LinearEquation a)
        {
            for (int i = 0; i < a.Size - 1; i++)
                if (a[i] != 0)
                    return true;
            return (a[a.Size - 1] != 0) ? false : true;
        }

        public override bool Equals(object obj)
        {
            LinearEquation a = (LinearEquation)obj;
            for (int i = 0; i < Size; i++)            
                if (Math.Abs(this[i] - a[i]) > 1e-9)
                    return false;            
            return true;
        }

        public bool IsNull()
        {
            for (int i = 0; i < Size; i++)
                if (this[i] != 0)
                    return false;
            return true;
        }

        public void CopyTo(LinearEquation a)
        {
            a.coefficients = coefficients.ToList();
        }
    }
}

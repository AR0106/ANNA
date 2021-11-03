using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AnnaMLTools.Regression
{

    [Serializable]
    public class NumberOfValuesNotEqualException : Exception
    {
        private int _xCount;
        private int _yCount;

        public NumberOfValuesNotEqualException(int value1, int value2)
        {
            _xCount = value1;
            _yCount = value2;
        }

        public override string Message
        {
            get
            {
                return $"The Number of Values Compared are not Equal. Value1 is {_xCount}, while Value2 is {_yCount}.";
            }
        }
    }

    public class LinearRegression
    {

        public static double a;
        public static double b;
        public static double averageError;

        private static List<double> errorList;
        private static List<double> xSquaredValues;
        private static List<double> ySquaredValues;
        private static List<double> xyValues;

        public static void TrainLinearModel(List<double> xValues, List<double> yValues)
        {
            xyValues = new List<double>();
            xSquaredValues = new List<double>();
            // Verifies xValues and yValues have the Same Amount of Values
            if (xValues.Count != yValues.Count)
            {
                throw new NumberOfValuesNotEqualException(xValues.Count, yValues.Count);
            }

            // Populate xyValues List with values of x*y
            for (int i = 0; i < yValues.Count; i++)
            {
                xyValues.Add(xValues[i] * yValues[i]);
            }

            // Populate xSquaredValues List with values of x^2
            for (int i = 0; i < yValues.Count; i++)
            {
                xSquaredValues.Add(xValues[i]*xValues[i]);
            }

            a = ((yValues.Sum() * xSquaredValues.Sum()) - (xValues.Sum() * xyValues.Sum())) / ((yValues.Count * xSquaredValues.Sum()) - (xValues.Sum() * xValues.Sum()));
            b = ((yValues.Count * xyValues.Sum()) - (xValues.Sum() * yValues.Sum())) / ((yValues.Count * xSquaredValues.Sum()) - (xValues.Sum() * xValues.Sum()));
        }

        public static void TrainPolynomialModel(List<double> xValues, List<double> yValues)
        {
            xyValues = new List<double>();
            xSquaredValues = new List<double>();
            // Verifies xValues and yValues have the Same Amount of Values
            if (xValues.Count != yValues.Count)
            {
                throw new NumberOfValuesNotEqualException(xValues.Count, yValues.Count);
            }

            // Populate xyValues List with values of x*y
            for (int i = 0; i < yValues.Count; i++)
            {
                xyValues.Add(xValues[i] * yValues[i]);
            }

            // Populate xSquaredValues List with values of x^2
            for (int i = 0; i < xValues.Count; i++)
            {
                xSquaredValues.Add(xValues[i] * xValues[i]);
            }

            // Populate ySquaredValues List with values of x^2
            for (int i = 0; i < yValues.Count; i++)
            {
                xSquaredValues.Add(yValues[i] * yValues[i]);
            }

            throw new NotImplementedException();
        }

        public static void TrainCorrector(List<double> xValues, List<double> yValues, int numOfDataElements, double maxPercentage, double minPercentage)
        {
            // Verifies xValues and yValues have the Same Amount of Values
            if (xValues.Count != yValues.Count)
            {
                throw new NumberOfValuesNotEqualException(xValues.Count, yValues.Count);
            }
            // Verifies the Number of Training Values are Within Training Range Params
            if (xValues.Count < numOfDataElements)
            {
                throw new NumberOfValuesNotEqualException(xValues.Count, numOfDataElements);
            }

            List<double> predictedValues = new List<double>();
            errorList = new List<double>();

            for (int i = 0; i < numOfDataElements; i++)
            {
                predictedValues.Add(Fit(xValues[i]));
            }

            for (int i = 0; i < predictedValues.Count; i++)
            {
                errorList.Add(yValues[i] / predictedValues[i]);
            }

            averageError = errorList.Sum() / errorList.Count;

            if (averageError > maxPercentage)
            {
                averageError = maxPercentage;
            }
            else if (averageError < minPercentage)
            {
                averageError = minPercentage;
            }
        }

        public static double Fit(double xValue) => a + (b * xValue);

        public static double Predict(double xValue) => Fit(xValue) * averageError;
    }
}

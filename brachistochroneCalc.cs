using System;
using MathNet.Numerics.RootFinding;
using ScottPlot;
using System.Drawing;

namespace brachistochrone
{
    public class BrachistochroneCalc
    {
        private readonly (double, double) _firstPoint;
        private readonly (double, double) _secondPoint;

        private readonly (double, double) _secondPointNew;

        public double[] xCoord { get; private set; }
        public double[] yCoord { get; private set; }
        private double cValue = -1;
        private double tEndValue = -1;
        private double time = -1;
        private const double step = 0.00001;


        public BrachistochroneCalc((double, double) FirstPoint, (double, double) SecondPoint)
        {
            if (FirstPoint.Item1 == SecondPoint.Item1 || FirstPoint.Item2 >= SecondPoint.Item2)
                throw new System.Exception("WRONG POINTS");
            _firstPoint = FirstPoint;
            _secondPoint = SecondPoint;
            if (SecondPoint.Item1 > FirstPoint.Item1)
                _secondPointNew = (_secondPoint.Item1 - _firstPoint.Item1, _secondPoint.Item2 - _firstPoint.Item2);
            else
                _secondPointNew = (-_secondPoint.Item1 + _firstPoint.Item1, _secondPoint.Item2 - _firstPoint.Item2);
            xCoord = new double[0];
            yCoord = new double[0];
        }
        public void brachistochroneCalcFind()
        {
            try
            {
                tEndValue = RobustNewtonRaphson.FindRoot(newTonFunc, newTonFuncDiff, step, Math.PI, step, int.MaxValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Время движения меньше 10^-5, невозможно провести расчеты");
            }
            cValue = Math.Sqrt((1 - Math.Cos(2 * tEndValue)) / (4 * 9.8 * _secondPointNew.Item2));
            var tArray = MathNet.Numerics.Generate.LinearRange(0.0, step, tEndValue);
            xCoord = new double[tArray.Length];
            yCoord = new double[tArray.Length];
            for (int i = 0; i < tArray.Length; ++i)
            {
                xCoord[i] = calcX(tArray[i]);
                yCoord[i] = -calcY(tArray[i]);
            }
            time = Math.Sqrt(tEndValue / (9.8 * 9.8 * cValue * cValue));
        }
        public void printAndSave(string filename)
        {
            var plt = new ScottPlot.Plot(600, 400);
            plt.AddScatter(xCoord, yCoord);
            plt.YAxis.TickLabelNotation(invertSign: true);
            plt.Title($"Необходимое время: t={Math.Round(time, 5)} c");

            plt.AddScatter(new[] { xCoord[0] }, new[] { yCoord[0] }, color: Color.Orange, markerSize: 16, lineWidth: 0, label: $"M1({_firstPoint.Item1};{_firstPoint.Item2})");

            var last = xCoord.Length - 1;
            plt.AddScatter(new[] { xCoord[last] }, new[] { yCoord[last] }, color: Color.Red, markerSize: 16, lineWidth: 0, label: $"M2({_secondPoint.Item1};{_secondPoint.Item2})");

            plt.Legend(location: ScottPlot.Alignment.LowerCenter);
            plt.XLabel("X");
            plt.YLabel("Y");
            plt.SaveFig(filename);
        }
        private double calcY(double t)
        {
            return (1 - Math.Cos(2 * t)) / (4 * 9.8 * cValue * cValue) + _firstPoint.Item2;
        }
        private double calcX(double t)
        {
            var sign = _secondPoint.Item1 > _firstPoint.Item1 ? 1 : -1;
            return sign * (2 * t - Math.Sin(2 * t)) / (4 * 9.8 * cValue * cValue) + _firstPoint.Item1;
        }
        private double newTonFunc(double t)
        {
            return _secondPointNew.Item2 / _secondPointNew.Item1
            - (1 - Math.Cos(2 * t)) / (2 * t - Math.Sin(2 * t));
        }
        private double newTonFuncDiff(double t)
        {
            return ((Math.Cos(2 * t) - 1) * (2 * Math.Cos(2 * t) - 2)) / Math.Pow(2 * t - Math.Sin(2 * t), 2) - 2 * Math.Sin(2 * t) / (2 * t - Math.Sin(2 * t));
        }
    }
}
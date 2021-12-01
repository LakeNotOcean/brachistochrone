using System;

namespace consoleFunctions
{
    public static class getData
    {
        public static double getDoubleData(string message, double minValue = double.MinValue, double maxValue = double.MaxValue)
        {
            double res;
            while (true)
            {
                Console.Write(message);
                var inputData = Console.ReadLine();
                if (double.TryParse(inputData, out res) && res >= minValue && res <= maxValue)
                    break;
                Console.WriteLine("Введено некорректное значение! Значение должно быть дробным и удовлетворять граничным условиям");
            }
            return res;
        }
        public static double getDoubleDataNotEquall(string message, double value)
        {
            double res;
            while (true)
            {
                Console.Write(message);
                var inputData = Console.ReadLine();
                if (double.TryParse(inputData, out res) && res != value)
                    break;
                Console.WriteLine("Введено некорректное значение! Значение должно быть дробным и удовлетворять граничным условиям");
            }
            return res;
        }
        public static double getPrevDouble(double value)
        {
            long bits = BitConverter.DoubleToInt64Bits(value);
            if (value > 0)
                return BitConverter.Int64BitsToDouble(bits - 1);
            else if (value < 0)
                return BitConverter.Int64BitsToDouble(bits + 1);
            else
                return -double.Epsilon;
        }
        public static double getNextDouble(double value)
        {
            long bits = BitConverter.DoubleToInt64Bits(value);
            if (value > 0)
                return BitConverter.Int64BitsToDouble(bits + 1);
            else if (value < 0)
                return BitConverter.Int64BitsToDouble(bits - 1);
            else
                return double.Epsilon;
        }
        public static double getTimeInSec(string message, double minValue = double.MinValue, double maxValue = double.MaxValue)
        {
            double res;
            while (true)
            {
                Console.Write(message);
                var inputData = Console.ReadLine();
                var inputValue = inputData.Split(' ');
                if (double.TryParse(inputValue[0], out res) && res > minValue && res < maxValue)
                {
                    if (inputValue[1] == "мин")
                        res *= 60;
                    break;
                }
                Console.WriteLine("Введено некорректное значение!");
            }
            return res;
        }
        public static long getLongData(string message, long minValue = long.MinValue, long maxValue = long.MaxValue)
        {
            long res;
            while (true)
            {
                Console.Write(message);
                var inputData = Console.ReadLine();
                if (long.TryParse(inputData, out res) && res >= minValue && res <= maxValue)
                    break;
                Console.WriteLine("Введено некорректное значение! Значение должно быть целым и удовлетворять граничным условиям");
            }
            return res;
        }

    }
}

namespace printMessage
{
    public static class FormatMessage
    {

        public static void printHelloMessage(string Message, int stringLength)
        {

            Console.WriteLine(getIndent(stringLength));
            Console.WriteLine(PadBoth(Message, stringLength));
            Console.WriteLine(PadBoth(howToEndMessage, stringLength));
            Console.WriteLine(getIndent(stringLength));
        }
        private const string howToEndMessage = "Для выхода из программы введите ctrl+C";
        public static string PadBoth(string source, int length)
        {
            int spaces = length - source.Length;
            int padLeft = spaces / 2 + source.Length;
            return source.PadLeft(padLeft, '#').PadRight(length, '#');
        }

        private static string getIndent(int countIndent)
        {
            return new string('#', countIndent);
        }
    }
}
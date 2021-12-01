using System;
using consoleFunctions;
using printMessage;

namespace brachistochrone
{
    class Program
    {
        static void Main(string[] args)
        {
            FormatMessage.printHelloMessage("Решение задачи о брахистохроне", 30);
            double x, y;
            x = getData.getDoubleData("Введите координату X первой точки: ");
            y = getData.getDoubleData("Введите координату Y первой точки: ");
            var FirstPoint = (x, y);
            x = getData.getDoubleDataNotEquall($"Введите координату X второй точки X!={FirstPoint.x} : ", FirstPoint.x);
            y = getData.getDoubleData($"Введите координату Y второй точки Y>{FirstPoint.y}: ", getData.getNextDouble(FirstPoint.y));
            var SecondPoint = (x, y);
            Console.WriteLine("Проведение расчетов...");
            var calc = new BrachistochroneCalc(FirstPoint, SecondPoint);
            calc.brachistochroneCalcFind();
            calc.printAndSave("result.png");
            Console.WriteLine("Задача решена! Файл с результатом - result.png");
            Console.WriteLine("Для выхода нажмите любую клавищу");
            Console.ReadLine();
        }

    }
}

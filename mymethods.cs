

namespace myNamespace
{
    public class myTetro
    {
        // Get game figures
        public static int[,] GetGameFigure()
        {
            Random i = new Random();
            switch (i.Next(0, 7))
            {
                case 1: return new int[,] { { 1, 1, 0 }, { 0, 1, 1 } };
                case 2: return new int[,] { { 0, 2, 2 }, { 2, 2, 0 } };
                case 3: return new int[,] { { 3, 3 }, { 3, 3 } };
                case 4: return new int[,] { { 4, 4, 4 }, { 0, 0, 4 } };
                case 5: return new int[,] { { 5, 5, 5 }, { 5, 0, 0 } };
                case 6: return new int[,] { { 6, 6, 6, 6 }, { 0, 0, 0, 0 } };
                default: return new int[,] { { 0, 7, 0 }, { 7, 7, 7 } };
            }
        }

        public static void ShowArray(int[,] inputArray)
        {
            Console.Clear();
            for (int i = 0; i < inputArray.GetLength(0); i++)
            {
                for (int j = 0; j < inputArray.GetLength(1); j++)
                    if (inputArray[i, j] != 0) Console.Write(inputArray[i, j]);
                    else Console.Write(" ");
                Console.WriteLine();
            }
        }

        public static int[,] HideFigure(int[,] field, int[,] figure, int cX, int cY)
        {
            for (int i = 0; i < figure.GetLength(0); i++)
            {
                for (int j = 0; j < figure.GetLength(1); j++)
                {
                    field[cY + i, cX + j] = 0;
                }
            }
            return field;
        }

        public static int[,] ShowFigure(int[,] field, int[,] figure, int cX, int cY)
        {
            for (int i = 0; i < figure.GetLength(0); i++)
            {
                for (int j = 0; j < figure.GetLength(1); j++)
                {
                    if (figure[i, j] != 0) field[cY + i, cX + j] = figure[i, j];
                }
            }
            return field;
        }

        public static bool FigureCanMove(int[,] field, int[,] figure, int cX, int cY, string direct)
        {
            switch (direct)
            {
                case "left":
                    {
                        break;
                    }
                case "right":
                    {
                        break;
                    }
                case "down":
                    {
                        // Если достигнут конец массива (дно поля)
                        if (cY == field.GetLength(0) - figure.GetLength(0)) return false;
                        for (int j = 0; j < figure.GetLength(1); j++)
                        {
                            // Для каждого j-того элемента нижней строки
                            // Если элемент нижней строки фигуры не равен нулю, и
                            // элемент игрового поля следующей строки не равен нулю
                            // перемещение невозможно
                            if (figure[figure.GetLength(0) - 1, j] != 0 && 
                                    field[cY + figure.GetLength(0), cX + j] != 0) return false;
                        }
                        break;
                    }
            }
            return true;
        }
    }
}

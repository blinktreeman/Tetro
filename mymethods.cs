namespace myNamespace
{
    public class myTetro
    {
        // Show game field
        public static void ShowPlayground(int sizeX, int sizeY)
        {
            for(int i = 0; i < sizeX; i++)
            {
                Console.Write("|");
                for(int j = 0; j < sizeY; j++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine("|");
            }
            for(int i = 0; i < sizeX + 2; i++) Console.Write("-");
        }

        public static void ShowResult(int result, int fieldSize)
        {
            Console.SetCursorPosition(0, fieldSize + 1);
            Console.WriteLine($"Собрано рядов: {result}");
        }
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
            for (int i = 0; i < inputArray.GetLength(0); i++)
            {
                Console.SetCursorPosition(1, i);
                for (int j = 0; j < inputArray.GetLength(1); j++)
                    if (inputArray[i, j] != 0) Console.Write("█");
                    //if (inputArray[i, j] != 0) Console.Write(inputArray[i, j]);
                    else Console.Write(".");
                Console.WriteLine();
            }
        }
        public static int[,] HideFigure(int[,] field, int[,] figure, int cX, int cY)
        {
            for (int i = 0; i < figure.GetLength(0); i++)
            {
                for (int j = 0; j < figure.GetLength(1); j++)
                {
                    if (figure[i, j] != 0) field[cY + i, cX + j] = 0;
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
        public static int[,] RotateFigure(int[,] figure)
        {
            int[,] tempArray = new int[figure.GetLength(1), figure.GetLength(0)];
            for (int i = 0; i < figure.GetLength(1); i++)
            {
                for (int j = 0; j < figure.GetLength(0); j++)
                {
                    tempArray[i, j] = figure[figure.GetLength(0) - 1 - j, i];
                }
            }
            return tempArray;
        }
        public static bool FigureCanRotate(int[,] field, int[,] figure, int cX, int cY)
        {
            HideFigure(field, figure, cX, cY);
            figure = RotateFigure(figure);
            // Если при повороте фигуры она выходит за границы поля
            if (cX + figure.GetLength(1) > field.GetLength(1) ||
                cY + figure.GetLength(0) > field.GetLength(0)) return false;
            // Перемножаем каждый элемент матрицы на элемент поля на 
            // котором он должен разместиться.
            // Если произведение отлично от нуля, разворот не возможен.
            for (int i = 0; i < figure.GetLength(0); i++)
            {
                for (int j = 0; j < figure.GetLength(1); j++)
                {
                    if (figure[i, j] * field[cY + i, cX + j] != 0) return false;
                }
            }
            return true;
        }
        public static bool FigureCanMove(int[,] field, int[,] figure, int cX, int cY, string direct)
        {
            switch (direct)
            {
                case "left":
                    {
                        if (cX == 0)
                        {
                            // Из-за шестерки
                            for (int i = 0; i < figure.GetLength(0); i++)
                            {
                                if (figure[i, 0] != 0) return false; // field (cY + i)
                            }
                        }
                        for (int i = 0; i < figure.GetLength(0); i++)
                        {
                            // Для каждой i-той строки
                            // устанавливаем значение первого столбцв
                            int j = 0;
                            // Определяем первый не нулевой элемент строки фигуры слева в пределах фигуры
                            while (figure[i, j] == 0 && j < figure.GetLength(1) - 1) j++;
                            // Если значение в следующем столбце (слева) игрового поля от первого значащего 
                            // элемента не ноль, перемещение невозможно
                            if (field[cY + i, cX + j - 1] != 0) return false;
                        }
                        break;
                    }
                case "right":
                    {
                        if (cX + figure.GetLength(1) == field.GetLength(1))
                        {
                            // Из-за шестерки
                            for (int i = 0; i < figure.GetLength(0); i++)
                            {
                                //if (field[cY + i, cX + figure.GetLength(1) - 1] != 0) return false;
                                if (figure[i, figure.GetLength(1) - 1] != 0) return false;
                            }
                        }
                        for (int i = 0; i < figure.GetLength(0); i++)
                        {
                            // Для каждой i-той строки
                            // устанавливаем значение последнего столбцв
                            int j = figure.GetLength(1) - 1;
                            // Определяем первый не нулевой элемент строки фигуры справа
                            while (figure[i, j] == 0 && j > 0) j--;
                            // Если значение в следующем столбце (справа) игрового поля от первого значащего 
                            // элемента не ноль, перемещение невозможно
                            if (field[cY + i, cX + j + 1] != 0) return false;
                        }
                        break;
                    }
                case "down":
                    {
                        // Если достигнут конец массива (дно поля)
                        if (cY >= field.GetLength(0) - figure.GetLength(0))
                        {
                            // Из-за шестерки
                            for (int j = 0; j < figure.GetLength(1); j++)
                            {
                                if (field[field.GetLength(0) - 1, cX + j] != 0) return false;
                            }
                        }
                        HideFigure(field, figure, cX, cY);
                        for (int i = 0; i < figure.GetLength(0); i++)
                        {
                            for (int j = 0; j < figure.GetLength(1); j++)
                            {
                                if (cY + figure.GetLength(0) < field.GetLength(0) &&    // Из-за шестерки
                                        figure[i, j] * field[cY + i + 1, cX + j] != 0) 
                                {
                                    ShowFigure(field, figure, cX, cY);
                                    return false;
                                }
                            }
                        }
                        break;
                    }
            }
            return true;
        }
    }
}

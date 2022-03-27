

namespace myNamespace
{
    public class myTetro
    {
        // Game field
        static int[,] field = new int[15, 15];
        int[,] currentFigure = GetGameFigure();
        // Стартовая позиция для фигуры
        int coordinateX = field.GetLength(1) / 2 - 2;
        int coordinateY = 0;

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
            //Console.Clear();
            Console.SetCursorPosition(0, 0);
            for (int i = 1; i < inputArray.GetLength(0); i++)
            {
                Console.Write("|");
                for (int j = 0; j < inputArray.GetLength(1); j++)
                    //if (inputArray[i, j] != 0) Console.Write("█");
                    if (inputArray[i, j] != 0) Console.Write(inputArray[i, j]);
                    else Console.Write(" ");
                Console.Write("|");
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

        /*
        public static ConsoleKeyInfo MonitorKeypress()
        {
            ConsoleKeyInfo innercki = new ConsoleKeyInfo();
            do innercki = Console.ReadKey(true); while (innercki.Key != ConsoleKey.UpArrow &&
                                                        innercki.Key != ConsoleKey.LeftArrow &&
                                                        innercki.Key != ConsoleKey.RightArrow
                                                        );
            //Task.Delay(500);
            return innercki;
        }
        */

        public static int[] MoveOrRotateFigure(int[,] field, int[,] figure, int cX, int cY)
        {
            int[] tempArray = new int[2];

            do
            {

                ConsoleKeyInfo cki = new ConsoleKeyInfo();

                cki = Console.ReadKey(true);

                switch (cki.Key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            if (myTetro.FigureCanMove(field, figure, cX, cY, "left"))
                            {
                                myTetro.HideFigure(field, figure, cX, cY);
                                cX--;
                                myTetro.ShowFigure(field, figure, cX, cY);
                            }
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            if (myTetro.FigureCanMove(field, figure, cX, cY, "right"))
                            {
                                myTetro.HideFigure(field, figure, cX, cY);
                                cX++;
                                myTetro.ShowFigure(field, figure, cX, cY);
                            }
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (myTetro.FigureCanRotate(field, figure, cX, cY))
                            {
                                myTetro.HideFigure(field, figure, cX, cY);
                                figure = myTetro.RotateFigure(figure);
                                myTetro.ShowFigure(field, figure, cX, cY);
                            }
                            break;
                        }
                    default: break;
                }
                tempArray[0] = cX;
                tempArray[1] = cY;
            } while (true);


            //return tempArray;
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
                                if (field[cY + i, cX + figure.GetLength(1) - 1] != 0) return false;
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

        public async void MoveDown()
        {
            while (myTetro.FigureCanMove(field, currentFigure, coordinateX, coordinateY, "down"))
            {
                var moveOrRotateTask = Task<int[]>.Run(() => myTetro.MoveOrRotateFigure(field, currentFigure, coordinateX, coordinateY));
                await Task.Delay(500);
                //moveOrRotateTask.
                //coordinateX = moveOrRotateTask.Result[0];
                //coordinateY = moveOrRotateTask.Result[1];
                // ConsoleKeyInfo cki = new ConsoleKeyInfo();
                //var consoleKeyTask = Task<ConsoleKeyInfo>.Run(() => myTetro.MonitorKeypress());
                //cki = consoleKeyTask.Result;

                // Если фигура сдвинулась по горизонтали, проверяем может ли она двигаться вниз
                // если нет, завершаем итерацию
                if (!myTetro.FigureCanMove(field, currentFigure, coordinateX, coordinateY, "down")) continue;

                // Затираем фигуру
                myTetro.HideFigure(field, currentFigure, coordinateX, coordinateY);

                // Рисуем с новыми координатами
                coordinateY++;
                myTetro.ShowFigure(field, currentFigure, coordinateX, coordinateY);

                myTetro.ShowArray(field);
            }
        }



    }
}

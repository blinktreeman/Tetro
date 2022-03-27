// One more tetris game

using myNamespace;

Console.Clear();
// Game field
int[,] gameField = new int[15, 15];
int[,] currentFigure = myTetro.GetGameFigure();
// Стартовая позиция для фигуры
int coordinateX = gameField.GetLength(1) / 2 - 2;
int coordinateY = 0;

void HideFigure(); //(int[,] field, int[,] figure, int cX, int cY)
{
    for (int i = 0; i < currentFigure.GetLength(0); i++)
    {
        for (int j = 0; j < currentFigure.GetLength(1); j++)
        {
            if (currentFigure[i, j] != 0) gameField[coordinateY + i, coordinateX + j] = 0;
        }
    }
    //return gameField;
}

void ShowFigure(); //(int[,] field, int[,] figure, int cX, int cY)
{
    for (int i = 0; i < currentFigure.GetLength(0); i++)
    {
        for (int j = 0; j < currentFigure.GetLength(1); j++)
        {
            if (currentFigure[i, j] != 0) gameField[coordinateY + i, coordinateX + j] = currentFigure[i, j];
        }
    }
    //return field;
}

int[,] RotateFigure(int[,] figure)
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

bool FigureCanRotate(int[,] field, int[,] figure, int cX, int cY)
{

    HideFigure(); //(field, figure, cX, cY);
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

int[] MoveOrRotateFigure(int[,] field, int[,] figure, int cX, int cY)
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
                    if (FigureCanMove(field, figure, cX, cY, "left"))
                    {
                        HideFigure(); //(field, figure, cX, cY);
                        cX--;
                        ShowFigure(field, figure, cX, cY);
                    }
                    break;
                }
            case ConsoleKey.RightArrow:
                {
                    if (FigureCanMove(field, figure, cX, cY, "right"))
                    {
                        HideFigure(); //(field, figure, cX, cY);
                        cX++;
                        ShowFigure(field, figure, cX, cY);
                    }
                    break;
                }
            case ConsoleKey.UpArrow:
                {
                    if (FigureCanRotate(field, figure, cX, cY))
                    {
                        HideFigure(); //(field, figure, cX, cY);
                        figure = RotateFigure(figure);
                        ShowFigure(field, figure, cX, cY);
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

bool FigureCanMove(int[,] field, int[,] figure, int cX, int cY, string direct)
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
                HideFigure(); //(field, figure, cX, cY);
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






/*
// Game field
int[,] field = new int[15, 15];
*/
for (int k = 0; k < 50; k++)
{
    // Стартовая позиция для фигуры
    /*
    int coordinateX = field.GetLength(1) / 2 - 2;
    int coordinateY = 0;

    int[,] currentFigure = myTetro.GetGameFigure();
    */
    // Перемещаем вниз
    while (FigureCanMove(gameField, currentFigure, coordinateX, coordinateY, "down"))
    {
        var moveOrRotateTask = Task<int[]>.Run(() => MoveOrRotateFigure(gameField, currentFigure, coordinateX, coordinateY));
        await Task.Delay(500);
        //moveOrRotateTask.
        //coordinateX = moveOrRotateTask.Result[0];
        //coordinateY = moveOrRotateTask.Result[1];
        // ConsoleKeyInfo cki = new ConsoleKeyInfo();
        //var consoleKeyTask = Task<ConsoleKeyInfo>.Run(() => myTetro.MonitorKeypress());
        //cki = consoleKeyTask.Result;

        // Если фигура сдвинулась по горизонтали, проверяем может ли она двигаться вниз
        // если нет, завершаем итерацию
        if (!FigureCanMove(gameField, currentFigure, coordinateX, coordinateY, "down")) continue;

        // Затираем фигуру
        HideFigure(); //(gameField, currentFigure, coordinateX, coordinateY);

        // Рисуем с новыми координатами
        coordinateY++;
        ShowFigure(gameField, currentFigure, coordinateX, coordinateY);

        myTetro.ShowArray(gameField);
    }
    /*
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
    */
}

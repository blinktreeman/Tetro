// One more tetris game

using myNamespace;

Console.Clear();
// Game field
int[,] gameField = new int[15, 15];

myTetro.ShowPlayground(gameField.GetLength(1), gameField.GetLength(0));
myTetro.ShowResult(0, gameField.GetLength(0));

int rowsComplete = 0;
int coordinateX = 0;
int coordinateY = 0;
int[,] currentFigure = new int[2, 2];

do
{
    // Стартовая позиция для фигуры
    coordinateX = gameField.GetLength(1) / 2 - 2;
    coordinateY = 0;
    // Получаем случайную фигуру
    currentFigure = myTetro.GetGameFigure();

    // Перемещаем вниз
    while (myTetro.FigureCanMove(gameField, currentFigure, coordinateX, coordinateY, "down"))
    {
        for (int i = 0; i < 20; i++)
        {
            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            await Task.Delay(20);
            if (Console.KeyAvailable) cki = Console.ReadKey(true);
            switch (cki.Key)
            {
                case ConsoleKey.LeftArrow:
                    {
                        if (myTetro.FigureCanMove(gameField, currentFigure, coordinateX, coordinateY, "left"))
                        {
                            myTetro.HideFigure(gameField, currentFigure, coordinateX, coordinateY);
                            coordinateX--;
                            myTetro.ShowFigure(gameField, currentFigure, coordinateX, coordinateY);
                        }
                        break;
                    }
                case ConsoleKey.RightArrow:
                    {
                        if (myTetro.FigureCanMove(gameField, currentFigure, coordinateX, coordinateY, "right"))
                        {
                            myTetro.HideFigure(gameField, currentFigure, coordinateX, coordinateY);
                            coordinateX++;
                            myTetro.ShowFigure(gameField, currentFigure, coordinateX, coordinateY);
                        }
                        break;
                    }
                case ConsoleKey.UpArrow:
                    {
                        if (myTetro.FigureCanRotate(gameField, currentFigure, coordinateX, coordinateY))
                        {
                            myTetro.HideFigure(gameField, currentFigure, coordinateX, coordinateY);
                            currentFigure = myTetro.RotateFigure(currentFigure);
                            myTetro.ShowFigure(gameField, currentFigure, coordinateX, coordinateY);
                        }
                        break;
                    }
                default: break;
            }
        }
        // Если фигура сдвинулась по горизонтали, проверяем может ли она двигаться вниз
        // если нет, завершаем итерацию
        if (!myTetro.FigureCanMove(gameField, currentFigure, coordinateX, coordinateY, "down")) continue;
        // Затираем фигуру
        myTetro.HideFigure(gameField, currentFigure, coordinateX, coordinateY);
        // Рисуем с новыми координатами
        coordinateY++;
        myTetro.ShowFigure(gameField, currentFigure, coordinateX, coordinateY);
        myTetro.ShowArray(gameField);
    }

    int tempRowNumber = gameField.GetLength(0) - 1;
    // Удаляем собраные строки
    for (int i = gameField.GetLength(0) - 1; i >= 0; i--)
    {
        bool rowComplete = true;
        for (int j = 0; j < gameField.GetLength(1); j++)
        {
            //Console.Write("dasfa");
            if (gameField[i, j] == 0) rowComplete = false;
        }
        if (!rowComplete)
        {
            for (int m = 0; m < gameField.GetLength(1); m++)
            {
                gameField[tempRowNumber, m] = gameField[i, m];
            }
            tempRowNumber--;
        }
        else myTetro.ShowResult(++rowsComplete, gameField.GetLength(0));
    }
    myTetro.ShowArray(gameField);
} while (!(!myTetro.FigureCanMove(gameField, currentFigure, coordinateX, coordinateY, "down") & coordinateY == 0));

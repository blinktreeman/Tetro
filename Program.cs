// One more tetris game

using myNamespace;

Console.Clear();
// Game field
int[,] gameField = new int[15, 15];

for (int k = 0; k < 50; k++)
{
    // Стартовая позиция для фигуры
    int coordinateX = gameField.GetLength(1) / 2 - 2;
    int coordinateY = 0;

    int[,] currentFigure = myTetro.GetGameFigure();

    Random rand = new Random();
    //ConsoleKeyInfo cki = new ConsoleKeyInfo();

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
}

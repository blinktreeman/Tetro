// One more tetris game

using myNamespace;

// Game field
int[,] gameField = new int[15, 15];

for (int k = 0; k < 50; k++)
{
    // Стартовая позиция для фигуры
    int coordinateX = gameField.GetLength(1) / 2 - 2;
    int coordinateY = 0;

    // Выводим фигуру на экран
    int[,] currentFigure = myTetro.GetGameFigure();
    for (int i = 0; i < currentFigure.GetLength(0); i++)
    {
        for (int j = 0; j < currentFigure.GetLength(1); j++)
        {
            gameField[coordinateY + i, coordinateX + j] = currentFigure[i, j];
        }
    }

    Random rand = new Random();

    // Перемещаем вниз
    while (myTetro.FigureCanMove(gameField, currentFigure, coordinateX, coordinateY, "down"))
    {
        /*
        Task.Factory.StartNew(() =>
        {
            while (Console.ReadKey().Key != ConsoleKey.Escape) ;
            //exit = true;
        });
        */

        await Task.Delay(200);

        switch (rand.Next(3))
        {
            case 0:
            {
                if (myTetro.FigureCanMove(gameField, currentFigure, coordinateX, coordinateY, "left"))
                {
                    gameField = myTetro.HideFigure(gameField, currentFigure, coordinateX, coordinateY);
                    coordinateX --;
                    gameField = myTetro.ShowFigure(gameField, currentFigure, coordinateX, coordinateY);
                }
                break;
            }
            case 1:
            {
                if (myTetro.FigureCanMove(gameField, currentFigure, coordinateX, coordinateY, "right"))
                {
                    gameField = myTetro.HideFigure(gameField, currentFigure, coordinateX, coordinateY);
                    coordinateX ++;
                    gameField = myTetro.ShowFigure(gameField, currentFigure, coordinateX, coordinateY);
                }
                break;
            }
            default: break;
        }

        if (!myTetro.FigureCanMove(gameField, currentFigure, coordinateX, coordinateY, "down")) continue;

        // Затираем фигуру
        gameField = myTetro.HideFigure(gameField, currentFigure, coordinateX, coordinateY);

        // Рисуем с новыми координатами
        coordinateY++;
        gameField = myTetro.ShowFigure(gameField, currentFigure, coordinateX, coordinateY);

        myTetro.ShowArray(gameField);
    }
}



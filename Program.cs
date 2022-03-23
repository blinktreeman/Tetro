// One more tetris game

using myNamespace;

// Game field
int[,] gameField = new int[10, 10];

for (int k = 0; k < 5; k++)
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

    // Перемещаем вниз
    while (myTetro.FigureCanMove(gameField, currentFigure, coordinateX, coordinateY, "down"))
    {
        await Task.Delay(500);

        // Затираем фигуру
        gameField = myTetro.HideFigure(gameField, currentFigure, coordinateX, coordinateY);

        // Рисуем с новыми координатами
        coordinateY++;
        gameField = myTetro.ShowFigure(gameField, currentFigure, coordinateX, coordinateY);

        myTetro.ShowArray(gameField);
    }
}



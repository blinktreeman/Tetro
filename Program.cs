// One more tetris game

using myNamespace;
{
    
};

// Game field
int[,] gameField = new int[15, 15];

for (int k = 0; k < 50; k++)
{
    // Стартовая позиция для фигуры
    int coordinateX = gameField.GetLength(1) / 2 - 2;
    int coordinateY = 0;

    int[,] currentFigure = myTetro.GetGameFigure();
    
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

        switch (rand.Next(4))
        {
            case 0:
            {
                if (myTetro.FigureCanMove(gameField, currentFigure, coordinateX, coordinateY, "left"))
                {
                    myTetro.HideFigure(gameField, currentFigure, coordinateX, coordinateY);
                    coordinateX --;
                    myTetro.ShowFigure(gameField, currentFigure, coordinateX, coordinateY);
                }
                break;
            }
            case 1:
            {
                if (myTetro.FigureCanMove(gameField, currentFigure, coordinateX, coordinateY, "right"))
                {
                    myTetro.HideFigure(gameField, currentFigure, coordinateX, coordinateY);
                    coordinateX ++;
                    myTetro.ShowFigure(gameField, currentFigure, coordinateX, coordinateY);
                }
                break;
            }
            case 2:
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

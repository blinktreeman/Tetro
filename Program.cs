// One more tetris game

using myNamespace;

Console.Clear();
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
    myTetro mt = new myTetro();
    mt.MoveDown();
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

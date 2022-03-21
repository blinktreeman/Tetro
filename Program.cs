// One more tetris game

// Game field
int[,] gameField = new int[10, 10];

// Game figures
int[,] figure01 = new int[,] {{1, 1, 0}, {0, 1, 1}};
int[,] figure02 = new int[,] {{0, 2, 2}, {2, 2, 0}};
int[,] figure03 = new int[,] {{3, 3}, {3, 3}};
int[,] figure04 = new int[,] {{4, 4, 4}, {0, 0, 4}};
int[,] figure05 = new int[,] {{5, 5, 5}, {5, 0, 0}};
int[] figure06 = new int[] {6, 6, 6, 6};
int[,] figure07 = new int[,] {{0, 7, 0}, {7, 7, 7}};

void ShowGameField()
{
    for (int i = 0; i < gameField.GetLength(0); i++)
    {
        for (int j = 0; j < gameField.GetLength(1); j++)
        Console.Write(gameField[i, j]);
    Console.WriteLine();
    }
}

ShowGameField();


namespace myNamespace
{
    public class myTetro
    {
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
    }
}

int n = int.Parse(Console.ReadLine());

char[,] matrix = new char[n, n];

int playerRow = 0, playerCol = 0;
int startingRow = 0, startingCol = 0;
int stars = 2;


for (int row = 0; row < n; row++)
{
    string[] inputMatrix = Console.ReadLine().Split().ToArray();

    for (int col = 0; col < n; col++)
    {
        matrix[row, col] = char.Parse(inputMatrix[col]);

        if (matrix[row, col] == 'P')
        {
            playerRow = row;
            playerCol = col;

        }

    }

}



while (true)
{
    if (stars == 0 || stars == 10)
    { break; }

    matrix[playerRow, playerCol] = '.';

    string move = Console.ReadLine();

    switch (move)
    {
        case "left":
            {
                playerCol--;


                if (playerCol == -1)
                {
                    playerRow = startingRow;
                    playerCol = startingCol;

                }
                else if (matrix[playerRow, playerCol] == '#')
                {
                    playerCol++;
                    stars--;
                }
                break;
            }

        case "right":
            {
                playerCol++;
                if (playerCol == n)
                {
                    playerRow = startingRow;
                    playerCol = startingCol;
                }

                else if (matrix[playerRow, playerCol] == '#')
                {
                    playerCol--;
                    stars--;
                }
                break;
            }

        case "up":
            {
                playerRow--;
                if (playerRow == -1)
                {
                    playerRow = startingRow;
                    playerCol = startingCol;
                }
                else if (matrix[playerRow, playerCol] == '#')
                {
                    playerRow++;
                    stars--;
                }

                break;
            }
        case "down":
            {
                playerRow++;


                if (playerRow == n)
                {
                    playerRow = startingRow;
                    playerCol = startingCol;
                }
                else if (matrix[playerRow, playerCol] == '#')
                {
                    playerRow--;
                    stars--;
                }
                break;
            }
    }
    if (matrix[playerRow, playerCol] == '*')
        stars++;




    matrix[playerRow, playerCol] = 'P';

    //PrintMatrix(matrix, ch => Console.Write(ch + " "));
}

if (stars == 10)
    Console.WriteLine("You won! You have collected 10 stars.");

else if (stars == 0)
    Console.WriteLine("Game over! You are out of any stars.");

Console.WriteLine($"Your final position is [{playerRow}, {playerCol}]");

PrintMatrix(matrix, ch => Console.Write(ch + " "));
void PrintMatrix<T>(T[,] matrix, Action<T> print)
{
    for (int row = 0; row < n; row++)
    {
        for (int col = 0; col < n; col++)
        {
            print(matrix[row, col]);
        }

        Console.WriteLine();
    }
}
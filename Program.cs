Random rand = new();
bool[,] isBomb;
char ch = 'A';
int numOfCells = 10;
MakeBoard(numOfCells);

// User input
Write("Enter a coordinate to check (i.e. A1): ");
string? coord = ReadLine();

// Parse input
try
{
    if (coord != null)
    {
        char letter = coord[0];
        if (char.IsLetter(letter) && char.IsUpper(letter))
        {
            WriteLine(coord.First());
        }

        coord = coord.Substring(1);
        int number = int.Parse(coord);
        if (number > numOfCells || number <= 0)
        {
            WriteLine("Please enter a valid value, i.e. A5");
        }
        else
        {
            WriteLine(number);
        }
    }
}
catch
{
    WriteLine("Please enter a valid value, i.e. A5");
}

/// <summary>
/// Makes a minesweeper board based on the number of cells provided.
/// </summary>
/// <param name="cells">Number of cells you wish to have for the horizontal/verticle axis.</param>
void MakeBoard(int cells)
{
    // Populate bombs - change value of 'cells' if you wish more or less bombs than default.
    bool[,] isBomb = PopulateBombs(cells);

    // Populate top line with alphabet
    Write("   ");
    for (int i = 0; i < cells; i++)
    {
        Write($"{ch} ");
        ch++;
    }
    WriteLine();

    // Populate board
    for (int i = 0; i < cells; i++)
    {
        Write($"{i + 1} ");
        for (int j = 0; j < cells; j++)
        {
            if (i < 9)
            {
                if (isBomb[i, j])
                {
                    Write(" B");
                }
                else
                {
                    Write(" *");
                }
            }
            else
            {
                if (isBomb[i, j])
                {
                    Write("B ");
                }
                else
                {
                    Write("* ");
                }
            }
        }
        WriteLine();
    }
}

/// <summary>
/// Populates a 2d boolean array with true being a bomb and false being safe.
/// </summary>
/// <param name="bombs">How many total bombs there are on the map.</param>
/// <returns>boolean 2d array representing whether a bomb is present or not.</returns>
bool[,] PopulateBombs(int bombs)
{
    isBomb = new bool[bombs, bombs];

    // Get random locations for bombs.
    int[] randNumbers = new int[bombs];
    for (int i = 0; i < bombs; i++)
    {
        int bomb = rand.Next(0, bombs * 10);
        // Check to make sure bomb location has not already been used.
        for (int j = 0; j < bombs; j++)
        {
            if (randNumbers[j] == bomb)
            {
                bomb = rand.Next(0, bombs * 10);
                j = 0;
            }
        }
        randNumbers[i] = bomb;
    }

    foreach (int num in randNumbers)
    {
        WriteLine($"Bomb at {num}.");
    }

    // Populate 2d array
    for (int i = 0; i < bombs; i++)
    {
        for (int j = 0; j < bombs; j++)
        {
            foreach (int num in randNumbers)
            {
                if (num - (i * 10) == j)
                {
                    isBomb[i, j] = true;
                    break;
                }
                else
                {
                    isBomb[i, j] = false;
                }
            }
        }
    }
    return isBomb;
}
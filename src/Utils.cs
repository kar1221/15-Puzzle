namespace _15_Puzzle;

public class Utils
{
    /// <summary>
    /// Return a 'Tiles' Object by providing the display number of the tile.
    /// </summary>
    /// <param name="tilesArray">The tiles array.</param>
    /// <param name="numberOnDisplay">Display number of the tile.</param>
    /// <returns></returns>
    public static Tiles? GetTileByNumber(Tiles[,] tilesArray, int numberOnDisplay)
    {

        var coordinate = GetTileCoordinate(tilesArray, numberOnDisplay);

        if (coordinate.Row == -1 || coordinate.Column == -1)
            return null;

        var firstDimension = coordinate.Row - 1;
        var secondDimension = coordinate.Column - 1;

        return tilesArray[firstDimension, secondDimension];
    }

    /// <summary>
    /// Return the coordinate of the tile by providing the tile's display number.
    /// </summary>
    /// <param name="tilesArray">The tiles array.</param>
    /// <param name="numberOnDisplay">Display number of the tile.</param>
    /// <returns></returns>
    public static Coordinate GetTileCoordinate(Tiles[,] tilesArray, int numberOnDisplay)
    {
        foreach (var tile in tilesArray)
            if (tile.NumberOnDisplay == numberOnDisplay)
                return tile.GetCurrentTileCoordinate();

        return new Coordinate(-1, -1);
    }

    /// <summary>
    /// Generate an array of shuffled number.
    /// </summary>
    /// <param name="minNumber">Lower bound (Contained)</param>
    /// <param name="maxNumber">Upper bound (Contained)</param>
    /// <returns>Shuffled array with upper bound of maxNumber and lower bound of minNumber</returns>
    public static int[] GetShuffledArray(int minNumber, int maxNumber)
    {
        var arrayLength = maxNumber - minNumber + 1;
        // If arrayLength less than 1, return an empty array.
        if (arrayLength < 1) return Array.Empty<int>();

        // Assign every element in array in ascending order.
        var array = new int[arrayLength];
        for (var i = minNumber; i < maxNumber + 1; i++)
            array[i] = i;

        // Shuffle it.
        var random = new Random();
        array = array.OrderBy(x => random.Next()).ToArray();

        return array;
    }

    public static int[] GetArrayFromRange(int minNumber, int maxNumber)
    {
        var arrayLength = maxNumber - minNumber + 1;
        if (arrayLength < 1) return Array.Empty<int>();

        var array = new int[arrayLength];

        for (var i = minNumber; i < maxNumber + 1; i++)
            array[i] = i;

        return array;
    }

    public static bool IsSolvable(int[] puzzle)
    {
        var parity = 0;
        var gridWidth = (int)Math.Sqrt(puzzle.Length);
        var row = 0; // the current row we are on
        var blankRow = 0; // the row with the blank tile

        for (var i = 0; i < puzzle.Length; i++)
        {
            if (i % gridWidth == 0)
            {
                // advance to next row
                row++;
            }

            if (puzzle[i] == 0)
            {
                // the blank tile
                blankRow = row; // save the row on which encountered
                continue;
            }

            for (var j = i + 1; j < puzzle.Length; j++)
            {
                if (puzzle[i] > puzzle[j] && puzzle[j] != 0)
                {
                    parity++;
                }
            }
        }

        if (gridWidth % 2 == 0)
        {
            // even grid
            if (blankRow % 2 == 0)
            {
                // blank on odd row; counting from bottom
                return parity % 2 == 0;
            }
            else
            {
                // blank on even row; counting from bottom
                return parity % 2 != 0;
            }
        }
        else
        {
            // odd grid
            return parity % 2 == 0;
        }
    }
}


public struct Coordinate
{
    public readonly int Row;
    public readonly int Column;

    public Coordinate(int row, int column)
    {
        Row = row;
        Column = column;
    }
}
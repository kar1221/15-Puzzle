namespace _15_Puzzle;

public class Board
{
    private readonly Tiles[,] _tilesArray = new Tiles[4, 4];
    private const int BoardSize = 4;
    private const int BlankTileNumber = 0;
    
    public Board()
    {
        var randomizedArray = Utils.GetShuffledArray(0, 15);
        while (true)
        {
            if (Utils.IsSolvable(randomizedArray))
            {
                InitializeTiles(randomizedArray);
                break;
            }
            randomizedArray = Utils.GetShuffledArray(0, 15);
        }
    }
    
    private void InitializeTiles(int[] randomizedArray)
    {
        var index = 0;

        for (var row = 0; row < BoardSize; row++)
        {
            for(var col = 0; col < BoardSize; col++)
            {
                if (index < randomizedArray.Length)
                {
                    _tilesArray[row, col] = new Tiles(row+1, col+1, randomizedArray[index]);
                    index++;
                }
                else
                {
                    return;
                }
            }
        }
    }
    
    /// <summary>
    /// Swap two tiles by providing the tile's display number.
    /// </summary>
    /// <param name="firstTileNumber">First tile's display number.</param>
    /// <param name="secondTileNumber">Second tile's display number.</param>
    public void SwapTwoTiles(int firstTileNumber, int secondTileNumber)
    {
        var firstTile = Utils.GetTileByNumber(_tilesArray, firstTileNumber);
        var secondTile = Utils.GetTileByNumber(_tilesArray, secondTileNumber);
    
        // Check if one of the tile is null
        if (firstTile == null || secondTile == null) return;
        
        // Swap the tile row and column
        var firstTileCoordinate = firstTile.GetCurrentTileCoordinate();
        var secondTileCoordinate = secondTile.GetCurrentTileCoordinate();
        
        firstTile.MoveTo(secondTileCoordinate.Row, secondTileCoordinate.Column);
        secondTile.MoveTo(firstTileCoordinate.Row, firstTileCoordinate.Column);
        
        // Swap the array afterwards.
        var firstArrayRow = firstTileCoordinate.Row - 1;
        var firstArrayColumn = firstTileCoordinate.Column - 1;
        var secondArrayRow = secondTileCoordinate.Row - 1;
        var secondArrayColumn = secondTileCoordinate.Column - 1;
        
        // Swap via Tuple.
        (_tilesArray[firstArrayRow, firstArrayColumn], _tilesArray[secondArrayRow, secondArrayColumn]) = 
            (_tilesArray[secondArrayRow, secondArrayColumn], _tilesArray[firstArrayRow, firstArrayColumn]);
    }
    
    /// <summary>
    /// Iterate through the array and mark the tile adjacent to '0'(Represent as blank tile) as movable.
    /// </summary>
    public void MarkMovable()
    {
        // Reset the movable variable of all tiles before marking other.
        foreach(var tile in _tilesArray)
            tile.Movable = false;
        
        var lengthRow = _tilesArray.GetLength(0);
        var lengthCols = _tilesArray.GetLength(1);
        
        // If i = 0, Row - 1, and Col does nothing.
        // If i = 1, Row + 1, and Col does nothing.
        // If i = 2, Row does nothing, and Col - 1.
        // If i = 3, Row does nothing, and Col + 1.
        int[] rowsOffset = { 1, -1, 0, 0 };
        int[] colsOffset = { 0, 0, -1, 1 };
        
        var blankTile = Utils.GetTileCoordinate(_tilesArray, BlankTileNumber);
        
        for (var i = 0; i < 4; i++)
        {
            var rowsArray = blankTile.Row + rowsOffset[i] - 1;
            var colsArray = blankTile.Column + colsOffset[i] - 1;
            
            if (rowsArray >= 0 && rowsArray < lengthRow && colsArray >= 0 && colsArray < lengthCols)
                _tilesArray[rowsArray, colsArray].Movable = true;
        }
    }
    
    // Generated using Chat-GPT, way too lazy to figure out how to make it looks good.
    public void ShowMatrix()
    {
        const int tileWidth = 4; // Adjust this value to control the spacing
        var horizontalLine = "+------------------+".PadRight(BoardSize * tileWidth, '-') + "+";
    
        Console.WriteLine(horizontalLine);

        for (var i = 0; i < BoardSize; i++)
        {
            for (var j = 0; j < BoardSize; j++)
            {
                Console.Write("|");
                var number = _tilesArray[i, j].NumberOnDisplay;
                var padding = (tileWidth - number.ToString().Length) / 2;
                Console.Write(new string(' ', padding) + number + new string(' ', tileWidth - padding - number.ToString().Length));
            }
            Console.WriteLine("|");
            Console.WriteLine(horizontalLine);
        }
    }

    public bool CheckIfFinished()
    {
        var finishedArray = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0 };
        var index = 0;
        for (var row = 0; row < 4; row++)
        {
            for (var column = 0; column < 4; column++)
            {
                if (_tilesArray[row, column].NumberOnDisplay != finishedArray[index])
                    return false;

                index++;
            }
        }

        return true;
    }
    
    public void MoveTile(int numberOnDisplay)
    {
        var targetTile = Utils.GetTileByNumber(_tilesArray, numberOnDisplay);
        
        if(targetTile!.Movable)
            SwapTwoTiles(targetTile.NumberOnDisplay, 0);
        else
            Console.WriteLine("This tile isn't movable.");
        
    }
    
    
}
namespace _15_Puzzle;

public class Tiles
{
    private int _column;
    private int _row;
    public int NumberOnDisplay { get; set; }
    public bool Movable { get; set; }

    public Tiles(int row, int column, int numberOnDisplay)
    {
        _row = row;
        _column = column;
        NumberOnDisplay = numberOnDisplay;
    }

    public Coordinate GetCurrentTileCoordinate() => new Coordinate(_row, _column);
    
    /// <summary>
    /// Move to specific row and column.
    /// </summary>
    public void MoveTo(int row, int column)
    {
        _column = column;
        _row = row;
    }

    
}
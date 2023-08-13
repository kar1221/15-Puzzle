using _15_Puzzle;

var board = new Board();
var finished = board.CheckIfFinished();

while (!finished)
{
    board.ShowMatrix();
    board.MarkMovable();
    Console.Write("Enter a number to move the tile: ");
    Int32.TryParse(Console.ReadLine(), out var number);
    Console.Clear();
    if (number > 0 && number < 16)
    {
        board.MoveTile(number);
    }
    finished = board.CheckIfFinished();
}

board.ShowMatrix();
Console.WriteLine("Great Job! You've finished the puzzle!");
Console.ReadKey(true);
using GameOfLife;

var board = new Board(10, 10);
var step = 0;

while (true)
{
    Console.WriteLine(board);
    Console.WriteLine($"Step {step++}");

    board.Next();

    Thread.Sleep(100);
    Console.Clear();
}
using System.Text;

namespace GameOfLife;

public class Field
{
    private List<Cell> _cells;

    public int Height { get; }

    public int Length { get; }

    public Field(int length, int height)
    {
        Length = length;
        Height = height;
        InitializeCells();
    }

    public override string ToString()
    {
        var str = new StringBuilder();

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Length; x++)
            {
                str.Append(_cells.Single(c => c.X == x && c.Y == y));
            }

            str.Append('\n');
        }

        return str.ToString();
    }

    public void NextGeneration()
    {
        var newGeneration = new List<Cell>();

        foreach (var cell in _cells)
        {
            var neighbors = GetNeighbors(cell);
            var willBeAlive = cell.IsAlive
                ? neighbors.Count(c => c.IsAlive) is 2 or 3
                : neighbors.Count(c => c.IsAlive) == 3;

            newGeneration.Add(new Cell(cell.X, cell.Y, willBeAlive));
        }

        _cells = newGeneration;
    }

    private IEnumerable<Cell> GetNeighbors(Cell cell)
    {
        var x = cell.X;
        var y = cell.Y;

        var neighborsCoordinates = new List<(int X, int Y)>
        {
            (x - 1, y + 1),
            (x, y + 1),
            (x + 1, y + 1),
            (x - 1, y),
            (x + 1, y),
            (x - 1, y - 1),
            (x, y - 1),
            (x + 1, y - 1)
        }.Where(coordinates => coordinates.X >= 0
                               && coordinates.X < Length
                               && coordinates.Y >= 0
                               && coordinates.Y < Height);

        var neighbors = _cells.Where(c => neighborsCoordinates.Contains((c.X, c.Y)));

        return neighbors;
    }

    private void InitializeCells()
    {
        _cells = new List<Cell>();
        for (var x = 0; x < Length; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                _cells.Add(new Cell(x, y));
            }
        }

        foreach (var cell in _cells.Where(c => c.Y == 2 && c.X is 1 or 2 or 3))
        {
            cell.IsAlive = true;
        }
    }
}
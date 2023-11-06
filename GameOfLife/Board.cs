using System.Text;

namespace GameOfLife;

public record Cell(int Row, int Column)
{
    public bool IsAlive { get; set; }

    public override string ToString() => IsAlive ? " * " : "   ";
}

public class Board
{
    private readonly Dictionary<(int Row, int Column), Cell> _cells = new();

    private readonly int _rows;
    private readonly int _columns;

    public Board(int rows, int columns, bool fillAliveCellsRandomly = true)
    {
        _rows = rows;
        _columns = columns;
        
        for (var row = 0; row < _rows; row++)
        {
            for (var col = 0; col < _columns; col++)
            {
                var cell = new Cell(row, col);
                _cells.Add((cell.Row, cell.Column), cell);
            }
        }

        if (fillAliveCellsRandomly)
        {
            var chance = Random.Shared.Next(2, 7);
            foreach (var cell in _cells.Values)
                cell.IsAlive = Random.Shared.Next() % chance is 0;
        }
    }

    public void Next()
    {
        foreach (var cell in _cells.Values)
        {
            var aliveNeighboursCount = GetNeighboursCoordinates(cell)
                .Select(coordinates => _cells[coordinates])
                .Count(c => c.IsAlive);

            cell.IsAlive = aliveNeighboursCount switch
            {
                3 => true,
                2 when cell.IsAlive => true,
                _ => false
            };
        }

        return;
        
        IEnumerable<(int Row, int Column)> GetNeighboursCoordinates(Cell cell)
        {
            return GetCoordinates()
                .Where(IsValid)
                .ToArray();

            IEnumerable<(int Row, int Column)> GetCoordinates()
            {
                var (r, c) = cell;

                yield return (r - 1, c - 1);
                yield return (r, c - 1);
                yield return (r + 1, c - 1);
                yield return (r - 1, c);
                yield return (r + 1, c);
                yield return (r - 1, c + 1);
                yield return (r, c + 1);
                yield return (r + 1, c + 1);
            }

            bool IsValid((int Row, int Column) rc) =>
                rc.Row < _rows &&
                rc.Row >= 0 &&
                rc.Column < _columns &&
                rc.Column >= 0;
        }
    }

    public override string ToString()
    {
        var str = new StringBuilder();

        for (var row = 0; row < _rows; row++)
        {
            for (var col = 0; col < _columns; col++)
                str.Append(_cells[(row, col)]);

            str.AppendLine();
        }

        return str.ToString();
    }
}
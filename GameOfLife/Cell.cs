namespace GameOfLife;

public class Cell
{
    public int X { get; }

    public int Y { get; }

    public bool IsAlive { get; set; }

    public Cell(int x, int y, bool isAlive = false)
    {
        X = x;
        Y = y;
        IsAlive = isAlive;  
    }

    public override string ToString() => IsAlive ? "[*]" : "[ ]";
}
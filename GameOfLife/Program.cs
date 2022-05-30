using GameOfLife;

var field = new Field(12, 12);

while (true)
{
    Console.Write(field);
    field.NextGeneration();
    Thread.Sleep(1000);
    Console.Clear();
}
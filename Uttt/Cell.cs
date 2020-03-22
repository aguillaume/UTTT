public class Cell
{
    public int Row { get; set; }
    public int Col { get; set; }
    public Actor Value { get; set; }

    public Cell() { }

    public Cell(int row, int col)
    {
        Row = row;
        Col = col;
        Value = Actor.None;
    }

    public Cell(string cellPosStr)
    {
        var input = cellPosStr.Split(' ');
        Row = int.Parse(input[0]);
        Col = int.Parse(input[1]);
        Value = Actor.None;
    }

    public Cell(Cell cell)
    {
        Row = cell.Row;
        Col = cell.Col;
        Value = cell.Value;
    }

    public override string ToString()
    {
        return $"{Row} {Col}";
    }
}

namespace SuperBowlSquares.Models;

public class GameState
{
    public string?[] Squares { get; set; } = new string?[100];
    public int[] RowNumbers { get; set; } = Enumerable.Repeat(-1, 10).ToArray();
    public int[] ColNumbers { get; set; } = Enumerable.Repeat(-1, 10).ToArray();
    public bool NumbersAssigned { get; set; } = false;

    public int ClaimedCount => Squares.Count(s => s != null);
    public int PlayerCount => Squares.Where(s => s != null).Distinct().Count();
}

public class SquareClaim
{
    public int Index { get; set; }
    public string Name { get; set; } = string.Empty;
}

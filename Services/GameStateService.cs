using SuperBowlSquares.Models;

namespace SuperBowlSquares.Services;

public class GameStateService
{
    private readonly GameState _gameState = new();
    private readonly object _lock = new();

    public GameState GetState()
    {
        lock (_lock)
        {
            return _gameState;
        }
    }

    public bool ClaimSquare(int index, string name)
    {
        lock (_lock)
        {
            if (index < 0 || index >= 100 || _gameState.Squares[index] != null)
                return false;

            _gameState.Squares[index] = name;
            return true;
        }
    }

    public void RandomizeNumbers()
    {
        lock (_lock)
        {
            var random = new Random();
            _gameState.RowNumbers = Enumerable.Range(0, 10).OrderBy(_ => random.Next()).ToArray();
            _gameState.ColNumbers = Enumerable.Range(0, 10).OrderBy(_ => random.Next()).ToArray();
            _gameState.NumbersAssigned = true;
        }
    }

    public void ResetBoard()
    {
        lock (_lock)
        {
            _gameState.Squares = new string?[100];
            _gameState.RowNumbers = Enumerable.Repeat(-1, 10).ToArray();
            _gameState.ColNumbers = Enumerable.Repeat(-1, 10).ToArray();
            _gameState.NumbersAssigned = false;
        }
    }
}

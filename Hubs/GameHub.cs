using Microsoft.AspNetCore.SignalR;
using SuperBowlSquares.Services;
using SuperBowlSquares.Models;

namespace SuperBowlSquares.Hubs;

public class GameHub : Hub
{
    private readonly GameStateService _gameStateService;

    public GameHub(GameStateService gameStateService)
    {
        _gameStateService = gameStateService;
    }

    public async Task GetGameState()
    {
        await Clients.Caller.SendAsync("ReceiveGameState", _gameStateService.GetState());
    }

    public async Task ClaimSquare(SquareClaim claim)
    {
        if (_gameStateService.ClaimSquare(claim.Index, claim.Name))
        {
            await Clients.All.SendAsync("ReceiveGameState", _gameStateService.GetState());
        }
    }

    public async Task RandomizeNumbers()
    {
        _gameStateService.RandomizeNumbers();
        await Clients.All.SendAsync("ReceiveGameState", _gameStateService.GetState());
    }

    public async Task ResetBoard()
    {
        _gameStateService.ResetBoard();
        await Clients.All.SendAsync("ReceiveGameState", _gameStateService.GetState());
    }

    public override async Task OnConnectedAsync()
    {
        await GetGameState();
        await base.OnConnectedAsync();
    }
}

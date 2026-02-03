# Super Bowl Squares - Blazor Edition 🏈

A real-time Super Bowl Squares game built with **Blazor Server** and **SignalR** for automatic synchronization across all users!

## Why Blazor?

- ✅ **Built-in real-time with SignalR** - No extra libraries needed
- ✅ **Strongly typed C#** - Better intellisense and fewer runtime errors
- ✅ **Automatic state management** - Blazor handles UI updates
- ✅ **Full .NET ecosystem** - Easy to add databases, auth, etc.
- ✅ **Great performance** - Compiled C# runs fast

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later

## Running Locally

1. Navigate to the project directory:
```bash
cd superbowl-blazor
```

2. Restore packages:
```bash
dotnet restore
```

3. Run the application:
```bash
dotnet run
```

4. Open your browser to the URL shown (usually `https://localhost:5001` or `http://localhost:5000`)

## Deployment Options

### Option 1: Azure App Service (EASIEST - Microsoft's Cloud)

**Free tier available!**

#### Using Visual Studio:
1. Right-click the project → **Publish**
2. Choose **Azure** → **Azure App Service (Windows)**
3. Create new or select existing App Service
4. Click **Publish**

#### Using Azure CLI:
```bash
# Login to Azure
az login

# Create resource group
az group create --name SuperBowlSquares --location eastus

# Create app service plan (Free tier)
az appservice plan create --name SuperBowlPlan --resource-group SuperBowlSquares --sku F1

# Create web app
az webapp create --name your-unique-app-name --resource-group SuperBowlSquares --plan SuperBowlPlan --runtime "DOTNET|8.0"

# Deploy
dotnet publish -c Release
cd bin/Release/net8.0/publish
zip -r deploy.zip .
az webapp deployment source config-zip --resource-group SuperBowlSquares --name your-unique-app-name --src deploy.zip
```

Your app will be live at: `https://your-unique-app-name.azurewebsites.net`

### Option 2: Azure Static Web Apps with Blazor Server
```bash
# Install SWA CLI
npm install -g @azure/static-web-apps-cli

# Build and deploy
dotnet publish -c Release
swa deploy ./bin/Release/net8.0/publish/wwwroot
```

### Option 3: Docker (Works anywhere!)

Create a `Dockerfile`:
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SuperBowlSquares.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "SuperBowlSquares.dll"]
```

Then:
```bash
docker build -t superbowl-squares .
docker run -p 8080:80 superbowl-squares
```

Deploy to any container service (Azure Container Apps, AWS ECS, Google Cloud Run)

### Option 4: Railway (Simple & Free)

1. Install [Railway CLI](https://docs.railway.app/develop/cli)
2. Run:
```bash
railway login
railway init
railway up
```

### Option 5: Traditional Hosting (IIS, etc.)

1. Publish the app:
```bash
dotnet publish -c Release
```

2. Copy contents of `bin/Release/net8.0/publish` to your web server

3. Configure IIS or your web server to run the ASP.NET Core app

## Architecture

```
├── Models/           # Data models (GameState, SquareClaim)
├── Services/         # Business logic (GameStateService)
├── Hubs/            # SignalR hub for real-time communication
├── Components/
│   ├── Pages/       # Blazor pages (Home.razor)
│   └── App.razor    # Root component
└── wwwroot/         # Static files (CSS)
```

## How It Works

1. **SignalR Hub** (`GameHub.cs`) manages all real-time communication
2. **GameStateService** maintains the game state (thread-safe)
3. **Blazor Server** automatically syncs UI across all connected clients
4. When anyone claims a square, everyone sees it instantly via SignalR

## Features

- 🔄 **Real-time synchronization** - All users see updates instantly
- 📱 **Responsive design** - Works on mobile, tablet, desktop
- 🎲 **Random number assignment** - Fair distribution of numbers
- 📊 **Live statistics** - Track claimed squares and players
- 🔌 **Connection status** - See if you're connected
- ⚡ **Fast** - Blazor Server is very performant

## Customization

### Change Team Names
Edit `Components/Pages/Home.razor`, line with `<div class="cell header corner">AFC</div>`

### Add Database Persistence
The current version stores state in memory (resets on restart). To add a database:

1. Install Entity Framework:
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

2. Create a DbContext and update `GameStateService` to use it
3. Add connection string to `appsettings.json`

### Add Authentication
```bash
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

Then follow [ASP.NET Core Identity docs](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity)

## Troubleshooting

**Port already in use?**
```bash
dotnet run --urls "http://localhost:5001"
```

**SignalR not connecting?**
- Check browser console for errors
- Ensure WebSockets are enabled on your hosting platform
- Azure requires "Web Sockets" enabled in Configuration

**State resets on refresh?**
- This is normal for in-memory storage
- Add a database for persistence

## Why Choose Blazor over Node.js?

| Feature | Blazor | Node.js |
|---------|--------|---------|
| Language | C# | JavaScript |
| Type Safety | Strong, compile-time | Weak, runtime |
| Real-time | SignalR built-in | Needs Socket.io |
| Performance | Compiled, very fast | Interpreted |
| Deployment | Azure, IIS native | More options but more setup |
| Learning curve | Medium (if you know C#) | Easy (if you know JS) |

Both work great! Choose based on your team's expertise.

## License

Free to use and modify!

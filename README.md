# Dart Score Keeper

A cross-platform .NET MAUI dart scorekeeper application for 1–4 players.

## Features

- **Interactive Dartboard**: Tap where each dart lands on an interactive dartboard for automatic score calculation
- **Multiple Game Modes**:
  - **501**: Classic countdown game starting from 501 points
  - **Cricket**: Strategic game focusing on numbers 15-20 and bulls
  - **Around the Clock**: Practice game hitting numbers 1-20 in order
  - **Killer**: Competitive elimination game
- **Multi-Player Support**: Play with 1 to 4 players
- **Score Tracking**: Automatic score calculation and turn management
- **Statistics**: Track darts thrown and average scores per player

## How to Play

1. Select the number of players (1-4)
2. Enter player names
3. Choose a game type
4. Tap dartboard sections or use quick score buttons to register dart hits
5. The app automatically calculates scores and manages turns

## Game Rules

### 501
- Players start with 501 points
- Score is subtracted from total with each dart
- First player to reach exactly 0 wins
- Cannot finish on a score of 1 or go below 0

### Cricket
- Hit numbers 15-20 and bulls (25)
- Need 3 hits to "close" a number
- Extra hits score points if opponents haven't closed that number
- Win by closing all numbers with highest score

### Around the Clock
- Hit numbers 1-20 in sequential order
- First player to hit all numbers wins
- Great for practice and accuracy

### Killer
- Each player establishes their "killer number" with a double
- Hit doubles of opponent's numbers to eliminate their lives
- Each player starts with 3 lives
- Last player standing wins

## Technical Details

- Built with .NET MAUI for cross-platform compatibility
- Targets: Android, iOS, MacCatalyst, and Windows
- Uses MVVM pattern with CommunityToolkit.Mvvm
- Responsive UI adapts to different screen sizes

## Requirements

- .NET 8.0 SDK or higher
- Visual Studio 2022 or Visual Studio Code with .NET MAUI workload

## Building

```bash
dotnet build DartScoreKeeper.sln
```

## Running

```bash
dotnet run --project DartScoreKeeper/DartScoreKeeper.csproj
```


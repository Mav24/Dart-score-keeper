# Dart Score Keeper - Architecture and Design

## Overview

Dart Score Keeper is a cross-platform mobile and desktop application built with .NET MAUI that allows 1-4 players to track scores for various dart games. The app features an interactive dartboard where players tap to register dart hits, with automatic score calculation and turn management.

## Architecture

### MVVM Pattern

The application follows the Model-View-ViewModel (MVVM) architectural pattern:

- **Models**: Business logic and data structures
- **Views**: XAML-based UI pages
- **ViewModels**: Bridge between Models and Views, handling user interactions and state management

### Technology Stack

- **.NET MAUI**: Cross-platform UI framework
- **CommunityToolkit.Mvvm**: Simplifies MVVM implementation with source generators
- **.NET 8.0**: Target framework
- **C# 12**: Programming language

## Core Components

### Models

#### Player.cs
Represents a player in the game:
- Name
- Current score
- Darts thrown count
- Turn scores history
- Cricket scores (for Cricket game mode)
- Average calculation

#### Game.cs (Abstract Base Class)
Defines the common interface for all game types:
- List of players
- Current player tracking
- Current dart in turn (1-3)
- Abstract methods for processing dart scores and checking win conditions
- Turn management logic

#### Game-Specific Classes

**Game501.cs**
- Starting score: 501 points
- Scoring: Subtract dart values from total
- Win condition: Reach exactly 0
- Validation: Cannot finish on 1 or go below 0
- Bust rule: Invalid turns don't count

**CricketGame.cs**
- Target numbers: 15-20 and Bulls (25)
- Scoring: Hit each number 3 times to "close" it
- Bonus points: Extra hits score points if opponents haven't closed
- Win condition: Close all numbers with highest or tied score

**AroundTheClockGame.cs**
- Objective: Hit numbers 1-20 in sequential order
- Scoring: Track current target number
- Win condition: First player to hit all 20 numbers
- Practice-focused game mode

**KillerGame.cs**
- Setup phase: Each player establishes "killer number" with a double
- Lives system: Each player starts with 3 lives
- Attack mechanism: Hit doubles of opponent's numbers
- Win condition: Last player with lives remaining

#### DartScore.cs
Represents a single dart throw:
- Number (1-20 or 25 for bulls)
- Multiplier (1=single, 2=double, 3=triple)
- Total score calculation
- Display formatting (e.g., "T20", "D16")

### ViewModels

#### MainViewModel.cs
Handles the main menu and game setup:
- Player count selection (1-4)
- Player name entry
- Game type selection
- Validation before starting game
- Navigation to game page

**Key Features:**
- Observable properties for data binding
- Commands for button interactions
- Input validation
- Navigation with parameters

#### GameViewModel.cs
Manages active game state and interactions:
- Current game instance
- Player collection (observable)
- Current player display
- Dart count display
- Turn score tracking
- Dartboard hit processing
- Win condition checking

**Key Features:**
- Query attributes for receiving navigation parameters
- Game initialization based on selected type
- Score processing and validation
- Turn management
- Game over detection and notification

### Views

#### MainPage.xaml/cs
Game setup interface:
- Player count selector (+/- buttons)
- Player name entry fields
- Game type selection buttons
- Clean, intuitive layout

**UI Components:**
- Vertical stack layout for organization
- Entry fields for player names
- Buttons for game type selection
- Responsive design

#### GamePage.xaml/cs
Active game interface with three main sections:

**1. Score Display Area (Top)**
- Current player name and highlight
- Dart counter (1 of 3, 2 of 3, 3 of 3)
- Player scores list
- Current turn scores display

**2. Interactive Dartboard (Center)**
- Touch-enabled dartboard graphic
- Tap gesture recognizer
- Position-to-score calculation
- Visual feedback

**3. Quick Score Buttons (Bottom)**
- Horizontal scrollable list
- All possible scores (S1-S20, D1-D20, T1-T20)
- Special buttons (MISS, BULL, OUTER)
- Back button

**Dartboard Interaction Logic:**
- Tap position detection
- Distance and angle calculation
- Sector determination (20 sectors)
- Ring identification (outer single, triple, inner single, double, bull)
- Score generation based on position

### Navigation

Uses Shell navigation:
- AppShell defines navigation structure
- Route registration for pages
- Parameter passing between pages
- Back navigation support

## Game Flow

### 1. Game Setup
1. User selects number of players
2. User enters player names
3. User selects game type
4. Validation ensures at least one player name
5. Navigation to game page with parameters

### 2. Game Initialization
1. GameViewModel receives players and game type
2. Appropriate game instance created
3. Players initialized with starting values
4. First player becomes current player

### 3. Gameplay Loop
1. Display current player and dart count
2. Player taps dartboard or score button
3. System calculates score
4. Game processes dart score according to rules
5. Update player scores and statistics
6. Check win condition
7. Advance to next dart or next player
8. Repeat until game over

### 4. Game Completion
1. Win condition detected
2. Display winner alert
3. Game marked as over
4. Option to return to main menu

## Score Calculation

### Dartboard Position Algorithm

```
1. Get tap coordinates relative to dartboard
2. Calculate distance from center
3. Calculate angle from top (0°)
4. Determine sector based on angle:
   - 20 sectors of 18° each
   - Numbers arranged clockwise: 20,1,18,4,13,6,10,15,2,17,3,19,7,16,8,11,14,9,12,5
5. Determine ring based on distance:
   - Center < 5%: Double Bull (50 points)
   - 5-15%: Single Bull (25 points)
   - 15-40%: Single (inner)
   - 40-50%: Triple (3x)
   - 50-85%: Single (outer)
   - 85-95%: Double (2x)
   - > 95%: Miss (0 points)
```

### Alternative Input Methods

- Quick score buttons provide precise input
- Useful for beginners or when accuracy needed
- Bypasses position calculation
- Direct DartScore object creation

## Data Flow

```
User Interaction
    ↓
View (XAML)
    ↓
ViewModel (Command/Property)
    ↓
Model (Game Logic)
    ↓
Game State Update
    ↓
PropertyChanged Notification
    ↓
UI Update (Data Binding)
```

## Key Design Decisions

### 1. Abstract Game Base Class
- Enables polymorphism for different game types
- Shared turn management logic
- Extensible for new game modes

### 2. Observable Collections
- Automatic UI updates on data changes
- Eliminates manual refresh logic
- Follows MVVM best practices

### 3. Interactive Dartboard
- Provides realistic dart-playing experience
- Touch-based interaction feels natural
- Quick buttons offer alternative for precision

### 4. Platform Abstraction
- Single codebase for all platforms
- Platform-specific code isolated in Platforms folder
- Consistent user experience across devices

### 5. Minimal Dependencies
- CommunityToolkit.Mvvm for MVVM helpers
- Microsoft.Extensions.Logging for debugging
- Standard .NET MAUI packages

## Extension Points

### Adding New Game Modes

1. Create new class inheriting from `Game`
2. Implement `ProcessDartScore()` method
3. Implement `CheckWinCondition()` method
4. Add to `GameType` enum
5. Update `GameViewModel` initialization
6. Add button in `MainPage`

### Enhancing Statistics

1. Add properties to `Player` model
2. Update score processing in game classes
3. Create new view/viewmodel for statistics
4. Add navigation to statistics page

### Improving Dartboard Graphics

1. Add custom drawing in `GamePage`
2. Use `Microsoft.Maui.Graphics` for rendering
3. Draw sectors, rings, numbers
4. Add visual feedback for hits

## Performance Considerations

- Gesture recognizers are lightweight
- Observable collections minimize unnecessary updates
- Game logic optimized for turn-based play
- No continuous animations or heavy processing

## Testing Strategy

### Unit Testing Focus Areas
- Game logic (scoring rules)
- Win condition checking
- Turn management
- Score calculations

### Integration Testing
- Navigation flows
- Data passing between pages
- UI updates from ViewModel changes

### Manual Testing
- Touch interaction responsiveness
- Multi-player scenarios
- All game modes
- Edge cases (bust in 501, ties, etc.)

## Future Enhancements

Potential features for future versions:
- Save/load game state
- Game history and statistics
- Online multiplayer
- Custom game rules
- Sound effects and animations
- Practice mode with AI opponents
- Tournament bracket system
- Photo finish replays
- Undo last dart
- Customizable dartboard themes

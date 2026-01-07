# Setup Instructions for Dart Score Keeper

This guide will help you set up the development environment and build the Dart Score Keeper .NET MAUI application.

## Prerequisites

### Required Software

1. **Visual Studio 2022** (version 17.8 or later) or **Visual Studio Code** with appropriate extensions
2. **.NET 10.0 SDK** or later
3. **.NET MAUI workload**

### Installation Steps

#### Option 1: Visual Studio 2022 (Recommended)

1. Download and install [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
2. During installation, select the ".NET Multi-platform App UI development" workload
3. This will automatically install:
   - .NET 10.0 SDK
   - .NET MAUI workload
   - Android SDK
   - iOS/MacCatalyst build tools (on macOS)
   - Windows App SDK (on Windows)

#### Option 2: Visual Studio Code

1. Install [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
2. Install .NET MAUI workload:
   ```bash
   dotnet workload install maui
   ```
3. Install Visual Studio Code extensions:
   - C# Dev Kit
   - .NET MAUI

### Platform-Specific Requirements

#### For Android Development
- Android SDK 21 or higher
- Android emulator or physical device
- Enabled USB debugging (for physical devices)

#### For iOS Development (macOS only)
- Xcode 14 or later
- iOS 11.0 or higher
- Valid Apple Developer account (for device deployment)

#### For Windows Development
- Windows 10 version 1809 or higher
- Windows App SDK

## Building the Project

### Using Visual Studio 2022

1. Open `DartScoreKeeper.sln`
2. Select your target platform from the dropdown (Android, iOS, Windows, MacCatalyst)
3. Press F5 or click the Start button to build and run

### Using Command Line

1. Restore dependencies:
   ```bash
   dotnet restore DartScoreKeeper.sln
   ```

2. Build the project:
   ```bash
   dotnet build DartScoreKeeper.sln
   ```

3. Run on specific platform:
   ```bash
   # For Android
   dotnet build -f net10.0-android -t:Run
   
   # For iOS (macOS only)
   dotnet build -f net10.0-ios -t:Run
   
   # For MacCatalyst (macOS only)
   dotnet build -f net10.0-maccatalyst -t:Run
   
   # For Windows
   dotnet build -f net10.0-windows10.0.19041.0 -t:Run
   ```

## Running on Emulators/Simulators

### Android Emulator

1. Open Android Device Manager in Visual Studio
2. Create or start an Android emulator
3. Select the emulator as your deployment target
4. Run the application

### iOS Simulator (macOS only)

1. Select an iOS Simulator from the device list
2. Run the application
3. The simulator will launch automatically

### Windows

1. Select "Windows Machine" as the target
2. Run the application

## Troubleshooting

### Build Errors

**Error: NETSDK1147 - Workload not installed**
- Solution: Run `dotnet workload install maui` or reinstall Visual Studio with MAUI workload

**Error: Android SDK not found**
- Solution: Install Android SDK through Visual Studio Installer or Android Studio

**Error: Unable to find iOS SDK**
- Solution: Install Xcode from Mac App Store and accept license agreements

### Runtime Errors

**Application crashes on startup**
- Check that all dependencies are installed
- Verify target platform requirements are met
- Check Output window for detailed error messages

**Dartboard not responding to taps**
- Ensure device/emulator supports touch input
- Check that gesture recognizers are properly initialized

## Project Structure

```
DartScoreKeeper/
├── DartScoreKeeper.sln          # Solution file
├── DartScoreKeeper/
│   ├── Models/                   # Game logic and data models
│   │   ├── Player.cs
│   │   ├── Game.cs
│   │   ├── Game501.cs
│   │   ├── CricketGame.cs
│   │   ├── AroundTheClockGame.cs
│   │   ├── KillerGame.cs
│   │   └── DartScore.cs
│   ├── ViewModels/               # MVVM ViewModels
│   │   ├── MainViewModel.cs
│   │   └── GameViewModel.cs
│   ├── Views/                    # UI Pages
│   │   ├── MainPage.xaml
│   │   └── GamePage.xaml
│   ├── Platforms/                # Platform-specific code
│   │   ├── Android/
│   │   ├── iOS/
│   │   ├── MacCatalyst/
│   │   └── Windows/
│   └── Resources/                # Images, fonts, styles
└── README.md
```

## Development Tips

1. **Hot Reload**: Use XAML Hot Reload to see UI changes without rebuilding
2. **Debugging**: Set breakpoints in C# code to debug game logic
3. **Testing**: Test on actual devices for the best touch experience
4. **Performance**: Profile the app to ensure smooth dartboard interactions

## Support

For issues or questions:
- Check the [README.md](README.md) for usage instructions
- Review error messages in the Output window
- Ensure all prerequisites are installed
- Verify SDK and workload versions match requirements

## Next Steps

After successful setup:
1. Run the application
2. Try different game modes
3. Test with multiple players
4. Customize the UI or add new features

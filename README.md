# KeyOverlay

[![.NET](https://img.shields.io/badge/.NET-7.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/7.0)
[![Platform](https://img.shields.io/badge/platform-Windows-0078D4?logo=windows)](https://www.microsoft.com/windows)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)
[![Stars](https://img.shields.io/github/stars/Wolren/KeyOverlay?style=flat&logo=github)](https://github.com/Wolren/KeyOverlay)

Real-time keyboard and mouse input visualization overlay for Windows. Displays key presses, clicks, and CPS (clicks per second) on top of other applications. Built with WPF and .NET 7 using the MVVM pattern.

## Features

- **Keyboard Overlay** -- full 104-key layout with visual press feedback and CPS counter
- **Mouse Overlay** -- tracks left/right clicks, scroll wheel, with CPS and SPS counters
- **Single Key Overlay** -- customizable slot-based key tracking with CPS counter
- **Always-on-top** windows that stay visible over fullscreen applications
- **Frameless** transparent window with drag-to-move and minimize/close controls
- **Settings panel** for configuration

## Screenshots

| Keyboard Overlay | Mouse Overlay | Key Overlay |
|---|---|---|
| ![Keyboard Overlay](https://github.com/Wolren/KeyOverlay/assets/86470291/c13faf59-c73d-4465-9f1e-7c37ff979f1a) | ![Mouse Overlay](https://github.com/Wolren/KeyOverlay/assets/86470291/8b0112c1-4f65-485f-85fc-0352b5b03dbf) | ![Key Overlay](https://github.com/Wolren/KeyOverlay/assets/86470291/bcca2ea8-b65d-43e4-91e3-99a72f9a1a54) |

## Getting Started

### Prerequisites

- Windows 10/11
- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)

### Build & Run

```sh
git clone https://github.com/Wolren/KeyOverlay.git
cd KeyOverlay
dotnet build
dotnet run --project KeyOverlay
```

## Architecture

- **MVVM** pattern with `BaseViewModel` implementing `INotifyPropertyChanged`
- **Windows API** low-level keyboard/mouse hooks (`SetWindowsHookEx`) for global input capture
- **View-first navigation** via `UpdateViewCommand` with `DataTemplate` bindings in `App.xaml`

```
KeyOverlay/
  Overlays/       -- Window overlays (Keyboard, Mouse, SingleKey)
  ViewModels/     -- MVVM view models (Main, Keyboard, Mouse, SingleKey, Settings)
  Views/          -- UserControl views for each view model
  Commands/       -- ICommand implementations
  Assets/         -- Application icon
```

## Download

Pre-built binaries are available on the [Releases](https://github.com/Wolren/KeyOverlay/releases) page.

## Built With

- C# / WPF (.NET 7, Windows)
- Microsoft.Xaml.Behaviors.Wpf
- Windows API (low-level keyboard/mouse hooks)

## Author

- [@Wolren](https://github.com/Wolren)

## License

This project is licensed under the MIT License -- see the [LICENSE](LICENSE) file for details.

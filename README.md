# Font-Test

A MonoGame application built in Visual Basic .NET for testing dialog systems and custom sprite fonts. This project demonstrates interactive text rendering, font scaling, and dialog box functionality.

## 🎯 Features

- **Interactive Dialog System**: Dynamic text wrapping and display in customizable dialog boxes
- **Real-time Font Scaling**: Adjust font size on-the-fly with keyboard controls and UI buttons
- **Multiple Font Support**: Test different sprite fonts including Georgia, Arial, Semaru, and custom monotype fonts
- **Text Wrapping**: Intelligent word wrapping that respects dialog box boundaries
- **UI Controls**: Interactive buttons for font manipulation
- **Visual Feedback**: Animated dialog advance indicators and hover effects

## 🎮 Controls

- **Enter**: Advance dialog text
- **Up Arrow**: Increase font size
- **Down Arrow**: Decrease font size
- **Escape**: Exit application
- **F1**: Toggle fullscreen
- **Font + Button**: Increase font size (UI control)
- **Font - Button**: Decrease font size (UI control)

## 🚀 Getting Started

### Prerequisites

- Visual Studio with VB.NET support
- MonoGame Framework
- .NET Framework

### Installation

1. Clone this repository:
   ```bash
   git clone https://github.com/MrValentine7777/Font-Test.git
   ```

2. Open the project in Visual Studio

3. Restore NuGet packages if needed

4. Build and run the project

## 📁 Project Structure

```
Font-Test/
├── Core/
│   ├── GameCore.vb           # Main game loop and management
│   ├── Screens/
│   │   └── StartScreen.vb    # Main test screen with dialog
│   └── UI/
│       ├── Dialog.vb         # Dialog system implementation
│       └── Button.vb         # Interactive button component
├── Globals/
│   ├── Globals.vb           # Global game state and utilities
│   ├── Fonts.vb             # Font loading and management
│   ├── Textures.vb          # Texture loading and management
│   └── Sounds.vb            # Sound management (placeholder)
├── Game1.vb                 # Main game class
└── Program.vb               # Application entry point
```

## 🎨 Dialog System Features

The dialog system includes several advanced features:

- **Dynamic Text Wrapping**: Automatically wraps text to fit within dialog boundaries
- **Font Scaling**: Real-time font size adjustment with boundary detection
- **Multi-line Display**: Handles multiple lines with proper spacing
- **Visual Indicators**: Animated icons show when more text is available
- **Responsive Layout**: Dialog boxes adapt to content and screen size

## 🔧 Font Configuration

The project supports multiple sprite fonts:

- **Georgia_16**: Standard serif font for UI elements
- **Arial_8**: Small sans-serif font for compact text
- **Semaru**: Custom font option
- **ScaleTest**: Font specifically for scaling tests
- **MonoType**: Monospace font with custom spacing settings

## 🎯 Use Cases

This project is ideal for:

- Testing sprite font rendering in MonoGame
- Prototyping dialog systems for games
- Experimenting with text layout algorithms
- Learning MonoGame UI development
- Font scaling and readability testing

## 🤝 Contributing

Feel free to fork this project and submit pull requests for improvements or additional features.

## 📝 License

This project is a fork of [Aardaerimus/Font-Test](https://github.com/Aardaerimus/Font-Test).

## 🙏 Acknowledgments

- Original project by Aardaerimus
- Built with MonoGame framework
- Visual Basic .NET implementation

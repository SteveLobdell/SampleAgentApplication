# SampleAgentApplication - Pacman Game

A .NET 8.0 console-based Pacman game implementation.

## Features

- Classic Pacman gameplay in the console
- Maze navigation with wall collision detection
- Dot collection with score tracking
- AI-controlled ghosts with simple movement patterns
- Keyboard controls (WASD or Arrow Keys)
- Win/lose conditions
- Console-based graphics with colors

## How to Run

1. Navigate to the PacmanGame directory:
   ```bash
   cd PacmanGame
   ```

2. Build and run the game:
   ```bash
   dotnet run
   ```

## Controls

- **W** or **↑**: Move Up
- **A** or **←**: Move Left  
- **S** or **↓**: Move Down
- **D** or **→**: Move Right
- **Q**: Quit Game

## Objective

- Collect all the dots (yellow `.`) to win
- Avoid the ghosts (red `M`) or you lose
- Try to achieve the highest score possible!

## Game Elements

- `C` - Pacman (you)
- `M` - Ghosts (enemies)
- `.` - Dots to collect (10 points each)
- `#` - Walls (blue)
- ` ` - Empty space

## Requirements

- .NET 8.0 SDK or later
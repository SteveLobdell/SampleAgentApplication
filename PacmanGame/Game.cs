namespace PacmanGame;

public enum GameState
{
    Playing,
    GameOver,
    Victory
}

public class Game
{
    private readonly GameBoard _board;
    private readonly Pacman _pacman;
    private readonly List<Ghost> _ghosts;
    private GameState _state = GameState.Playing;
    private DateTime _lastUpdate = DateTime.Now;
    private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(300); // Game speed
    
    public Game()
    {
        _board = new GameBoard();
        _pacman = new Pacman(5, 9); // Start in center-ish position
        _ghosts = new List<Ghost>
        {
            new Ghost(5, 8),
            new Ghost(5, 10),
            new Ghost(4, 9)
        };
        
        // Place entities on board
        _board.SetCell(_pacman.Position.Row, _pacman.Position.Col, 'P');
        foreach (var ghost in _ghosts)
        {
            _board.SetCell(ghost.Position.Row, ghost.Position.Col, 'G');
        }
    }
    
    public void Run()
    {
        Console.CursorVisible = false;
        Console.WriteLine("PACMAN GAME");
        Console.WriteLine("Use W,A,S,D or Arrow Keys to move. Press Q to quit.");
        Console.WriteLine("Collect all dots to win! Avoid the ghosts!");
        Console.WriteLine("Press any key to start...");
        Console.ReadKey();
        
        while (_state == GameState.Playing)
        {
            HandleInput();
            
            // Update game at fixed intervals
            if (DateTime.Now - _lastUpdate >= _updateInterval)
            {
                Update();
                _lastUpdate = DateTime.Now;
            }
            
            Render();
            Thread.Sleep(50); // Small delay to prevent excessive CPU usage
        }
        
        ShowGameOverScreen();
        Console.CursorVisible = true;
    }
    
    private void HandleInput()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true);
            
            switch (key.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    _pacman.NextDirection = Direction.Up;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    _pacman.NextDirection = Direction.Down;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    _pacman.NextDirection = Direction.Left;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    _pacman.NextDirection = Direction.Right;
                    break;
                case ConsoleKey.Q:
                    _state = GameState.GameOver;
                    break;
            }
        }
    }
    
    private void Update()
    {
        // Move Pacman
        _pacman.TryMove(_board);
        
        // Move Ghosts
        foreach (var ghost in _ghosts)
        {
            ghost.Move(_board);
        }
        
        // Check for collisions with ghosts
        foreach (var ghost in _ghosts)
        {
            if (ghost.CollidesWithPacman(_pacman))
            {
                _state = GameState.GameOver;
                return;
            }
        }
        
        // Check for victory (all dots collected)
        if (_board.DotsRemaining == 0)
        {
            _state = GameState.Victory;
        }
    }
    
    private void Render()
    {
        _board.Render();
        Console.WriteLine($"Score: {_pacman.Score}");
        Console.WriteLine($"Dots remaining: {_board.DotsRemaining}");
        Console.WriteLine("Controls: W/A/S/D or Arrow Keys to move, Q to quit");
    }
    
    private void ShowGameOverScreen()
    {
        Console.Clear();
        
        if (_state == GameState.Victory)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("🎉 CONGRATULATIONS! YOU WON! 🎉");
            Console.WriteLine("You collected all the dots!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("💀 GAME OVER 💀");
            Console.WriteLine("You were caught by a ghost!");
        }
        
        Console.ResetColor();
        Console.WriteLine($"Final Score: {_pacman.Score}");
        Console.WriteLine("Thanks for playing!");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
namespace PacmanGame;

public class Ghost
{
    public Position Position { get; private set; }
    public Direction CurrentDirection { get; private set; } = Direction.Right;
    private Random _random = new Random();
    
    public Ghost(int startRow, int startCol)
    {
        Position = new Position(startRow, startCol);
    }
    
    public void Move(GameBoard board)
    {
        // Simple AI: try to continue in current direction, otherwise pick a random valid direction
        var directions = new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
        var validDirections = new List<Direction>();
        
        foreach (var direction in directions)
        {
            var newPos = Position.Move(direction);
            
            // Handle wrapping
            if (newPos.Col < 0)
                newPos.Col = GameBoard.Width - 1;
            else if (newPos.Col >= GameBoard.Width)
                newPos.Col = 0;
            
            if (!board.IsWall(newPos.Row, newPos.Col))
            {
                validDirections.Add(direction);
            }
        }
        
        if (validDirections.Count > 0)
        {
            // 70% chance to continue in current direction if it's valid
            if (validDirections.Contains(CurrentDirection) && _random.NextDouble() < 0.7)
            {
                // Continue in current direction
            }
            else
            {
                // Pick a random valid direction
                CurrentDirection = validDirections[_random.Next(validDirections.Count)];
            }
            
            // Clear old position
            board.SetCell(Position.Row, Position.Col, ' ');
            
            // Move to new position
            var newPos = Position.Move(CurrentDirection);
            
            // Handle wrapping
            if (newPos.Col < 0)
                newPos.Col = GameBoard.Width - 1;
            else if (newPos.Col >= GameBoard.Width)
                newPos.Col = 0;
            
            Position = newPos;
            board.SetCell(Position.Row, Position.Col, 'G');
        }
    }
    
    public bool CollidesWithPacman(Pacman pacman)
    {
        return Position.Equals(pacman.Position);
    }
}
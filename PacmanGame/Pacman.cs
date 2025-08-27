namespace PacmanGame;

public class Position
{
    public int Row { get; set; }
    public int Col { get; set; }
    
    public Position(int row, int col)
    {
        Row = row;
        Col = col;
    }
    
    public Position Copy()
    {
        return new Position(Row, Col);
    }
    
    public override bool Equals(object? obj)
    {
        return obj is Position other && Row == other.Row && Col == other.Col;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Row, Col);
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    None
}

public static class DirectionExtensions
{
    public static Position Move(this Position position, Direction direction)
    {
        return direction switch
        {
            Direction.Up => new Position(position.Row - 1, position.Col),
            Direction.Down => new Position(position.Row + 1, position.Col),
            Direction.Left => new Position(position.Row, position.Col - 1),
            Direction.Right => new Position(position.Row, position.Col + 1),
            _ => position.Copy()
        };
    }
}

public class Pacman
{
    public Position Position { get; private set; }
    public Direction CurrentDirection { get; set; } = Direction.None;
    public Direction NextDirection { get; set; } = Direction.None;
    public int Score { get; private set; } = 0;
    
    public Pacman(int startRow, int startCol)
    {
        Position = new Position(startRow, startCol);
    }
    
    public bool TryMove(GameBoard board)
    {
        // Try to change direction if a new direction was requested
        if (NextDirection != Direction.None)
        {
            var nextPos = Position.Move(NextDirection);
            if (!board.IsWall(nextPos.Row, nextPos.Col))
            {
                CurrentDirection = NextDirection;
                NextDirection = Direction.None;
            }
        }
        
        // Move in current direction
        if (CurrentDirection != Direction.None)
        {
            var newPos = Position.Move(CurrentDirection);
            
            // Handle wrapping (simple tunnel effect on sides)
            if (newPos.Col < 0)
                newPos.Col = GameBoard.Width - 1;
            else if (newPos.Col >= GameBoard.Width)
                newPos.Col = 0;
            
            if (!board.IsWall(newPos.Row, newPos.Col))
            {
                // Clear old position
                board.SetCell(Position.Row, Position.Col, ' ');
                
                // Check for dot collection
                if (board.HasDot(newPos.Row, newPos.Col))
                {
                    board.EatDot(newPos.Row, newPos.Col);
                    Score += 10;
                }
                
                // Move to new position
                Position = newPos;
                board.SetCell(Position.Row, Position.Col, 'P');
                return true;
            }
        }
        
        return false;
    }
}
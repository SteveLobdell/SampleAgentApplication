namespace PacmanGame;

public class GameBoard
{
    public const int Width = 19;
    public const int Height = 11;
    
    private readonly char[,] _board = new char[Height, Width];
    
    // Board legend:
    // # = Wall
    // . = Dot (food)
    // P = Pacman
    // G = Ghost
    // ' ' = Empty space
    
    private readonly string[] _initialBoard = new string[]
    {
        "###################",
        "#........#........#",
        "#.##.###.#.###.##.#",
        "#.................#",
        "#.##.#.#####.#.##.#",
        "#....#...#...#....#",
        "####.###.#.###.####",
        "#....#.......#....#",
        "#.##.#.#####.#.##.#",
        "#.................#",
        "###################"
    };
    
    public int DotsRemaining { get; private set; }
    
    public GameBoard()
    {
        InitializeBoard();
    }
    
    private void InitializeBoard()
    {
        DotsRemaining = 0;
        
        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
            {
                _board[row, col] = _initialBoard[row][col];
                if (_board[row, col] == '.')
                {
                    DotsRemaining++;
                }
            }
        }
    }
    
    public char GetCell(int row, int col)
    {
        if (row < 0 || row >= Height || col < 0 || col >= Width)
            return '#'; // Treat out-of-bounds as walls
        
        return _board[row, col];
    }
    
    public void SetCell(int row, int col, char value)
    {
        if (row >= 0 && row < Height && col >= 0 && col < Width)
        {
            _board[row, col] = value;
        }
    }
    
    public bool IsWall(int row, int col)
    {
        return GetCell(row, col) == '#';
    }
    
    public bool IsEmpty(int row, int col)
    {
        char cell = GetCell(row, col);
        return cell == ' ' || cell == '.';
    }
    
    public bool HasDot(int row, int col)
    {
        return GetCell(row, col) == '.';
    }
    
    public void EatDot(int row, int col)
    {
        if (HasDot(row, col))
        {
            SetCell(row, col, ' ');
            DotsRemaining--;
        }
    }
    
    public void Render()
    {
        Console.Clear();
        
        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
            {
                char cell = _board[row, col];
                Console.ForegroundColor = cell switch
                {
                    '#' => ConsoleColor.Blue,
                    '.' => ConsoleColor.Yellow,
                    'P' => ConsoleColor.Yellow,
                    'G' => ConsoleColor.Red,
                    _ => ConsoleColor.White
                };
                
                char displayChar = cell switch
                {
                    'P' => 'C', // Pacman as 'C' (mouth shape)
                    'G' => 'M', // Ghost as 'M' (monster)
                    _ => cell
                };
                
                Console.Write(displayChar);
            }
            Console.WriteLine();
        }
        
        Console.ResetColor();
    }
}
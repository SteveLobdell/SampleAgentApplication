using PacmanGame;

try
{
    var game = new Game();
    game.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}

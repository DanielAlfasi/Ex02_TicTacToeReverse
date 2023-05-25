namespace Ex02
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            GameEngine gameToRun = Runner.SetupGame();
            Runner.RunGame(gameToRun);
        }
    }
}

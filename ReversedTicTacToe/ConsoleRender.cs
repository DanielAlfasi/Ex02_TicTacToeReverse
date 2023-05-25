using System;

namespace Ex02
{
    public static class ConsoleRender
    {
        internal static void RenderBoard(Board i_Board)
        {
            int length = i_Board.SequenceSize;
            ConsoleUtils.Screen.Clear();
            DrawBoardHeader(length);
            for (int i = 0 ; i < length ; i++)
            {
                Console.Write($"{i + 1}  ");
                for (int j = 0; j < length; j++)
                {
                    DrawCellContent(i_Board.CellContent(i, j));
                }

                Console.WriteLine("| ");
                Console.Write(" ");
                for (int j = 0 ; j < length ; j++)
                {
                    Console.Write("====");
                }

                Console.WriteLine("===");
            }
        }

        internal static void DisplayPlayersScore(Player[] i_Players)
        {
            foreach (Player player in i_Players)
            {
                Console.WriteLine($"{player.Mark} player's score: {player.Score}");
            }
        }

        internal static void DisplayWinner(Player i_Player)
        {
            Console.WriteLine();
            Console.WriteLine($"The Player with the '{i_Player.Mark}' Mark have Won");
            Console.WriteLine();
        }

        internal static string DisplayRequest(string i_Request)
        {
            Console.WriteLine(i_Request);
            return Console.ReadLine();
        }

        internal static void DisplayMessage(string i_Message)
        {
            Console.WriteLine(i_Message);
        }

        internal static void DisplayWelcome()
        {
            Console.WriteLine(UIStrings.WelcomeMessage);
        }

        private static void DrawBoardHeader(int i_Length)
        {
            Console.Write("    ");
            for (int i = 0; i < i_Length; i++)
            {
                Console.Write($"{i + 1}   ");
            }
            Console.WriteLine();
        }

        private static void DrawCellContent(eMark i_Mark)
        {
            Console.Write($"| {(char)i_Mark} ");
        }
    }
}

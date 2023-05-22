using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public static class ConsoleRender
    {
        public const string k_BoardSizeRequest = "Please enter the board size(between 3 - 9) : ";
        public const string k_BoardSizeRequestInvalid = "Invalid input, Please enter the board size(between 3 - 9) : ";
        public const string k_GameModeRequest = "Please enter game mode, press 1 for PvP or 2 for PvC";
        public const string k_GameModeRequestInvalid = "Invalid input, Please enter game mode, press 1 for PvP or 2 for PvC";
        public const string k_ColumnNumberRequest = "Please enter the column number: ";
        public const string k_ColumnNumberRequestInvalid = "Invalid input, Please enter the column number: ";
        public const string k_RowNumberRequest = "Please enter the row number: ";
        public const string k_RowNumberRequestInvalid = "Invalid input, Please enter the row number: ";
        public const string k_PlayAnotherRoundRequest = "Would you like to play another round? \nPress Y for yes \nPress N for no: ";
        public const string k_PlayAnotherRoundRequestInvalid = "Invalid input, Would you like to play another round? \nPress Y for yes \nPress N for no: ";
        public const string k_GameEndWithTieMessage = "The game is over with tie";
        public const string k_OutOfBoundsMessage = "The coords you entered are invalid";
        public const string k_CellIsMarkedMessage = "This cell is already marked, try again";
        public const string k_QuitSign = "Q";
        public const string k_YesSign = "Y";
        public const string k_NoSign = "N";

        public static void RenderBoard(Board i_Board)
        {
            int length = i_Board.SequenceSize;
            Ex02.ConsoleUtils.Screen.Clear();
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

        private static void DrawBoardHeader(int i_Length)
        {
            Console.Write("    ");
            for (int i = 0 ; i < i_Length ; i++)
            {
                Console.Write($"{i + 1}   ");
            }
            Console.WriteLine();
        }

        private static void DrawCellContent(eMark i_Mark)
        {
            Console.Write($"| {(char)i_Mark} ");
        }

        public static void DisplayPlayersScore(Player[] i_Players)
        {
            foreach (Player player in i_Players)
            {
                Console.WriteLine($"{player.Mark} player's score: {player.Score}");
            }
        }

        public static void DisplayWinner(Player i_Player)
        {
            Console.WriteLine($"{i_Player.Mark} player Won");
        }

        public static string DisplayRequest(string i_Request)
        {
            Console.WriteLine(i_Request);
            return Console.ReadLine();
        }

        public static void DisplayMessage(string i_Message)
        {
            Console.WriteLine(i_Message);
        }
    }
}

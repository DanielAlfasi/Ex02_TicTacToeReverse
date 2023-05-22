using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public static class Runner
    {
        public static GameEngine SetupGame()
        {
            int boardSize = GetBoardSize();
            ePlayerType secondPlayerType = GetSecondPlayerType();
            return new GameEngine(boardSize, secondPlayerType);
        }
        
        public static void RunGame(GameEngine i_GameEngine)
        {
            int rowIndex, columnIndex;
            bool isQNotRaised = true;
            ConsoleRender.RenderBoard(i_GameEngine.Board);
            while (!i_GameEngine.IsGameOver())
            {
                if(!int.TryParse(GetUserMove(ConsoleRender.k_RowNumberRequest), out rowIndex) || !int.TryParse(GetUserMove(ConsoleRender.k_ColumnNumberRequest), out columnIndex))
                {
                    isQNotRaised = false;
                    break;
                }
                rowIndex--;
                columnIndex--;
                if(i_GameEngine.PerformMove(rowIndex, columnIndex))
                {
                    ConsoleRender.RenderBoard(i_GameEngine.Board);
                }
                else 
                {
                    if(i_GameEngine.Board.IsInBoardBounds(rowIndex, columnIndex))
                    {
                        ConsoleRender.DisplayMessage(ConsoleRender.k_CellIsMarkedMessage);
                    }
                    else 
                    {
                        ConsoleRender.DisplayMessage(ConsoleRender.k_OutOfBoundsMessage);
                    }
                }
            }
            if(isQNotRaised)
            {
                if(i_GameEngine.IsGameEndWithTie)
                {
                    ConsoleRender.DisplayMessage(ConsoleRender.k_GameEndWithTieMessage);
                }
                else
                {
                    ConsoleRender.DisplayWinner(i_GameEngine.GetWinner());
                }
                ConsoleRender.DisplayPlayersScore(i_GameEngine.Players);
            }
            string userDesicionForReset = GetUserInputForReset();
            if(userDesicionForReset == ConsoleRender.k_NoSign)
            {
                return;
            }
            i_GameEngine.ResetGame();
            RunGame(i_GameEngine);

        }

        private static bool BoardSizeInputValidator(string i_String)
        {
            return int.TryParse(i_String, out int boardSize) && boardSize >= Game.k_MinSizeForBoard && boardSize <= Game.k_MaxSizeForBoard;
        }

        private static bool PlayerTypeValidator(string i_String)
        {
            return int.TryParse(i_String, out int playerType) && (playerType == 1 || playerType == 2);
        }

        private static bool IsYOrN(string i_String)
        {
            return i_String == ConsoleRender.k_NoSign || i_String == ConsoleRender.k_YesSign;
        }

        private static int GetBoardSize()
        {
            return int.Parse(ConsoleInputGenericValidator(BoardSizeInputValidator, ConsoleRender.k_BoardSizeRequest, ConsoleRender.k_BoardSizeRequestInvalid));
        }

        private static int GetPlayerTypeInt()
        {
            return int.Parse(ConsoleInputGenericValidator(PlayerTypeValidator, ConsoleRender.k_GameModeRequest, ConsoleRender.k_GameModeRequestInvalid));
        }

        private static string GetUserMove(string i_RequestMessage)
        {
            string i_RequestMessageInvalid = "Invalid input, " + i_RequestMessage;
            return ConsoleInputGenericValidator(IsIntOrQ, i_RequestMessage, i_RequestMessageInvalid);
        }



        private static string GetUserInputForReset()
        {
            return ConsoleInputGenericValidator(IsYOrN, ConsoleRender.k_PlayAnotherRoundRequest, ConsoleRender.k_PlayAnotherRoundRequestInvalid);
        }

        public static ePlayerType GetSecondPlayerType()
        {
            if(GetPlayerTypeInt() == 1)
            {
                return ePlayerType.Person;
            }
            else
            {
                return ePlayerType.Computer;
            }
        }
        
        private static bool IsIntOrQ(string i_String)
        {
            return int.TryParse(i_String, out int validInt) || i_String == ConsoleRender.k_QuitSign;
        }

        private static string ConsoleInputGenericValidator(Func <string, bool> i_ValidationFunction, string i_RequestMessage, string i_RequestMessageForInvalidInput)
        {
            string userInput;
            string RequestMessage = i_RequestMessage;
            while (true)
            {
                userInput = ConsoleRender.DisplayRequest(i_RequestMessage);
                if (i_ValidationFunction(userInput))
                {
                    break;
                }
                else
                {
                    i_RequestMessage = i_RequestMessageForInvalidInput;
                }
            }
            return userInput;
        }

    }
}

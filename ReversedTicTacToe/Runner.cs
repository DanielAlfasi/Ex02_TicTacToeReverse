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
            int boardSize = getBoardSize();
            ePlayerType secondPlayerType = getPlayerType();
            return new GameEngine(boardSize, secondPlayerType);
        }

        public static void RunGame(GameEngine i_GameEngine)
        {
            bool userWantAnotherSession = true;
            bool isGameOver = false;
            while(userWantAnotherSession)
            {
                isGameOver = runGameSession(i_GameEngine);
                if(isGameOver)
                {
                    showWinnerAndStats(i_GameEngine);
                }
                userWantAnotherSession = getUserInputForResetParsedToBool();
                if(userWantAnotherSession)
                {
                    i_GameEngine.ResetGame();
                }
            }

        }

        private static void showWinnerAndStats(GameEngine i_GameEngine)
        {
            if (i_GameEngine.IsGameEndWithTie)
            {
                ConsoleRender.DisplayMessage(ConsoleRender.k_GameEndWithTieMessage);
            }
            else
            {
                ConsoleRender.DisplayWinner(i_GameEngine.GetWinner());
            }
            ConsoleRender.DisplayPlayersScore(i_GameEngine.Players);
        }
        
        private static bool runGameSession(GameEngine i_GameEngine)
        {
            int rowIndex, columnIndex;
            bool isQNotRaised = true;
            ConsoleRender.RenderBoard(i_GameEngine.Board);
            while (!i_GameEngine.IsGameOver())
            {
                if(!int.TryParse(getUserMove(ConsoleRender.k_RowNumberRequest), out rowIndex) || !int.TryParse(getUserMove(ConsoleRender.k_ColumnNumberRequest), out columnIndex))
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
                    tellUserWhyMoveFailed(i_GameEngine, rowIndex, columnIndex);
                }
            }
            return isQNotRaised;

        }

        private static void tellUserWhyMoveFailed(GameEngine i_GameEngine, int i_RowIndex, int i_ColumnIndex)
        {
            if (i_GameEngine.Board.IsInBoardBounds(i_RowIndex, i_ColumnIndex))
            {
                ConsoleRender.DisplayMessage(ConsoleRender.k_CellIsMarkedMessage);
            }
            else
            {
                ConsoleRender.DisplayMessage(ConsoleRender.k_OutOfBoundsMessage);
            }
        }

        private static bool boardSizeValidator(string i_String)
        {
            return int.TryParse(i_String, out int boardSize) && boardSize >= Game.MinSizeForBoard && boardSize <= Game.MaxSizeForBoard;
        }

        private static bool playerTypeValidator(string i_String)
        {
            return int.TryParse(i_String, out int playerType) && (playerType == 1 || playerType == 2);
        }

        private static bool isInputYOrNValidator(string i_String)
        {
            return i_String == ConsoleRender.k_NoSign || i_String == ConsoleRender.k_YesSign;
        }

        private static int getBoardSize()
        {
            return int.Parse(userInputValidator(boardSizeValidator, ConsoleRender.k_BoardSizeRequest, ConsoleRender.k_BoardSizeRequestInvalid));
        }

        private static int getGameMode()
        {
            return int.Parse(userInputValidator(playerTypeValidator, ConsoleRender.k_GameModeRequest, ConsoleRender.k_GameModeRequestInvalid));
        }

        private static string getUserMove(string i_RequestMessage)
        {
            string i_RequestMessageInvalid = "Invalid input, " + i_RequestMessage;
            return userInputValidator(isInputIsIntOrQ, i_RequestMessage, i_RequestMessageInvalid);
        }

        private static string getUserInputForReset()
        {
            return userInputValidator(isInputYOrNValidator, ConsoleRender.k_PlayAnotherRoundRequest, ConsoleRender.k_PlayAnotherRoundRequestInvalid);
        }

        private static bool getUserInputForResetParsedToBool()
        {
            return getUserInputForReset() == "Y";
        }

        private static ePlayerType getPlayerType()
        {
            ePlayerType typeToReturn;
            if(getGameMode() == 1)
            {
                typeToReturn = ePlayerType.Person;
            }
            else
            {
                typeToReturn = ePlayerType.Computer;
            }
            return typeToReturn;
        }
        
        private static bool isInputIsIntOrQ(string i_String)
        {
            return int.TryParse(i_String, out int validInt) || i_String == ConsoleRender.k_QuitSign;
        }

        private static string userInputValidator(Func <string, bool> i_ValidationFunction, string i_RequestMessage, string i_RequestMessageForInvalidInput)
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

using System;

namespace Ex02
{
    public static class Runner
    {
        public static GameEngine SetupGame()
        {
            ConsoleRender.DisplayWelcome();
            int boardSize = getBoardSize();
            ePlayerType secondPlayerType = getPlayerType();
            return new GameEngine(boardSize, secondPlayerType);
        }

        public static void RunGame(GameEngine i_GameEngine)
        {
            bool userWantAnotherSession = true;
            bool isGameOver;

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
                ConsoleRender.DisplayMessage(UIStrings.GameEndWithTieMessage);
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
                if(!int.TryParse(getUserMove(UIStrings.RowNumberRequest), out rowIndex) || !int.TryParse(getUserMove(UIStrings.ColumnNumberRequest), out columnIndex))
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
                ConsoleRender.DisplayMessage(UIStrings.CellIsMarkedMessage);
            }
            else
            {
                ConsoleRender.DisplayMessage(UIStrings.OutOfBoundsMessage);
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
            return i_String == UIStrings.NoSign || i_String == UIStrings.YesSign;
        }

        private static int getBoardSize()
        {
            return int.Parse(userInputValidator(boardSizeValidator, UIStrings.BoardSizeRequest, UIStrings.BoardSizeRequestInvalid));
        }

        private static int getGameMode()
        {
            return int.Parse(userInputValidator(playerTypeValidator, UIStrings.GameModeRequest, UIStrings.GameModeRequestInvalid));
        }

        private static string getUserMove(string i_RequestMessage)
        {
            string i_RequestMessageInvalid = "Invalid input, " + i_RequestMessage;
            return userInputValidator(isInputIsIntOrQ, i_RequestMessage, i_RequestMessageInvalid);
        }

        private static string getUserInputForReset()
        {
            return userInputValidator(isInputYOrNValidator, UIStrings.PlayAnotherRoundRequest, UIStrings.PlayAnotherRoundRequestInvalid);
        }

        private static bool getUserInputForResetParsedToBool()
        {
            return getUserInputForReset() == UIStrings.YesSign;
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
            return int.TryParse(i_String, out int validInt) || i_String == UIStrings.QuitSign;
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

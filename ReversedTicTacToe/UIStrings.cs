namespace Ex02
{
    public static class UIStrings
    {
        private const string k_BoardSizeRequest = "Please enter the board size(between 3 - 9) : ";
        private const string k_BoardSizeRequestInvalid = "Invalid input, Please enter the board size(between 3 - 9) : ";
        private const string k_GameModeRequest = "Please enter game mode, press 1 for PvP or 2 for PvC";
        private const string k_GameModeRequestInvalid = "Invalid input, Please enter game mode, press 1 for PvP or 2 for PvC";
        private const string k_ColumnNumberRequest = "Please enter the column number: ";
        private const string k_RowNumberRequest = "Please enter the row number: ";
        private const string k_PlayAnotherRoundRequest = "Would you like to play another round? \nPress Y for yes \nPress N for no ";
        private const string k_PlayAnotherRoundRequestInvalid = "Invalid input, Would you like to play another round? \nPress Y for yes \nPress N for no ";
        private const string k_GameEndWithTieMessage = "The game is over with tie";
        private const string k_OutOfBoundsMessage = "The coords you entered are invalid";
        private const string k_CellIsMarkedMessage = "This cell is already marked, try again";
        private const string k_WelcomeMessage = "Welcome to Reversed TicTacToe created by Gal Botzer and Daniel Alfasi";
        private const string k_QuitSign = "Q";
        private const string k_YesSign = "Y";
        private const string k_NoSign = "N";

        public static string WelcomeMessage
        {
            get { return k_WelcomeMessage; }
        }

        public static string BoardSizeRequest
        {
            get { return k_BoardSizeRequest; }
        }

        public static string BoardSizeRequestInvalid
        {
            get { return k_BoardSizeRequestInvalid; }
        }

        public static string GameModeRequest
        {
            get { return k_GameModeRequest; }
        }

        public static string GameModeRequestInvalid
        {
            get { return k_GameModeRequestInvalid; }
        }

        public static string ColumnNumberRequest
        {
            get { return k_ColumnNumberRequest; }
        }

        public static string RowNumberRequest
        {
            get { return k_RowNumberRequest; }
        }

        public static string PlayAnotherRoundRequest
        {
            get { return k_PlayAnotherRoundRequest; }
        }

        public static string PlayAnotherRoundRequestInvalid
        {
            get { return k_PlayAnotherRoundRequestInvalid; }
        }

        public static string GameEndWithTieMessage
        {
            get { return k_GameEndWithTieMessage; }
        }

        public static string OutOfBoundsMessage
        {
            get { return k_OutOfBoundsMessage; }
        }

        public static string CellIsMarkedMessage
        {
            get { return k_CellIsMarkedMessage; }
        }

        public static string QuitSign
        {
            get { return k_QuitSign; }
        }

        public static string YesSign
        {
            get { return k_YesSign; }
        }

        public static string NoSign
        {
            get { return k_NoSign; }
        }

    }
}

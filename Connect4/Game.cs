using System;

namespace Connect4 {
    class Game {
        
        public Game() {
            GameInstance = this;

            GenerateBoard();
            ProcessAndRender();
        }
        
        public enum PlayerID {
            None
        ,   Yellow
        ,   Red
        }

        public static readonly Vector2 BoardSize = new Vector2(7, 6);
        public static Game GameInstance;
        public char[,] board = new char[BoardSize.x, BoardSize.y];
        public ConsoleKeyInfo Info;
        public bool IsRunning = true;
        
        private Renderer renderer = new Renderer();
        public string log = "";
        public PlayerID turn = new Random().Next(0, 2) == 0 ? PlayerID.Yellow : PlayerID.Red;
        public PlayerID victor = PlayerID.None;

        private void GenerateBoard() {
            for (int y = 0; y < BoardSize.y; y++) {
                for (int x = 0; x < BoardSize.x; x++) {
                    board[x, y] = '-';
                }
            }
        }

        private void ProcessAndRender() {
            renderer.Render();

            while (IsRunning) {
                renderer.MessageLog();

                IsRunning = !IsGameOver();
                if (!IsRunning) {
                    return;
                }

                ClearDialogue();

                Info = Console.ReadKey(true);

                Act();
                renderer.Render();
            }
        }

        public void Act() {
            log = "";

            if (!int.TryParse(Info.KeyChar.ToString(), out int selectedColumn) || selectedColumn > BoardSize.x) {
                return;
            }

            selectedColumn--;

            if (selectedColumn < 0) {
                return;
            }

            PlaceTile(selectedColumn);
        }

        private void PlaceTile(int selectedColumn) {
            for (int i = 0; i < BoardSize.y; i++) { //start from bottom
                char c = board[selectedColumn, i];
                //Console.WriteLine(c + ", " + i);

                if (c == '-') {
                    board[selectedColumn, i] = turn == PlayerID.Yellow ? 'X' : 'O';
                    
                    CheckForVictory(new Vector2(selectedColumn, i));

                    turn = turn == PlayerID.Yellow ? PlayerID.Red : PlayerID.Yellow;

                    break;
                }
            }
        }

        private void CheckForVictory(Vector2 _coord) {
            char c = board[_coord.x, _coord.y];
            int h = _coord.x;
            int v = _coord.y;

            if (CheckHorizontal(c, h, v)
            ||  CheckVertical(c, h, v)
            ||  CheckDiagonalToNorthEast(c, h, v)
            || CheckDiagonalToSouthWest(c, h, v)
            ) {
                victor = turn;
            }
        }

        private bool CheckHorizontal(char c, int h, int v) {
            int matchesFound = 1;
            
            // Check left.
            for (int i = 1; i < 4; i++) {
                if (h - i < 0 || board[h - i, v] != c) {
                    break;
                }

                matchesFound++;
            }

            // Check right.
            for (int i = 1; i < 4; i++) {
                if (h + i > BoardSize.y - 1 || board[h + i, v] != c) {
                    break;
                }

                matchesFound++;
            }

            if (matchesFound == 4) {
                //Console.WriteLine("It's a Horizontal match!");
                return true;
            }

            return false;
        }

        private bool CheckVertical(char c, int h, int v) {
            int matchesFound = 1;
            
            // Check down.
            for (int i = 1; i <= 4; i++) {
                if (v - i < 0 || board[h, v - i] != c) {
                    break;
                }

                matchesFound++;
            }

            // Check up.
            for (int i = 1; i <= 4; i++) {
                if (v + i > BoardSize.y - 1 || board[h, v + i] != c) {
                    break;
                }

                matchesFound++;
            }

            if (matchesFound == 4) {
                //Console.WriteLine("It's a vertical match!");
                return true;
            }

            return false;
        }

        private bool CheckDiagonalToNorthEast(char c, int h, int v) {

            int matchesFound = 1;
            // Check down.
            for (int i = 1; i <= 4; i++) {
                if (v - i < 0 || h - i < 0 || board[h - i, v - i] != c) {
                    break;
                }
                matchesFound++;
            }

            // Check up.
            for (int i = 1; i <= 4; i++) {
                if (v + i > BoardSize.y - 1 || h + i > BoardSize.x - 1 || board[h + i, v + i] != c) {
                    break;
                }
                matchesFound++;
            }

            if (matchesFound == 4) {
                //Console.WriteLine("It's a vertical match!");
                return true;
            }

            return false;
        }
        private bool CheckDiagonalToSouthWest(char c, int h, int v) {
            int matchesFound = 1;

            // Check down.
            for (int i = 1; i <= 4; i++) {
                if (v - i < 0 || h + i > BoardSize.x - 1 || board[h + i, v - i] != c) {
                    break;
                }

                matchesFound++;
            }

            // Check up.
            for (int i = 1; i <= 4; i++) {
                if (v + i > BoardSize.y - 1 || h - i < 0 || board[h - i, v + i] != c) {
                    break;
                }

                matchesFound++;
            }

            if (matchesFound == 4) {
                //Console.WriteLine("It's a vertical match!");
                return true;
            }

            return false;
        }
        private bool IsGameOver() {
            if (victor != PlayerID.None) {
                IsRunning = false;

                // Haxx in a render before we restart the game.
                renderer.Render();
                renderer.MessageLog();

                Console.ForegroundColor = victor == PlayerID.Yellow ? ConsoleColor.Yellow : ConsoleColor.Red;
                Console.WriteLine("\n" + victor.ToString() + " has won. Press ENTER to play again.");

                ConsoleKey key = ConsoleKey.D0; //Inidtialising as '0' key
                while (key != ConsoleKey.Enter) {
                    key = Console.ReadKey(true).Key;
                }

                return true;
            }
            return false;
        }

        public void ClearDialogue() {
            int linesToClear = 6;
            string output = "";
            for (int i = 0; i < linesToClear; i++) {
                for (int j = 0; j < Console.WindowWidth; j++) {
                    output += " ";
                }
            }
            Console.Write(output);
        }
    }
}
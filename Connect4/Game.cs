﻿using System;

namespace Connect4 {
    internal class Game {
        public enum PlayerID {
            None
        ,   Yellow
        ,   Red
        }
        
        public Game() {
            Instance = this;

            Generate();
            ProcessAndRender();
        }
        
        public static readonly Vector2 BoardSize = new Vector2(7, 6);

        public static Game Instance;

        public char[,] board = new char[BoardSize.x, BoardSize.y];
        public ConsoleKeyInfo Info;
        public bool IsRunning = true;

        private static int scoreYellow = 0;
        private static int scoreRed = 0;
        
        private Renderer renderer = new Renderer();
        private string log = "";
        private PlayerID turn = new Random().Next(0, 2) == 0 ? PlayerID.Yellow : PlayerID.Red;
        private PlayerID victor = PlayerID.None;

        private void Generate() {
            for (int y = 0; y < BoardSize.y; y++) {
                for (int x = 0; x < BoardSize.x; x++) {
                    board[x, y] = '-';
                }
            }
        }

        private void ProcessAndRender() {
            renderer.Render();

            while (IsRunning) {
                Log();

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
            for (int i = 0; i < BoardSize.y; i++) {
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
                if ( h - i < 0) { break; }
                if (v - i < 0 || board[h - i, v - i] != c) {
                    break;
                }

                matchesFound++;
            }

            // Check up.
            for (int i = 1; i <= 4; i++) {
                if (h + i > BoardSize.x - 1) { break; }
                if (v + i > BoardSize.y - 1 || board[h + i, v + i] != c) {
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
                if (h + i > BoardSize.x - 1) { break; }
                if (v - i < 0 || board[h + i, v - i] != c) {
                    break;
                }

                matchesFound++;
            }

            // Check up.
            for (int i = 1; i <= 4; i++) {
                if (h - i < 0) { break; }
                if (v + i > BoardSize.y - 1 || board[h - i, v + i] != c) {
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

                if (victor == PlayerID.Yellow) {
                    scoreYellow++;
                } else {
                    scoreRed++;
                }

                // Haxx in a render before we restart the game.
                renderer.Render();
                Log();

                Console.ForegroundColor = victor == PlayerID.Yellow ? ConsoleColor.Yellow : ConsoleColor.Red;
                Console.WriteLine("\n" + victor.ToString() + " has won. Press ENTER to play again.");

                ConsoleKey key = ConsoleKey.D0;
                while (key != ConsoleKey.Enter) {
                    key = Console.ReadKey(true).Key;
                }

                return true;
            }
            return false;
        }

        public void Log() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Yellow: " + scoreYellow);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\tRed: " + scoreRed);

            Console.ResetColor();
            Console.Write("\nChoose a column from 1-" + BoardSize.x);

            if (victor == PlayerID.None) {
                Console.ForegroundColor = turn == PlayerID.Yellow ? ConsoleColor.Yellow : ConsoleColor.Red;
                Console.Write("\n" + turn.ToString() + "'s turn...");
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(log);
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
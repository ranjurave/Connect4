using System;

namespace Connect4 {
    internal class Renderer {
        public void Render() {
            Console.SetCursorPosition(0, 0);
            RenderVerticalBoundary();
            RenderBoard();
            RenderDivider();
            RenderGuide();

            Console.Write("\n");
            RenderVerticalBoundary();
        }

        private static void RenderBoard() {
            //var temp = new Renderer();
            for (int y = Game.BoardSize.y - 1; y >= 0; y--) {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n|| ");
                
                for (int x = 0; x < Game.BoardSize.x; x++) {
                    
                    SetObjectColor(Game.GameInstance.board[x, y]);
                    Console.Write(Game.GameInstance.board[x, y] + " ");
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("||");
            }
        }
        private static void RenderGuide() {
            Console.Write("|| ");
            for (int x = 0; x < Game.BoardSize.x; x++) {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write((x + 1) + " ");
            }
            Console.Write("||");
        }

        private static void SetObjectColor(char _c) {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            switch (_c) {
                case 'O':
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 'X':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
        }

        void RenderVerticalBoundary() {
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i <= (Game.BoardSize.x * 2) + 4; i++) {
                Console.Write('=');
            }
        }

        void RenderDivider() {
            Console.Write("\n||");
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i <= (Game.BoardSize.x * 2); i++) {
                Console.Write('-');
            }
            Console.Write("||\n");
        }
        public void MessageLog() {
            Console.ResetColor();
            Console.Write("\nChoose a column from 1-" + Game.BoardSize.x);

            if (Game.GameInstance.victor == Game.PlayerID.None) {
                Console.ForegroundColor = Game.GameInstance.turn == Game.PlayerID.Yellow ? ConsoleColor.Yellow : ConsoleColor.Red;
                Console.Write("\n" + Game.GameInstance.turn.ToString() + "'s turn...");
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(Game.GameInstance.log);
        }
    }
}
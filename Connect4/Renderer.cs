﻿using System;

namespace Connect4 {
    internal class Renderer {
        public Renderer() {
            //Console.WindowHeight = Game.BoardSize.y + 10;
            //Console.WindowWidth = Game.BoardSize.x + 5;
        }

        public void Render() {
            Console.SetCursorPosition(0, 0);

            RenderVerticalBoundary();

            for (int y = Game.BoardSize.y - 1; y >= 0; y--) {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n|| ");

                for (int x = 0; x < Game.BoardSize.x; x++) {
                    SetObjectColor(Game.Instance.board[x, y]);
                    Console.Write(Game.Instance.board[x, y] + " ");
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("||");
            }

            RenderDivider();
            RenderGuide();

            Console.Write("\n");
            RenderVerticalBoundary();
            Console.Write("\n\n");
        }

        private static void RenderGuide() {
            Console.Write("|| ");
            for (int x = 0; x < Game.BoardSize.x; x++) {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write((x + 1) + " ");
            }
            Console.Write("||");
        }

        private void SetObjectColor(char _c) {
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
    }
}
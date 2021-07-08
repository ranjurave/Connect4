// The most commonly used Connect Four board size is 7 columns × 6 rows.
using System;

namespace Connect4 {
    class Program {
        static void Main() {
            Console.CursorVisible = false;

            while (true) {
                new Game();
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nThanks for playing. Press any key to exit.");
            Console.ReadKey(true);
        }
    }
}

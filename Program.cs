using System;
using TicTacToeGame;

namespace TicTacToeGame
{
    public enum CellState
    {
        Empty,
        X,
        O
    }

    public struct Move
    {
        public int Row;
        public int Col;

        public Move(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }

    public class Game
    {
        private CellState?[,] board = new CellState?[3, 3];
        private bool isPlayer1Turn;
        private bool playWithComputer;
        private Random random = new Random();
        private CellState player1Symbol = CellState.X;
        private CellState player2Symbol = CellState.O;

        public void Start()
        {
            Console.WriteLine("Tic Tac Toe Game");
            Console.WriteLine("Choose a game mode:");
            Console.WriteLine("1 — Player vs. Computer");
            Console.WriteLine("2 — Player vs. Player");

            string choice;

            do
            {
                Console.Write("You choice (1 or 2): ");
                choice = Console.ReadLine();
            } while (choice != "1" && choice != "2");

            playWithComputer = choice == "1";
            isPlayer1Turn = random.Next(2) == 0;

            Console.WriteLine(playWithComputer
                ? $"Goes first {(isPlayer1Turn ? "you" : "computer")} (you: {player1Symbol})"
                : $"Goes first {(isPlayer1Turn ? "Player 1 (X)" : "Player 2 (O)")}");

            while (true)
            {
                PrintBoard();

                if (isPlayer1Turn)
                    PlayerMove(player1Symbol, "Player 1");
                else if (playWithComputer)
                    ComputerMove();
                else
                    PlayerMove(player2Symbol, "Player 2");

                var winner = CheckWinner();
                if (winner != null)
                {
                    PrintBoard();
                    Console.WriteLine(winner == player1Symbol
                        ? (playWithComputer ? "You won!" : "Player 1 won!")
                        : (playWithComputer ? "The computer won.!" : "Player 2 won!"));
                    break;
                }

                if (IsDraw())
                {
                    PrintBoard();
                    Console.WriteLine("Draw!");
                    break;
                }

                isPlayer1Turn = !isPlayer1Turn;
            }
        }

        private void PlayerMove(CellState symbol, string playerName)
        {
            while (true)
            {
                Console.WriteLine($"{playerName} ({symbol}), your turn.");
                Console.Write("Enter the line number (0-2): ");
                int row = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter the column number (0-2): ");
                int col = Convert.ToInt32(Console.ReadLine());

                if (row >= 0 && row < 3 && col >= 0 && col < 3 && board[row, col] == null)
                {
                    board[row, col] = symbol;
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect move, try again..");
                }
            }
        }

        private void ComputerMove()
        {
            Console.WriteLine("The computer makes a move...");
            while (true)
            {
                int row = random.Next(3);
                int col = random.Next(3);

                if (board[row, col] == null)
                {
                    board[row, col] = player2Symbol;
                    break;
                }
            }
        }

        private void PrintBoard()
        {
            Console.WriteLine("\nCurrent state of the board:");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var cell = board[i, j];
                    string symbol = cell == null ? "." : cell.ToString();
                    Console.Write($"{symbol} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private CellState? CheckWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != null && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                    return board[i, 0];
                if (board[0, i] != null && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                    return board[0, i];
            }

            if (board[0, 0] != null && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                return board[0, 0];
            if (board[0, 2] != null && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
                return board[0, 2];

            return null;
        }

        private bool IsDraw()
        {
            foreach (var cell in board)
            {
                if (cell == null)
                    return false;
            }
            return true;
        }
    }
}


class Program
{
    static void Main()
    {
        Game game = new Game();
        game.Start();
    }
}
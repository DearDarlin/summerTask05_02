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
        private CellState playerSymbol;
        private CellState computerSymbol;
        private bool isPlayerTurn;
        private Random random = new Random();

        public void Start()
        {
            Console.WriteLine("Tic Tac Toe Game");

            playerSymbol = random.Next(2) == 0 ? CellState.X : CellState.O;
            computerSymbol = playerSymbol == CellState.X ? CellState.O : CellState.X;
            isPlayerTurn = playerSymbol == CellState.X;

            Console.WriteLine($"Your symbol: {playerSymbol}");
            Console.WriteLine($"goes first {(isPlayerTurn ? "user" : "computer")}");

            while (true)
            {
                PrintBoard();

                if (isPlayerTurn)
                    PlayerMove();
                else
                    ComputerMove();

                var winner = CheckWinner();
                if (winner != null)
                {
                    PrintBoard();
                    Console.WriteLine(winner == playerSymbol ? "You win!" :
                                      winner == computerSymbol ? "The computer won.!" : "Draw!");
                    break;
                }
                isPlayerTurn = !isPlayerTurn;
            }
        }

        private void PlayerMove()
        {
            while (true)
            {
                Console.Write("Enter line number (0-2): ");
                int row = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter the column number (0-2): ");
                int col = Convert.ToInt32(Console.ReadLine());

                if (row >= 0 && row < 3 && col >= 0 && col < 3 && board[row, col] == null)
                {
                    board[row, col] = playerSymbol;
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect move, try again");
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
                    board[row, col] = computerSymbol;
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

            bool isDraw = true;
            foreach (var cell in board)
            {
                if (cell == null)
                {
                    isDraw = false;
                    break;
                }
            }

            return isDraw ? (CellState?)null : null;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }
}
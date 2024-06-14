using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMinimax : MonoBehaviour
{
    private CompManager compManager;
    private int[,] board;

    // // testing
    // private prototype prototype;


    void Start()
    {
        compManager = FindObjectOfType<CompManager>();
        board = compManager.BackingArray;

        // // testing
        // prototype = FindObjectOfType<prototype>();
        // board = prototype.BackingArray;
    }

    void Update()
    {
        
        board = compManager.BackingArray;
    }

    // Checks if there are any open cells
    private bool isBoardFull()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // Checks for wins
    private int evaluateBoard() 
    {
        // Checking for Rows for X or O victory
        for (int row = 0; row < 3; row++)
        {
            if (board[row, 0] == board[row, 1] && board[row, 1] == board[row, 2])
            {
                if (board[row, 0] == 2)
                    return +10;
                else if (board[row, 0] == 1)
                    return -10;
            }
        }

        // Checking for Columns for X or O victory
        for (int col = 0; col < 3; col++)
        {
            if (board[0, col] == board[1, col] && board[1, col] == board[2, col])
            {
                if (board[0, col] == 2)
                    return +10;
                else if (board[0, col] == 1)
                    return -10;
            }
        }

        // Checking for Diagonals for X or O victory
        if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        {
            if (board[0, 0] == 2)
                return +10;
            else if (board[0, 0] == 1)
                return -10;
        }

        if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            if (board[0, 2] == 2)
                return +10;
            else if (board[0, 2] == 1)
                return -10;
        }

        // None has won
        return 0;
    }

    private int minimax(int[,] board, int depth, bool isMax, int alpha, int beta)
    {
        int score = evaluateBoard();

        if (score == 10)
        {
            return score;
        }

        if (score == -10)
        {
            return score;
        }

        if (isBoardFull())
        {
            return 0;
        }

        if (isMax)
        {
            int maxVal = int.MinValue; // -Infinity
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // traverse empty cells
                    if (board[i, j] == 0)
                    {
                        board[i, j] = 2; // AI moves
                        int value = minimax(board, depth + 1, false, alpha, beta); // recursively go through game tree
                        board[i, j] = 0; // undo AI move
                        maxVal = Mathf.Max(maxVal, value);
                        alpha = Mathf.Max(alpha, value);
                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                }
            }
            return maxVal;
        }
        else
        {
            int minVal = int.MaxValue; // +Infinity
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // traverse empty cells
                    if (board[i, j] == 0)
                    {
                        board[i, j] = 1; // Player moves
                        int value = minimax(board, depth + 1, true, alpha, beta); // recursively go through game tree
                        board[i, j] = 0; // undo Player move
                        minVal = Mathf.Min(minVal, value);
                        beta = Mathf.Min(beta, value);
                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                }
            }
            return minVal;
        }
    }

    public int BestButtonMove()
    {
        int bestScore = int.MinValue;
        int bestRow = -1;
        int bestCol = -1;
        
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == 0)
                {
                    board[i, j] = 2;
                    int score = minimax(board, 0, false, int.MinValue, int.MaxValue);
                    board[i, j] = 0;
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestRow = i;
                        bestCol = j;
                    }
                }
            }
        }

        Debug.Log($"Best move is at ({bestRow},{bestCol}) with score {bestScore}");
        int buttonIndex = bestRow * 3 + bestCol;
        return buttonIndex;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimax : MonoBehaviour
{
    public static int[,][,] globalBoard;

    private void Start()
    {
        createBoard();
    }

    private void createBoard()
    {
        // Define the main 3x3 matrix where each element is another 3x3 matrix
        globalBoard = new int[3, 3][,];

        // Initialize each sub-matrix and set each element to zero
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                globalBoard[i, j] = new int[3, 3];

                // Set each element of the sub-grid to zero
                for (int subRow = 0; subRow < 3; subRow++)
                {
                    for (int subCol = 0; subCol < 3; subCol++)
                    {
                        globalBoard[i, j][subRow, subCol] = 0;
                    }
                }
            }
        }
    }

    public void UpdateSubGrid(int mainRow, int mainCol, int[,] subGrid)
    {
        globalBoard[mainRow, mainCol] = subGrid;
    }

    public int[,] GetSubGrid(int mainRow, int mainCol)
    {
        return globalBoard[mainRow, mainCol];
    }


    // -1 player win, +1 AI win, 0 draw
    // X = minimizing player
    // O = maximizing
    // depth = amount of moves ahead to 
 
    // for debuggiing
    public void PrintBoard()
    {
        for (int mainRow = 0; mainRow < 3; mainRow++)
        {
            for (int subRow = 0; subRow < 3; subRow++)
            {
                string rowString = "";
                for (int mainCol = 0; mainCol < 3; mainCol++)
                {
                    for (int subCol = 0; subCol < 3; subCol++)
                    {
                        rowString += globalBoard[mainRow, mainCol][subRow, subCol] + " ";
                    }
                    rowString += "  "; // Separate sub-grids for readability
                }
                Debug.Log(rowString);
            }
            Debug.Log(""); // Separate main grid rows for readability
        }
    }



}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CompManager : MonoBehaviour
{
    public GameObject prefab;
    public int currentPlayer; 

    private GridWinManager GWM; // reference to GWM, used to access same backing array and functions
    private int[,] backingArray;
    
    private AIdifficult AIdifficultObject; // reference to AIdifficult script, used to get difficulty
    public int difficulty;

    // private int count; // used for depth of MiniMax

    void Start()
    {
        difficulty = AIdifficultObject.Difficulty; // gets difficulty from property from AIdifficult
        backingArray = GWM.backingArray;

        currentPlayer = GridWinManager.CurrentPlayer; // static property, use class name instead of instance reference

        // Instantiate the prefab as a GameObject in the scene, and accssing it's components
        GameObject instantiatedPrefab = Instantiate(prefab);
        // Accessing the buttons within the prefab
        Button[] buttons = instantiatedPrefab.GetComponentsInChildren<Button>();
    }

    // programmatically triggers button click
    private void ClickButton(Button button)
    {   
        button.onClick.Invoke();
    }

    public void AImove()
    {
        
        switch(difficulty)
        {
            case 1:
                Minimax1(backingArray, 0, true); // easy mode
                break;

            case 2:
                Minimax2(backingArray, 0, true); // normal mode
                break;

            case 3:
                Minimax3(backingArray, 0, true); // hard mode
                break;

        }
    }

    
    // easy difficulty 
    private int Minimax1(int[,] gameBoard, int depth, bool isMaximizing)
    {
        // base case
        if (depth == 9)
        {
            return GWM.winCheck();
        }

        // Recursive case: Generate possible moves and evaluate them
        // Choose the move with the highest score for the AI (maximizer) or lowest score for the opponent (minimizer)

        return 0; // debug
    }

    // normal difficulty 
    private int Minimax2(int[,] gameBoard, int depth, bool isMaximizing)
    {
        // base case
        if (depth == 9)
        {
            return GWM.winCheck();
        }

        // Recursive case: Generate possible moves and evaluate them
        // Choose the move with the highest score for the AI (maximizer) or lowest score for the opponent (minimizer)




        return 0; // debug
    }

    // hard difficulty, maximizing = X aka 1, minimizing = 0 aka 2
    private double Minimax3(int[,] gameBoard, int depth, bool isMaximizing)
    {  
        // base case
        if (depth == 9)
        {
            return GWM.winCheck();
        }

        // Recursive case: Generate possible moves and evaluate them
        // Choose the move with the highest score for the AI (maximizer) or lowest score for the opponent (minimizer)

        if (isMaximizing)
        {
            double bestScore = isMaximizing ? double.NegativeInfinity : double.PositiveInfinity;

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (gameBoard[row, col] == 0)
                    {
                        gameBoard[row, col] = isMaximizing ? 1 : 2; // make the move

                        // gets button coordinates
                        int buttonIndex = row * 3 + col;
                        Button button = GWM.buttons[buttonIndex];

                        double score = Minimax3(gameBoard, depth + 1, !isMaximizing); // flip flops between min and max to determine best outcome

                        gameBoard[row, col] = 0; // undo the move

                        if (isMaximizing)
                        {
                            bestScore = Math.Max(bestScore, score);
                        }
                        else
                        {
                            bestScore = Math.Min(bestScore, score);
                        }
                    }
                }
            }
            
            return bestScore;
        } 

        return 0; // debug
    }




    
}

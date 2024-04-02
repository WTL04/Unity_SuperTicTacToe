using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CompManager : MonoBehaviour
{

    private AIdifficult AIdifficultObject; // reference to AIdifficult script, used to get difficulty
    public int difficulty;

    // GridWinManager copy
    public static int currentPlayer = 2; // X = 1, O = 2 // currentPlayer changes on CLick() so X = 2
    private int count = 0;
    public int[,] backingArray = new int[3, 3];

    public Button[] buttons;

    public GameObject crossWinner;
    public GameObject circleWinner;
    public GameObject draw;

    // private int count; // used for depth of MiniMax

    void Start()
    {
        // DOES NOT WORK RN

        // Find the AIdifficultObject in the scene
        AIdifficultObject = FindObjectOfType<AIdifficult>();

        // Check if AIdifficultObject is not null
        if (AIdifficultObject != null)
        {
            // Get the difficulty from AIdifficultObject
            difficulty = AIdifficultObject.Difficulty;
        }
        else
        {
            Debug.LogError("AIdifficultObject not found in the scene!");
        }

        // FIX THIS
        // // Instantiate the prefab as a GameObject in the scene, and accssing it's components
        // GameObject instantiatedPrefab = Instantiate(prefab);
        // Button[] buttons = instantiatedPrefab.GetComponentsInChildren<Button>();

        // GridWinManager copy
        crossWinner.SetActive(false);
        circleWinner.SetActive(false);
        draw.SetActive(false);
    }

    //adds 1 or 2 to backing array
    public void statusUpdate(int buttonIndex) {

        if (currentPlayer == 2)
        {
            AImove();
        }
        
        count++;

        if (backingArray[buttonIndex / 3, buttonIndex % 3] == 0) 
        {

            backingArray[buttonIndex / 3, buttonIndex % 3] = currentPlayer;
            
        }
        currentPlayer = (currentPlayer == 1) ? 2 : 1; // compact if-else statement  

    }

    // GridWinManager copy
    private void resetGrid() {
        count = 0;
        backingArray = new int[3, 3];
    }

    //checks if there are any wins in the backing array, set by the statusUpdate() function
    public int winCheck() {

        for (int i = 0; i < 3; i++) {
            
            //row wins
            if (backingArray[i, 0] != 0 && backingArray[i, 0] == backingArray[i, 1] && backingArray[i, 0] == backingArray[i, 2]) 
            {

                if (currentPlayer == 1) {
                    crossWinner.SetActive(true);
                    resetGrid();
                    return 1;
                }
                else {
                    circleWinner.SetActive(true);
                    resetGrid();
                    return 2;
                }
                
                
            }
            
            
            //column wins
            if (backingArray[0, i] != 0 && backingArray[0, i] == backingArray[1, i] && backingArray[0, i] == backingArray[2, i])
            {
                //resetGrid();
                if (currentPlayer == 1) {
                    crossWinner.SetActive(true);
                    resetGrid();
                    return 1;
                }
                else {
                    circleWinner.SetActive(true);
                    resetGrid();
                    return 2;
                }
                
            }

        }


        //diagonal  wins
        if (backingArray[0, 0] != 0 && backingArray[0, 0] == backingArray[1, 1] && backingArray[0, 0] == backingArray[2, 2]) 
        {
            //resetGrid();
            if (currentPlayer == 1) {
                crossWinner.SetActive(true);
                resetGrid();
                return 1;
            }
            else {
                circleWinner.SetActive(true);
                resetGrid();
                return 2;
            }
            
        }

        if (backingArray[0, 2] != 0 && backingArray[0, 2] == backingArray[1, 1] && backingArray[0, 2] == backingArray[2, 0]) 
        {

            
            if (currentPlayer == 1) {
                crossWinner.SetActive(true);
                resetGrid();
                return 1;
                
            }
            else {
                circleWinner.SetActive(true);
                resetGrid();
                return 2;
                
            }
            
        }

        //draw
        if (count == 9) 
        {
            draw.SetActive(true);
            resetGrid();
            return 3;
        }

        return 0;

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
    
    // easy difficulty NOT DONE
    private int Minimax1(int[,] gameBoard, int depth, bool isMaximizing)
    {
        // base case
        if (depth == 9)
        {
            return winCheck();
        }

        // Recursive case: Generate possible moves and evaluate them
        // Choose the move with the highest score for the AI (maximizer) or lowest score for the opponent (minimizer)

        return 0; // debug
    }

    // normal difficulty NOT DONE
    private int Minimax2(int[,] gameBoard, int depth, bool isMaximizing)
    {
        // base case
        if (depth == 9)
        {
            return winCheck();
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
            return winCheck();
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
                        Button button = buttons[buttonIndex];

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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;


public class CompManager : MonoBehaviour
{

    private AIdifficult AIdifficultObject; // reference to AIdifficult script, used to get difficulty
    public static int difficulty;
    public static bool isAIturn;

    // GridWinManager copy
    public static int currentPlayer = 2; // X = 1, O = 2 // currentPlayer changes on CLick() so X = 2 // STATIC for scope ranges throughtout all subgrids
    private int count = 0;
    private int[,] backingArray = new int[3, 3];

    public Button[] buttons;

    public GameObject crossWinner;
    public GameObject circleWinner;
    public GameObject draw;

    // private int count; // used for depth of MiniMax

    void Start()
    {
        // Find the AIdifficultObject in the scene
        AIdifficultObject = FindObjectOfType<AIdifficult>();

        // Get the difficulty from AIdifficultObject
        difficulty = AIdifficultObject.Difficulty;

        // GridWinManager copy
        crossWinner.SetActive(false);
        circleWinner.SetActive(false);
        draw.SetActive(false);

        isAIturn = (currentPlayer == 1 ) ? isAIturn = true : isAIturn = false;
    }

    //adds 1 or 2 to backing array
    private void statusUpdate(int buttonIndex) {
        count++;
        if (backingArray[buttonIndex / 3, buttonIndex % 3] == 0) 
        {
            backingArray[buttonIndex / 3, buttonIndex % 3] = currentPlayer;
        }
    }

    public void changeTurns(int buttonIndex)
    {

        DisableButtons(); // idek if this works

        statusUpdate(buttonIndex);
        
        currentPlayer = (currentPlayer == 1) ? 2 : 1; // compact if-else statement

        if (currentPlayer == 1)
        {
            isAIturn = true;
        }
        LogBackingArray(); // idek if this works

        EnableButtons();
         
    }

    void Update()
    {
        if (isAIturn)
        {
            AImove();
            isAIturn = false;
            Debug.Log("AI moved");
        }
    }


    private void DisableButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }

    private void EnableButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }


    //checks if there are any wins in the backing array, set by the statusUpdate() function
    public int winCheck() 
    {

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


    public void AImove()
    {
        StartCoroutine(DelayedAImove());
    }

    IEnumerator DelayedAImove()
    {
        yield return new WaitForSeconds(2f);

        Button buttonToClick;

        switch(difficulty)
        {
            case 1:
                // easy mode, randomly chooses
                int randInt = UnityEngine.Random.Range(0, 9);
                
                while (backingArray[randInt / 3, randInt % 3] != 0)
                {
                    randInt = UnityEngine.Random.Range(0, 9);
                }

                buttonToClick = buttons[randInt];
                ClickButton(buttonToClick);
                break;

            case 2:
                // normal mode
                break;

            case 3:
                // hard mode
                int bestMoveIndex = Minimax3(backingArray, 0, true); 
                if (bestMoveIndex != -1)
                {
                    buttonToClick = buttons[bestMoveIndex];
                    ClickButton(buttonToClick);
                }

                break;

        }

    }

    // // normal difficulty NOT DONE
    // private int Minimax2(int[,] gameBoard, int depth, bool isMaximizing)
    // {
    //     // base case
    //     if (depth == 9)
    //     {
    //         return winCheck();
    //     }

    //     // Recursive case: Generate possible moves and evaluate them
    //     // Choose the move with the highest score for the AI (maximizer) or lowest score for the opponent (minimizer)




    //     return 0; // debug
    // }

    // // hard difficulty, maximizing = X aka 1, minimizing = 0 aka 2
    // private double Minimax3(int[,] gameBoard, int depth, bool isMaximizing)
    // {  
    //     // base case
    //     if (depth == 9)
    //     {
    //         return winCheck();
    //     }

    //     // Recursive case: Generate possible moves and evaluate them
    //     // Choose the move with the highest score for the AI (maximizer) or lowest score for the opponent (minimizer)

    //     double bestScore = isMaximizing ? double.NegativeInfinity : double.PositiveInfinity;

    //     for (int row = 0; row < 3; row++)
    //     {
    //         for (int col = 0; col < 3; col++)
    //         {
    //             if (gameBoard[row, col] == 0)
    //             {
    //                 gameBoard[row, col] = isMaximizing ? 1 : 2; // make the move

    //                 double score = Minimax3(gameBoard, depth + 1, !isMaximizing); // flip flops between min and max to determine best outcome

    //                 gameBoard[row, col] = 0; // undo the move

    //                 if (isMaximizing)
    //                 {
    //                     bestScore = Math.Max(bestScore, score);
    //                 }
    //                 else
    //                 {
    //                     bestScore = Math.Min(bestScore, score);
    //                 }
    //             }
    //         }
    //     }
        
    //     return bestScore;
    // }

    private int Minimax3(int[,] gameBoard, int depth, bool isMaximizing)
    {
        Debug.Log($"Depth: {depth}");
        // Base case
        if (depth == 9)
        {
            return -1; // Return invalid move
        }

        int bestMoveIndex = -1;
        double bestScore = isMaximizing ? double.NegativeInfinity : double.PositiveInfinity;

        for (int index = 0; index < 9; index++)
        {
            int row = index / 3;
            int col = index % 3;

            if (gameBoard[row, col] == 0)
            {
                gameBoard[row, col] = isMaximizing ? 1 : 2; // make the move
                double score = Minimax3(gameBoard, depth + 1, !isMaximizing); // flip flops between min and max to determine best outcome
                gameBoard[row, col] = 0; // undo the move

                if ((isMaximizing && score > bestScore) || (!isMaximizing && score < bestScore))
                {
                    bestScore = score;
                    bestMoveIndex = index;
                }
            }
        }
        Debug.Log($"Depth: {depth}, Best Move Index: {bestMoveIndex}");
        return bestMoveIndex;
    }

    // GridWinManager copy
    private void resetGrid() 
    {
        count = 0;
        backingArray = new int[3, 3];
    }

    // programmatically triggers button click
    private void ClickButton(Button button)
    {   
        button.onClick.Invoke();
    }

    //debugging 
    void LogBackingArray() 
    {
        string arrayString = "backingArray:\n";

        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                arrayString += backingArray[i, j].ToString() + " ";
            }
            arrayString += "\n";
        }

        Debug.Log(arrayString);
    }


}

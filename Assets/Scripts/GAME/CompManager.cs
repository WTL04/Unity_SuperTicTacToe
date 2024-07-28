using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CompManager : MonoBehaviour
{
    [SerializeField]
    private GameObject overlay; // stops button interaction after player move

    private AIdifficult AIdifficultObject; // reference to AIdifficult script, used to get difficulty
    public static int difficulty;

    private CanvasManager canvasManager;
    private SimpleMinimax minimax;

    // GridWinManager copy
    public static int currentPlayer = 2; // X = 1, O = 2 // currentPlayer changes on CLick() so X = 2 // STATIC for scope ranges throughtout all subgrids
    private int count = 0;
    public int[,] backingArray = new int[3, 3];

    public Button[] buttons;

    //property for current player
    public static int CurrentPlayer {
        get {return currentPlayer;}
    }

    // property, used for minimax?
    public int[,] BackingArray {
        get {return backingArray;}
    }

    void Start()
    {
        // Find object that references to AIdifficult, CanvasManager, SimpleMinimax scripts
        AIdifficultObject = FindObjectOfType<AIdifficult>();
        canvasManager = FindObjectOfType<CanvasManager>();
        minimax = FindObjectOfType<SimpleMinimax>();


        // Get the difficulty from AIdifficultObject
        difficulty = AIdifficultObject.Difficulty;

        // sets overlay to false
        overlay.SetActive(false);
    }

    // helper method
    //adds 1 or 2 to backing array
    private void statusUpdate(int buttonIndex) {
        count++;
        if (backingArray[buttonIndex / 3, buttonIndex % 3] == 0) 
        {
            backingArray[buttonIndex / 3, buttonIndex % 3] = currentPlayer;
        }
    }


    // onClick() event
    public void updateBackingArray(int buttonIndex)
    {
        currentPlayer = (currentPlayer == 1) ? 2 : 1; // compact if-else statement
        statusUpdate(buttonIndex);
    }

    //checks if there are any wins in the backing array, set by the statusUpdate() function
    public int winCheck() 
    {

        for (int i = 0; i < 3; i++) {
            
            //row wins
            if (backingArray[i, 0] != 0 && backingArray[i, 0] == backingArray[i, 1] && backingArray[i, 0] == backingArray[i, 2]) 
            {
                return backingArray[i, 0];
            }
            
            
            //column wins
            if (backingArray[0, i] != 0 && backingArray[0, i] == backingArray[1, i] && backingArray[0, i] == backingArray[2, i])
            {
                return backingArray[0, i];
            }
        }


        //diagonal  wins
        if (backingArray[0, 0] != 0 && backingArray[0, 0] == backingArray[1, 1] && backingArray[0, 0] == backingArray[2, 2]) 
        {
            return backingArray[0, 0];
        }

        if (backingArray[0, 2] != 0 && backingArray[0, 2] == backingArray[1, 1] && backingArray[0, 2] == backingArray[2, 0]) 
        {
            return backingArray[0, 2];
        }

        //draw
        if (count == 9) 
        {
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
        overlay.SetActive(true);

        yield return new WaitForSeconds(1f);

        // AI = circle, checks for 1 because changeTurns turns it into 2 after being pressed
        if (currentPlayer == 1)
        {
            switch(difficulty)
            {
                case 1:
                    // easy
                    ClickButton(buttons[EasyAIDiff()]);
                    break;

                case 2:
                    // normal 
                    ClickButton(buttons[MidAIDiff()]);
                    break;

                case 3:
                    // hard mode
                    ClickButton(buttons[HardAIDiff()]);
                    LogBackingArray();
                    break;
            }
        }
        overlay.SetActive(false);

    }

    private int EasyAIDiff()
    {
        // easy mode, randomly chooses
        int randInt = UnityEngine.Random.Range(0, 9);
        
        while (backingArray[randInt / 3, randInt % 3] != 0)
        {
            randInt = UnityEngine.Random.Range(0, 9);
        }

        return randInt;
    }

    private int MidAIDiff()
    {
        // iterates thru backingArray
        for (int i = 0; i < 9; i++)
        {
            int row = i / 3;
            int col = i % 3;

            if (backingArray[row, col] == 0)
            {
                // simulate Ai move
                backingArray[row, col] = 2;

                // if 2 in a row, make winning move
                if (winCheck() == 2)
                {
                    backingArray[row, col] = 0;
                    return i;
                }
                backingArray[row, col] = 1;

                // if player 2 in a row, block move
                if (winCheck() == 1)
                {
                    backingArray[row, col] = 0;
                    return i;
                }
                backingArray[row, col] = 0;

            }

        }

        // return random move
        int randInt = EasyAIDiff();
        return randInt;
    }


    // work in progress
    private int HardAIDiff()
    {
        return minimax.BestButtonMove();
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

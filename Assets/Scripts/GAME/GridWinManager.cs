using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridWinManager : MonoBehaviour
{

    public Button[] buttons;

    public static int currentPlayer = 2; // X = 1, O = 2 // currentPlayer changes on CLick() so X = 2
    private int count = 0;
    public int[,] backingArray = new int[3, 3];
    
    
    //property
    public static int CurrentPlayer {
        get {return currentPlayer;}
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
        LogBackingArray(); // debug
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

    private void resetGrid() {
        count = 0;
        backingArray = new int[3, 3];
    }

    //debugging 
    void LogBackingArray() {
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


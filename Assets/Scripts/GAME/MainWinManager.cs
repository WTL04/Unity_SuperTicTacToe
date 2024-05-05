using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainWinner : MonoBehaviour
{

    public List<GridWinManager> subGrids;
    public List<CompManager> AIsubGrids;

    public int count = 0;
    public int currentPlayer;
    public int[,] backingArray = new int[3, 3];

    //property
    public int[,] MainBackingArray
    {
        get {return backingArray;}
    }

    //checks for currentPlayer, sets the backingArray to what the results are from sub-grids. 
    // onClick() event
    public void MainGridUpdate(int gridIndex) 
    {
        count++;
        currentPlayer = GridWinManager.CurrentPlayer;
        int result = subGrids[gridIndex].winCheck();
        backingArray[gridIndex / 3, gridIndex % 3] = result;
        MainWinnerCheck();
    }

    // for AI singleplayer mode
    public void AIMainGridUpdate(int gridIndex) 
    {
        count++;
        currentPlayer = GridWinManager.CurrentPlayer;
        int result = AIsubGrids[gridIndex].winCheck();
        backingArray[gridIndex / 3, gridIndex % 3] = result;
        MainWinnerCheck();
    }

    //checks for winner according to the backingArray
    void MainWinnerCheck()
    {
        
        for (int i = 0; i < 3; i++) {
            
            //row wins
            if (backingArray[i, 0] != 0 && backingArray[i, 0] == backingArray[i, 1] && backingArray[i, 0] == backingArray[i, 2]) 
            {

                if (currentPlayer == 1) {
                    ChangeScene(true);
                }
                else {
                    ChangeScene(false);    
                }
                resetGrid();
            }
            
            
            //column wins
            if (backingArray[0, i] != 0 && backingArray[0, i] == backingArray[1, i] && backingArray[0, i] == backingArray[2, i])
            {

                if (currentPlayer == 1) {
                    ChangeScene(true);
                }
                else {
                    ChangeScene(false);    
                }
                resetGrid();
                
            }

        }


        //diagonal  wins
        if (backingArray[0, 0] != 0 && backingArray[0, 0] == backingArray[1, 1] && backingArray[0, 0] == backingArray[2, 2]) 
        {

            if (currentPlayer == 1) {
                ChangeScene(true);
            }
            else {
                ChangeScene(false);    
            }
            resetGrid();

        }

        if (backingArray[0, 2] != 0 && backingArray[0, 2] == backingArray[1, 1] && backingArray[0, 2] == backingArray[2, 0]) 
        {

            if (currentPlayer == 1) {
                ChangeScene(true);                
            }
            else {
                ChangeScene(false);                
            }
            resetGrid();

        }

        //draw
        if (count == 81) 
        {
            resetGrid();
        }

    }

    private void resetGrid() {
        count = 0;
        backingArray = new int[3, 3];
    }

    //change scene to winner
    public void ChangeScene(bool winner) 
    {
        if (winner) {
            //X win
            SceneManager.LoadScene("CrossWinner");
        } else {
            //O win
            SceneManager.LoadScene("CricleWinner");
        }
            
    }

     //debugging 
    public void LogBackingArray() {
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

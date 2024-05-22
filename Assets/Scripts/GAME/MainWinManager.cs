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

    public DisplaySubGridWins displaySubGridWins;

    //property
    public int[,] MainBackingArray
    {
        get {return backingArray;}
    }


    void Start()
    {
        displaySubGridWins = GetComponent<DisplaySubGridWins>();
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

        // displays winner images in subgrid after win is detected
        displaySubGridWins.DisplayWin(result, gridIndex);
        MainWinnerCheck();
    }

    //checks for winner according to the backingArray
    void MainWinnerCheck()
    {
        
        for (int i = 0; i < 3; i++) {
            
            //row wins
            if (backingArray[i, 0] != 0 && backingArray[i, 0] == backingArray[i, 1] && backingArray[i, 0] == backingArray[i, 2]) 
            {
                resetGrid();
                SceneManager.LoadScene(currentPlayer == 1 ? "CircleWinner" : "CrossWinner");
                
            }
            
            
            //column wins
            if (backingArray[0, i] != 0 && backingArray[0, i] == backingArray[1, i] && backingArray[0, i] == backingArray[2, i])
            {
                resetGrid();
                SceneManager.LoadScene(currentPlayer == 1 ? "CircleWinner" : "CrossWinner");
            }

        }


        //diagonal  wins
        if (backingArray[0, 0] != 0 && backingArray[0, 0] == backingArray[1, 1] && backingArray[0, 0] == backingArray[2, 2]) 
        { 
            resetGrid();
            SceneManager.LoadScene(currentPlayer == 1 ? "CircleWinner" : "CrossWinner");
           
        }

        if (backingArray[0, 2] != 0 && backingArray[0, 2] == backingArray[1, 1] && backingArray[0, 2] == backingArray[2, 0]) 
        {
            resetGrid();
            SceneManager.LoadScene(currentPlayer == 1 ? "CircleWinner" : "CrossWinner");
        }

        //draw
        if (count == 81) 
        {
            resetGrid();
            SceneManager.LoadScene("Draw");

        }

    }

    private void resetGrid() {
        count = 0;
        backingArray = new int[3, 3];
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

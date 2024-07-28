using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class CanvasManager : MonoBehaviour
{
    public GameObject[] subGrids;
    public List<GridWinManager> gridWins;
    public List<CompManager> AIgridWins;
    
    private MainWinner mainWinner;
    private int [,] backingArray;

    // flag for ai to move AFTER switchCanvas finishes
    public static bool AIcanvasChanged = false;
    
    public HashSet <int> wonGrids = new HashSet<int>();
    public GraphStructure graph;

    //property
    public HashSet <int> WonGrids
    {
        get {return wonGrids;}
    }

    // initalizes the graph
    void Start()
    {
        graph = new GraphStructure(this);
        CreateGraph();
    }

    public void SwitchCanvas(int canvasIndex)
    {
        StartCoroutine(CanvasDelay(canvasIndex));
    }

    //creates a time delay in switching canvases
    private IEnumerator CanvasDelay(int canvasIndex)
    {
        //testing, later reset to 1
        yield return new WaitForSeconds(0.5f); 

        //grants access to MainWinner backingArray
        mainWinner = subGrids[canvasIndex].GetComponentInParent<MainWinner>();

        // flags if buttonIndex and canvasIndex are equal and subgrid won
        bool wonByMove = false;

        // get Main Backing Array
        backingArray = mainWinner.MainBackingArray;

        if (wonGrids.Contains(canvasIndex))
        {
            canvasIndex = graph.SearchNearestGrid(canvasIndex);
        }

        for (int i = 0; i < subGrids.Length; i++)
        {
            //if something on the backingArray is not 0, then keep it active
            if (backingArray[i / 3, i % 3] != 0 && !wonGrids.Contains(i)) 
            {
                wonByMove = true;
                wonGrids.Add(i);
            }
        }
        
        if (wonByMove && wonGrids.Contains(canvasIndex))
        {
            canvasIndex = graph.SearchNearestGrid(canvasIndex);
        }

        for (int i = 0; i < subGrids.Length; i++)
        {
            subGrids[i].SetActive(i == canvasIndex ? true : false);
        }
   
    }

    // for AI gamemode
    public void AISwitchCanvas(int canvasIndex)
    {
        StartCoroutine(AICanvasDelay(canvasIndex));
    }

    IEnumerator AICanvasDelay(int canvasIndex)
    {
        //testing, later reset to 1
        yield return new WaitForSeconds(0.5f); 

        //grants access to MainWinner backingArray
        mainWinner = subGrids[canvasIndex].GetComponentInParent<MainWinner>();

        // flags if buttonIndex and canvasIndex are equal and subgrid won
        bool wonByMove = false;

        // get Main Backing Array
        backingArray = mainWinner.MainBackingArray;

        if (wonGrids.Contains(canvasIndex))
        {
            canvasIndex = graph.SearchNearestGrid(canvasIndex);
        }

        for (int i = 0; i < subGrids.Length; i++)
        {
            //if something on the backingArray is not 0, then keep it active
            if (backingArray[i / 3, i % 3] != 0 && !wonGrids.Contains(i)) 
            {
                wonByMove = true;
                wonGrids.Add(i);
            }
        }
        
        if (wonByMove && wonGrids.Contains(canvasIndex))
        {
            canvasIndex = graph.SearchNearestGrid(canvasIndex);
        }

        for (int i = 0; i < subGrids.Length; i++)
        {
            subGrids[i].SetActive(i == canvasIndex ? true : false);
        }

            
        // flag AIcanvasChange set true for ai to move
        AIcanvasChanged = true;
    }


    public void CreateGraph()
    {
        for (int i = 0; i < 9; i++)
        {
            graph.AddSubGrid(i);
        }
        graph.AddEdges();

    }

    private void DisableButtons(int canvasIndex)
    {
        Button[] buttons = subGrids[canvasIndex].GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }


}

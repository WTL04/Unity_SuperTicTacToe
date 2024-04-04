using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class CanvasManager : MonoBehaviour
{
    public GameObject[] subGrids;
    public List<GridWinManager> gridWins;
    public List<CompManager> AIgridWins;
    // public int result;

    private int nearestGrid = -1;
    private MainWinner mainWinner;
    private int [,] backingArray;
    
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
    IEnumerator CanvasDelay(int canvasIndex)
    {

        yield return new WaitForSeconds(.5f); //testing, later reset to 1
        mainWinner = subGrids[canvasIndex].GetComponentInParent<MainWinner>(); //grants access to MainWinner backingArray
        bool wonByMove = false;

        if (mainWinner != null)
        {
            backingArray = mainWinner.MainBackingArray;
        }

        if (wonGrids.Contains(canvasIndex))
        {
            nearestGrid = graph.SearchNearestGrid(canvasIndex);
            subGrids[nearestGrid].SetActive(true);
        }
        else 
        {
            nearestGrid = -1;
        }


        for (int i = 0; i < subGrids.Length; i++)
        {
            // result = gridWins[i].winCheck();

            //if something on the backingArray is not 0, then keep it active
            if (backingArray[i / 3, i % 3] != 0 && !wonGrids.Contains(i)) 
            {
                //disables interaction after win
                GraphicRaycaster raycaster = GetComponentInChildren<GraphicRaycaster>();
                raycaster.enabled = false;

                subGrids[i].SetActive(true);
                wonByMove = true;
                wonGrids.Add(i);

            }
    
        }

        if (wonByMove && wonGrids.Contains(canvasIndex))
        {
            nearestGrid = graph.SearchNearestGrid(canvasIndex);
        }


        for (int i = 0; i < subGrids.Length; i++)
        {
            if (wonGrids.Contains(i) || i == nearestGrid || i == canvasIndex)
            {
                subGrids[i].SetActive(true);
            }
            else 
            {
                subGrids[i].SetActive(false);
            }
        }

        // Debug.Log("Won Grids: " + string.Join(", ", wonGrids));
        // Debug.Log("Nearest Grid: " + nearestGrid);
   
    }

    public void CreateGraph()
    {

        for (int i = 0; i < 9; i++)
        {
            graph.AddSubGrid(i);
        }
        graph.AddEdges();

    }

}

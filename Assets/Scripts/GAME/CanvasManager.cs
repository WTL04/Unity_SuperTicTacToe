using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class CanvasManager : MonoBehaviour
{
    public GameObject[] subGrids;
    public List<GridWinManager> gridWins;
    public List<CompManager> AIgridWins;

    private int nearestGrid = -1;
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
    IEnumerator CanvasDelay(int canvasIndex)
    {

        yield return new WaitForSeconds(0.5f); //testing, later reset to 1
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
            // if (wonGrids.Contains(i) || i == nearestGrid || i == canvasIndex)
            // {
            //     subGrids[i].SetActive(true);
            // }
            // else 
            // {
            //     subGrids[i].SetActive(false);
            // }

            //prototype 1

            if (i == nearestGrid || i == canvasIndex)
            {
                subGrids[i].SetActive(true);
            }
            else if (wonGrids.Contains(i))
            {
                DisableButtons(i);
                subGrids[i].SetActive(false);
            }
            else
            {
                subGrids[i].SetActive(false);
            }
        }
   
    }

    // for AI gamemode
    public void AISwitchCanvas(int canvasIndex)
    {
        StartCoroutine(AICanvasDelay(canvasIndex));
    }

    // prototype 1
    // IEnumerator AICanvasDelay(int canvasIndex)
    // {

    //     yield return new WaitForSeconds(0.5f);
    //     mainWinner = subGrids[canvasIndex].GetComponentInParent<MainWinner>(); //grants access to MainWinner backingArray
    //     bool wonByMove = false;

    //     if (mainWinner != null)
    //     {
    //         backingArray = mainWinner.MainBackingArray;
    //     }

    //     if (wonGrids.Contains(canvasIndex))
    //     {
    //         nearestGrid = graph.SearchNearestGrid(canvasIndex);
    //         subGrids[nearestGrid].SetActive(true);
    //     }
    //     else 
    //     {
    //         nearestGrid = -1;
    //     }


    //     for (int i = 0; i < subGrids.Length; i++)
    //     {
    //         //if something on the backingArray is not 0, then keep it active
    //         if (backingArray[i / 3, i % 3] != 0 && !wonGrids.Contains(i)) 
    //         {
    //             //disables interaction after win
    //             GraphicRaycaster raycaster = GetComponentInChildren<GraphicRaycaster>();
    //             raycaster.enabled = false;

    //             subGrids[i].SetActive(true);
    //             wonByMove = true;
    //             wonGrids.Add(i);

    //         }
    //     }

    //     if (wonByMove && wonGrids.Contains(canvasIndex))
    //     {
    //         nearestGrid = graph.SearchNearestGrid(canvasIndex);
    //     }


    //     for (int i = 0; i < subGrids.Length; i++)
    //     {
    //         if ( i == nearestGrid || i == canvasIndex)
    //         {
    //             subGrids[i].SetActive(true);
    //         }
    //         else if (wonGrids.Contains(i))
    //         {
    //             subGrids[i].SetActive(false);
    //         }
    //         else
    //         {
    //             subGrids[i].SetActive(false);
    //         }
    //     }

    //     // flag AIcanvasChange set true for ai to move
    //     AIcanvasChanged = true;
    // }

    // prototype 2

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

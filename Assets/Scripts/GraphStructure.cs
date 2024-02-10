using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GraphStructure
{
    public Dictionary<int, List<int>> adjacencyList; //var for the graph
    public CanvasManager CM;

    //initializes the graph CONSTRUCTOR
    public GraphStructure(CanvasManager canvasManager)
    {
        adjacencyList = new Dictionary<int, List<int>>();
        CM = canvasManager;
    }

    //adds subgrids as nodes
    public void AddSubGrid(int subGridIndex) 
    {
        if (!adjacencyList.ContainsKey(subGridIndex))
        {
            adjacencyList[subGridIndex] = new List<int>();
        }
    }

    public void RemoveSubGrid(int subGridIndex)
    {
        if (adjacencyList.ContainsKey(subGridIndex))
        {
            // Remove subgrid
            foreach (var neighbor in adjacencyList[subGridIndex])
            {
                adjacencyList[neighbor].Remove(subGridIndex);
            }

            // Remove the subgrid and its associated edges from the graph
            adjacencyList.Remove(subGridIndex);
        }
    }

    //adds edges to nodes and their adjacent nodes
    public void AddEdges()
    {
        for (int subGridIndex = 0; subGridIndex < 9; subGridIndex++)
        {
            int row = subGridIndex / 3;
            int col = subGridIndex % 3;

            // Add edges to all adjacent nodes, including diagonals
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < 3 && j >= 0 && j < 3 && !(i == row && j == col))
                    {
                        int neighbor = i * 3 + j;
                        adjacencyList[subGridIndex].Add(neighbor);
                    }
                }
            }
        }
    }

    public bool CheckExist(int subGridIndex)
    {
        if(adjacencyList.ContainsKey(subGridIndex))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }


    //bfs and returns closest node
    public int SearchNearestGrid(int root)
    {
        HashSet<int> visited = new HashSet<int>();
        Queue<int> queue = new Queue<int>();

        queue.Enqueue(root);
        visited.Add(root);

        while (queue.Count > 0)
        {
            int currentSubGrid = queue.Dequeue();

            foreach (var neighbor in adjacencyList[currentSubGrid])
            {
                if (!visited.Contains(neighbor) && !CM.wonGrids.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                    return neighbor;  // Return the first unvisited and unwon neighbor found
                }
                else if (!visited.Contains(neighbor))
                {
                    queue.Enqueue(4);
                }
            }

            // If all neighbors are visited or won, explore visited neighbors that haven't been won
            foreach (var neighbor in adjacencyList[currentSubGrid])
            {
                if (visited.Contains(neighbor) && !CM.wonGrids.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        return -1;
    }

    //debugging
    public void PrintGraph()
    {
        foreach (var kvp in adjacencyList)
        {
            Debug.Log($"SubGrid {kvp.Key} neighbors: [{string.Join(", ", kvp.Value)}]");
        }
    }
}

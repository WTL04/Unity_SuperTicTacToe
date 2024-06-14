using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prototypeAITurn : MonoBehaviour
{

    public int currentPlayer;
    private static int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        difficulty = 3;
        currentPlayer = prototype.CurrentPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        prototype prototype = GetComponentInChildren<prototype>();
        currentPlayer = prototype.CurrentPlayer;
        
        // happens after changeTurn function
        // static var = use script name instead of instance
        if (currentPlayer == 1)
        {
            prototype.AImove();
        }

        
    }
}

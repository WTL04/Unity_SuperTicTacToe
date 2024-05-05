using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIturn : MonoBehaviour
{

    public int currentPlayer;

    // reference to AIdifficult script, used to get difficulty
    private AIdifficult AIdifficultObject;
    private static int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        // Find the AIdifficultObject in the scene, get difficulty property
        AIdifficultObject = FindObjectOfType<AIdifficult>();
        difficulty = AIdifficultObject.Difficulty;

        // Get static currentPlayer from CompManager
        currentPlayer = CompManager.CurrentPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayer = CompManager.CurrentPlayer;
        CompManager compManager = GetComponentInChildren<CompManager>();

        // happens after changeTurn function
        // static var = use script name instead of instance
        if (currentPlayer == 1 && CanvasManager.AIcanvasChanged)
        {
            compManager.AImove();
            CanvasManager.AIcanvasChanged = false;
        }

        
    }
}

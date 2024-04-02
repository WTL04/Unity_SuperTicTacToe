using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIdifficult : MonoBehaviour
{
    public static int difficulty;

    // property
    public int Difficulty
    {
        get {return difficulty;}
    }

    // sets difficulty, 1 = easy, 2 = normal, 3 = hard
    public void SetDifficulty(int diff)
    {
        switch (diff)
        {
            case 1: 
                difficulty = 1;
                break;
            
            case 2:
                difficulty = 2;
                break;

            case 3:
                difficulty = 3;
                break;
        }
        
    }

}

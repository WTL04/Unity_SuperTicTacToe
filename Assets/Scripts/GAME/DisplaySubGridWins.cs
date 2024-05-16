using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySubGridWins : MonoBehaviour
{

    public List<GameObject> imageObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in imageObjects)
        {
            obj.SetActive(false);
        }
    }

    public void DisplayWin(int result, int gridIndex)
    {
        if (result >= 1 && result <= 3)
        {
            GameObject imageObject = imageObjects[gridIndex];
            imageObject.SetActive(true);

            Transform winnerTransform = imageObject.transform;
            GameObject circleWinner = winnerTransform.Find("circleWinner").gameObject;
            GameObject crossWinner = winnerTransform.Find("crossWinner").gameObject;
            GameObject draw = winnerTransform.Find("draw").gameObject;

            circleWinner.SetActive(result == 2);
            crossWinner.SetActive(result == 1);
            draw.SetActive(result == 3);
        }

    }
}

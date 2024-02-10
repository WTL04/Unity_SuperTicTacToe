using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Sprite circle;
    public Sprite cross;
    public Text displayText;

    public bool turn = true;

    // Start is called before the first frame update
    void Start()
    {

        Button[] buttons = GetComponentsInChildren<Button>();
        
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => ChangeImage(button));
        }

    }


    //Changes image of button alternating
    void ChangeImage(Button button)
    {
        
        if (turn) 
        {   
            ChangeButtonImage(button, cross);

            turn = false;
        }
        else
        {
            ChangeButtonImage(button, circle);
            turn = true;
        }
        button.interactable = false;

    }

    //helper method
    void ChangeButtonImage(Button button, Sprite sprite)
    {
        Image buttonImage = button.GetComponent<Image>();

        if (buttonImage != null && sprite != null)
        {
            buttonImage.sprite = sprite;
        }

    }

    void ChangeText(string newText)
    {
        if (displayText != null)
        {
            displayText.text = newText;
        }
    }

    public void ChangeUIText()
    {
        if (turn)
        {
            //flipped to match up with UI
            ChangeText("O's Turn");
        }
        else 
        {
            ChangeText("X's Turn");
        }
        
    }

}
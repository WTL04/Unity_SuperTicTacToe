using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertScript : MonoBehaviour
{
    [SerializeField]
    private GameObject popup; // This will be visible in the Inspector but remain private
    
    [SerializeField]
    private GameObject overlay;

    // Start is called before the first frame update
    void Start()
    {
        popup.SetActive(false);
        overlay.SetActive(false);
    }

    // onclick()
    public void openAlert()
    {
        popup.SetActive(true);
        overlay.SetActive(true);
    }

    public void hideAlert()
    {
        popup.SetActive(false);
        overlay.SetActive(false);
    }
}

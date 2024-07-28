using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    private static Music instance = null;

    // awake() is checked earlier than start()
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        PlayerPrefs.SetInt("Muted", 1);
    }

    public void ToggleSound()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            PlayerPrefs.SetInt("Muted", 1);
            AudioListener.volume = 1;
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
            AudioListener.volume = 0;
        }
    }
}

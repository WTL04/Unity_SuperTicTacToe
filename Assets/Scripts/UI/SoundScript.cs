using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour
{
    private Music music;
    public Toggle toggle;

    void Start()
    {
        music = GameObject.FindObjectOfType<Music>();
    }

    // onClick() for toggle button
    public void PauseMusic()
    {
        music.ToggleSound();
    }
}

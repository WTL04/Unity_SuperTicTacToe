using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // change scene using onClick()
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

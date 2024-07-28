using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // change scene using onClick()
    public void SceneChange(string sceneName)
    {
        StartCoroutine(SceneDelay(sceneName));
    }

    IEnumerator SceneDelay(string sceneName)
    {
        yield return new WaitForSeconds(0.7f); 
        SceneManager.LoadScene(sceneName);
    }
}

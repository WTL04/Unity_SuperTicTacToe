using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    [SerializeField]
    private GameObject text;

    // onclick()
    public void ButtonBlink()
    {
        StartCoroutine(DelayBlink());
    }

    private IEnumerator DelayBlink()
    {
        while (true)
        {
            if (text.activeInHierarchy)
            {
                text.SetActive(false);
            }
            else
            {
                text.SetActive(true);
            }
            yield return new WaitForSeconds(0.3f);
        }
        yield break;
    }
}

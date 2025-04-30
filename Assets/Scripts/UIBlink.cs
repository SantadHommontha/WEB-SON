using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class UIBlink : MonoBehaviour
{
    [SerializeField] private Graphic uiGraphic;
    [SerializeField] private float blinkSpeed = 1f;
    private Coroutine coroutine;

    public void Stop()
    {
        StopCoroutine(coroutine);
    }
    public void Play()
    {
        coroutine = StartCoroutine(FadeAlpha());
    }
    IEnumerator FadeAlpha()
    {
        while (true)
        {

            for (float i = 1f; i >= 0f; i -= Time.deltaTime * blinkSpeed)
            {
                Color currentColor = uiGraphic.color;
                currentColor.a = i;
                uiGraphic.color = currentColor;
                yield return null;
            }

            for (float i = 0f; i <= 1f; i += Time.deltaTime * blinkSpeed)
            {
                Color currentColor = uiGraphic.color;
                currentColor.a = i;
                uiGraphic.color = currentColor;
                yield return null;
            }
        }
    }
}

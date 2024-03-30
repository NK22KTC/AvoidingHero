using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransform : MonoBehaviour
{
    protected internal RectTransform rectTransform;

    protected internal bool fadeOut = true,
                            fadeIn = false;

    public float endFadeTime = 1.2f;

    float fadeTime = 0;

    float rightMax = 2200,
          moveAmount = -2200;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut && fadeIn)
        {
            fadeOut = false;
            fadeIn = false;

            Debug.LogError("fadeOut‚ÆfadeIn‚Ç‚Á‚¿‚àƒIƒ“‚É‚È‚Á‚Ä‚é!!");
        }


        if (fadeOut) PanelFadeOut();
        if(fadeIn) PanelFadeIn();

        if (!fadeOut && !fadeIn) fadeTime = 0;
    }

    void PanelFadeOut()
    {
        Vector3 rectPos = rectTransform.position;
        rectPos.x = 960 + moveAmount / (1 + Mathf.Exp(-15*(fadeTime - 1)));
        //Debug.Log(rectPos);
        rectTransform.position = rectPos;

        fadeTime += Time.unscaledDeltaTime;
    }

    void PanelFadeIn()
    {
        Vector3 rectPos = rectTransform.position;
        rectPos.x = 960 + rightMax + moveAmount / (1 + Mathf.Exp(-15 * (fadeTime - 0.2f)));
        //Debug.Log(rectPos);
        rectTransform.position = rectPos;

        fadeTime += Time.unscaledDeltaTime;

        if(fadeTime >= endFadeTime)
        {
            fadeIn = false;
        }
    }
}

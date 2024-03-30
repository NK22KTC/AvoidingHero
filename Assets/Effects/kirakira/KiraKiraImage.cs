using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KiraKiraImage : MonoBehaviour
{
    [SerializeField]
    Image image;

    [SerializeField]
    float frameRate = 30f;

    [SerializeField]
    bool attacked = true;

    [SerializeField]
    Sprite[] kiraKiraSprites;

    void FixedUpdate()
    {
        if (attacked) StartCoroutine(SpriteChange(0));
    }

    IEnumerator SpriteChange(int i)
    {
        attacked = false;

        if (i < kiraKiraSprites.Length)
        {
            image.sprite = kiraKiraSprites[i];
            yield return new WaitForSeconds(1f / frameRate);

            StartCoroutine(SpriteChange(i + 1));
        }
        else
        {
            //image.sprite = kiraKiraSprites[0];
            yield return null;
        }
    }
}

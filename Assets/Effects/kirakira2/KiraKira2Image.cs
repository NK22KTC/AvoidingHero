using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KiraKira2Image : MonoBehaviour
{

    GameObject canvas;

    [SerializeField]
    Image image;

    [SerializeField]
    float frameRate = 180f;

    [SerializeField]
    bool attacked = true;

    [SerializeField]
    Sprite[] kiraKira2Sprites;

    float destroyTimer = 1.5f;

    void Start()
    {
        canvas = transform.root.gameObject;
    }

    void FixedUpdate()
    {
        destroyTimer -= Time.fixedDeltaTime;

        if(destroyTimer <= 0) Destroy(gameObject);

        if (attacked) StartCoroutine(SpriteChange(0));
    }

    IEnumerator SpriteChange(int i)
    {
        attacked = false;

        if (i < kiraKira2Sprites.Length)
        {
            image.sprite = kiraKira2Sprites[i];
            yield return new WaitForSeconds(1f / frameRate);

            StartCoroutine(SpriteChange(i + 1));
        }
        else
        {
            canvas = null;
            //image.sprite = kiraKira2Sprites[0];
            yield return null;
        }
    }
}

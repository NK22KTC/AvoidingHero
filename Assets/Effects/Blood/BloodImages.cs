using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodImages : MonoBehaviour
{
    //[SerializeField]
    //GameObject faceObj;

    [SerializeField]
    Image image;

    [SerializeField]
    float frameRate = 30f;

    public bool attacked = false,
                roop = true;

    [SerializeField]
    int roopNum = 20;


    [SerializeField]
    Sprite[] bloodSprites;

    void Update()
    {
        //transform.position = faceObj.transform.position;
    }

    void FixedUpdate()
    {
        if(attacked) StartCoroutine(SpriteChange(0));
    }

    IEnumerator SpriteChange(int i)
    {
        attacked = false;

        if (i < bloodSprites.Length)
        {
            image.sprite = bloodSprites[i];
            yield return new WaitForSeconds(1f / frameRate);

            StartCoroutine(SpriteChange(i + 1));
        }
        else
        {
            if (roop)  //ƒ‹[ƒv‚ªƒIƒ“‚ÌŽž
            {
                StartCoroutine(SpriteChange(roopNum));
                yield return null;
            }
            else
            {
                image.sprite = bloodSprites[0];
                yield return null;
            }
        }
    }
}
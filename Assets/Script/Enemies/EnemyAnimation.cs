using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField]
    Sprite[] imageSprites;

    [SerializeField]
    float waitSecond = 0.1f;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeSprite(0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeSprite(int i)
    {
        spriteRenderer.sprite = imageSprites[i];
        yield return new WaitForSeconds(waitSecond);
        if(i < imageSprites.Length-1)
        {
            StartCoroutine(ChangeSprite(i + 1));
        }
        else
        {
            StartCoroutine(ChangeSprite(0));
        }
    }
}

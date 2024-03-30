using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutCoin : MonoBehaviour
{
    CircleCollider2D cc2D;
    Rigidbody2D rb2D;

    [SerializeField]
    GameObject kiraKiraEffect;

    [SerializeField]
    float coroutineTimer = 1f, stopMoveTimer = 1f, moveSpeed = 0.01f, rotateSpeed = 270f;

    bool isTouched;


    // Start is called before the first frame update
    void Start()
    {
        cc2D = gameObject.GetComponent<CircleCollider2D>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CoinRotation();

        if (isTouched)
        {
            Destroy(gameObject);
        }

        if(transform.position.y <= -6.3f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.root.gameObject.tag == "Player")
        {
            GameObject kirakira = Instantiate(kiraKiraEffect, transform.position, Quaternion.identity);

            if (gameObject.CompareTag("RedCoin"))
            {
                kirakira.transform.GetChild(0).GetComponent<Image>().color = Color.red;
            }


            StartCoroutine("PlayProcess");  //コルーチンのスタート
        }
    }

    IEnumerator PlayProcess()
    {
        isTouched = true;

        cc2D.enabled = false;
        rb2D.simulated = false;

        yield return new WaitForSeconds(coroutineTimer);//2秒止める

        Destroy(gameObject);

        yield return null;
    }

    void CoinRotation()
    {
        transform.eulerAngles += transform.up * Time.fixedDeltaTime * rotateSpeed;

        float rotateY = transform.eulerAngles.y;
        if (rotateY >= 270) rotateY -= 360;
        if (rotateY < 90)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void TouchedMove()
    {
        if (stopMoveTimer < 0) return;
        Vector3 v3;

        stopMoveTimer -= Time.deltaTime;

        v3 = transform.position;
        v3.y += moveSpeed;
        transform.position = v3;
    }
}
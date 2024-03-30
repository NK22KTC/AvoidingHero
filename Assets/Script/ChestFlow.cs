using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class ChestFlow : MonoBehaviour
{
    GameFlow gameFlow;

    PanelManager panelManager;

    [SerializeField]
    GameObject coin;

    List<GameObject> gimmickItems = new List<GameObject>();

    GameObject player;

    [SerializeField]
    int dropCoins = 0;

    public float moveSpeed = 16f;

    [SerializeField]
    Transform playerPos;

    public float stopTime = 1.0f;
    int AttackTimes = 0;

    [SerializeField, Tooltip("後でSerializeFieldを消す")]
    bool isAttack, isUTurn;

    [SerializeField, Tooltip("後でSerializeFieldを消す")]
    Vector3 originalPos;

    float moveDir;

    bool coroutineStart = true;

    void Start()
    {
        gameFlow = GameObject.FindWithTag("GameManager").GetComponent<GameFlow>();

        panelManager = GameObject.Find("GameManager").GetComponent<PanelManager>();

        originalPos = transform.position;

        player = GameObject.FindWithTag("Player");
        playerPos = player.transform;

        moveDir = Time.deltaTime * moveSpeed;
        if (Random.Range(0, 2) == 0) moveDir *= -1;  //左右どちらに動くかをランダムに決める

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (panelManager.isPaused) return;

        if (coroutineStart)
        {
            StartCoroutine("CoinDrop");  //コルーチンのスタート
            coroutineStart = false;
        }

        ChestMoves();
    }

    void ChestMoves()
    {
        if (gameFlow.isGameOver) return;

        Vector3 newPos = transform.position;

        if (!isAttack)
        {
            if (Mathf.Abs(newPos.x) > 8) moveDir *= -1;

            newPos.x += moveDir * Time.fixedDeltaTime;
            transform.position = newPos;
            return;
        }

        if(isUTurn && AttackTimes == 3)
        {
            gameFlow.avoidCount = 10;
        }

        if (transform.position.y >= originalPos.y && isUTurn && AttackTimes < 3)  //処理終了後画面から消える
        {
            isUTurn = false;
            isAttack = false;
            StartCoroutine("CoinDrop");
        }

        if (isUTurn)
        {
            newPos.y += Mathf.Abs(moveDir) * Time.fixedDeltaTime;
            transform.position = newPos;

            gameFlow.avoidCount = AttackTimes;

            return;
        }

        if (newPos.y > playerPos.position.y && !isUTurn)
        {
            newPos.y -= Mathf.Abs(moveDir) * Time.fixedDeltaTime;
            transform.position = newPos;
        }
        else
        {
            isUTurn = true;
        }
    }

    IEnumerator CoinDrop()
    {
        GameObject coinObject = Instantiate(coin, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
        coinObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-50f, 50f), 0));
        gimmickItems.Add(coinObject);
        dropCoins++;
        //gameFlow.avoidCount = AttackTimes;

        if (dropCoins <= 15)
        {
            yield return new WaitForSeconds(stopTime);//コルーチンの処理を止める;
            StartCoroutine("CoinDrop");
        }
        else if(AttackTimes  < 3)
        {
            yield return new WaitForSeconds(2f);//コルーチンの処理を止める;
            isAttack = true;
            dropCoins = 0;
            AttackTimes++;
        }
        else
        {
            yield return null;//コルーチンの処理を止める;
        }
    }
}

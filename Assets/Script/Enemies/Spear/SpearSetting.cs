using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpearSetting : MonoBehaviour
{
    [SerializeField]
    GameObject spear, coin, redCoin;

    [SerializeField]
    int stopTimes = 2;

    List<float> spearPos = new List<float>(),  //槍の出現位置を格納する
                spacePos = new List<float>(),
                coinPos = new List<float>();   //コインの出現位置を格納する

    List<GameObject> gimmickItems = new List<GameObject>();

    int attackedTimes = 0;

    GameFlow gameFlow;
    int atInt;

    GameObject player;
    PlayerMoves pm;

    int removePoint;


    SingletonObject items;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pm = player.GetComponent<PlayerMoves>();

        gameFlow = gameObject.GetComponent<GameFlow>();

        items = GameObject.FindWithTag("SingletonObject").GetComponent<SingletonObject>();

        StartCoroutine("SetSpear");  //コルーチンのスタート
    }

    void Update()
    {
        for (int i = gimmickItems.Count - 1; i > 0; i--)
        {
            if (gimmickItems[i] == null) continue;

            if (gimmickItems[i].transform.position.y < -6.3f)
            {
                while (gimmickItems.Count != 0)
                {
                    GameObject item = gimmickItems[gimmickItems.Count - 1];
                    item = null;
                    Destroy(item);
                    gimmickItems.RemoveAt(gimmickItems.Count - 1);
                }
                break;
            }
        }
    }

    IEnumerator SetSpear()
    {
        attackedTimes += 1;  //槍の発射回数のカウント

        SetUpObjects();

        SetObjects();

        yield return new WaitForSeconds(stopTimes);//2秒止める

        /*if (pm != null) */if (pm.isGameOver) yield break;

        gameFlow.avoidCount++;
        items.avoidTimes++;
        items.getYellowCoins++;

        if (attackedTimes < 10)  //10回終わるまで
        {
            StartCoroutine("SetSpear");
        }
    }

    void SetUpObjects()
    {
        for (float i = -8; i <= 9; i += 1.15f) //槍の出現可能な場所を全て格納
        {
            spearPos.Add(i - 0.1f);
        }

        for (int i = 0; i < 1; i++)//一部消して避けるスペースを作る
        {
            removePoint = Random.Range(0, spearPos.Count() - 1);
            spearPos.RemoveAt(removePoint);
            spearPos.RemoveAt(removePoint);
        }
    }

    void SetObjects()
    {
        //int firstCount = spearPos.Count();//出現する槍の初期値(次のfor文でarrowPos.Count()を使うと振ってくる槍の数が減る)

        for (int i = 0; i < Random.Range(0, 3); i++)  //槍の一部をコインに置き換える
        {
            int replacePoint = Random.Range(0, spearPos.Count());

            if(Random.Range(1, 101) <= 20)  //5分の1で赤コインを生成する
            {
                gimmickItems.Add(Instantiate(redCoin, new Vector3(spearPos[replacePoint], 6.25f, 0), Quaternion.identity));
            }
            else
            {
                gimmickItems.Add(Instantiate(coin, new Vector3(spearPos[replacePoint], 6.25f, 0), Quaternion.identity));
            }
            
            spearPos.RemoveAt(replacePoint);
        }

        int firstCount = spearPos.Count();  //出現する槍の数を格納

        for (int i = 0; i < firstCount; i++)
        {
            gimmickItems.Add(Instantiate(spear, new Vector3(spearPos[0], 6.25f, 0), Quaternion.identity));
            spearPos.RemoveAt(0);
        }
    }
}

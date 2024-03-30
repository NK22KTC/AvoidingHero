using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSetting : MonoBehaviour
{
    [SerializeField]
    GameObject DragonPrehab;

    GameFlow gameFlow;

    List<GameObject> dragons = new List<GameObject>();

    public List<GameObject> fireObjects = new List<GameObject>();

    float maxRightPos = 8.9f,
          maxUpPos = 5f;

    internal bool endPhase = true;

    void Start()
    {
        gameFlow = GetComponent<GameFlow>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (endPhase)
        {
            for (int i = dragons.Count - 1; i > 0; i++)
            {
                Destroy(dragons[i]);
                dragons[i] = null;
            }
            for (int j = fireObjects.Count - 1; j > 0; j--)
            {
                Destroy(fireObjects[j]);
                fireObjects[j] = null;
            }

            if (gameFlow.avoidTime < 0) return;

            for (int i = 0; i < 6; i++)
            {
                SetDragons();
            }
            endPhase = false;
        }
    }

    void SetDragons()
    {
        Vector3 dragonPos;
        GameObject dragon;
        BreatheFire.dragonPos originalPos;
        int randNum = Random.Range(0, 4);

        switch (randNum)
        {
            case 0:  //ドラゴンを右側に置く
                dragonPos = new Vector3(maxRightPos, Random.Range(-maxUpPos, maxUpPos), 0);
                originalPos = BreatheFire.dragonPos.Right;
                break;
            case 1:  //ドラゴンを左側に置く
                dragonPos = new Vector3(-maxRightPos, Random.Range(-maxUpPos, maxUpPos), 0);
                originalPos = BreatheFire.dragonPos.Left;
                break;
            case 2:  //ドラゴンを上側に置く
                dragonPos = new Vector3(Random.Range(-maxRightPos, maxRightPos), maxUpPos, 0);
                originalPos = BreatheFire.dragonPos.Up;
                break;
            default:  //ドラゴンを下側に置く
                dragonPos = new Vector3(Random.Range(-maxRightPos, maxRightPos), -maxUpPos, 0);
                originalPos = BreatheFire.dragonPos.Down;
                break;
        }

        dragon = Instantiate(DragonPrehab, dragonPos, DragonPrehab.transform.rotation);
        if (originalPos == BreatheFire.dragonPos.Left)
        {
            dragon.GetComponent<SpriteRenderer>().flipX = true;
        }

        BreatheFire breatheFire = dragon.GetComponent<BreatheFire>();
        breatheFire.pos = originalPos;

    }
}

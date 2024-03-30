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

    List<float> spearPos = new List<float>(),  //���̏o���ʒu���i�[����
                spacePos = new List<float>(),
                coinPos = new List<float>();   //�R�C���̏o���ʒu���i�[����

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

        StartCoroutine("SetSpear");  //�R���[�`���̃X�^�[�g
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
        attackedTimes += 1;  //���̔��ˉ񐔂̃J�E���g

        SetUpObjects();

        SetObjects();

        yield return new WaitForSeconds(stopTimes);//2�b�~�߂�

        /*if (pm != null) */if (pm.isGameOver) yield break;

        gameFlow.avoidCount++;
        items.avoidTimes++;
        items.getYellowCoins++;

        if (attackedTimes < 10)  //10��I���܂�
        {
            StartCoroutine("SetSpear");
        }
    }

    void SetUpObjects()
    {
        for (float i = -8; i <= 9; i += 1.15f) //���̏o���\�ȏꏊ��S�Ċi�[
        {
            spearPos.Add(i - 0.1f);
        }

        for (int i = 0; i < 1; i++)//�ꕔ�����Ĕ�����X�y�[�X�����
        {
            removePoint = Random.Range(0, spearPos.Count() - 1);
            spearPos.RemoveAt(removePoint);
            spearPos.RemoveAt(removePoint);
        }
    }

    void SetObjects()
    {
        //int firstCount = spearPos.Count();//�o�����鑄�̏����l(����for����arrowPos.Count()���g���ƐU���Ă��鑄�̐�������)

        for (int i = 0; i < Random.Range(0, 3); i++)  //���̈ꕔ���R�C���ɒu��������
        {
            int replacePoint = Random.Range(0, spearPos.Count());

            if(Random.Range(1, 101) <= 20)  //5����1�ŐԃR�C���𐶐�����
            {
                gimmickItems.Add(Instantiate(redCoin, new Vector3(spearPos[replacePoint], 6.25f, 0), Quaternion.identity));
            }
            else
            {
                gimmickItems.Add(Instantiate(coin, new Vector3(spearPos[replacePoint], 6.25f, 0), Quaternion.identity));
            }
            
            spearPos.RemoveAt(replacePoint);
        }

        int firstCount = spearPos.Count();  //�o�����鑄�̐����i�[

        for (int i = 0; i < firstCount; i++)
        {
            gimmickItems.Add(Instantiate(spear, new Vector3(spearPos[0], 6.25f, 0), Quaternion.identity));
            spearPos.RemoveAt(0);
        }
    }
}

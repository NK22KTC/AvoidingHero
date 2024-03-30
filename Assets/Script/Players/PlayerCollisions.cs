using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField]
    GameObject bloodObj;

    PlayerMoves pm;
    GameFlow gameFlow;

    PanelManager pause;
    GameObject gameManager;

    SingletonObject items;

    AudioSource audioSource;

    void Start()
    {
        pm = GetComponent<PlayerMoves>();

        gameManager = GameObject.Find("GameManager");
        gameFlow = gameManager.GetComponent<GameFlow>();

        items = SingletonObject.instance;

        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            HitEnemyAttack();
        }

        if (other.CompareTag("Coin"))
        {
            items.getYellowCoins += 1;
        }

        if (other.CompareTag("RedCoin"))
        {
            gameFlow.redCoin += 1;
            items.getRedCoins += 1;

            for (int i = 0; i < items.coinColorSetter.Length; i++)
            {
                if (!items.coinColorSetter[i])
                {
                    items.coinColorSetter[i] = true;
                    break;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack"))
        {
            HitEnemyAttack();
        }
    }

    void HitEnemyAttack()
    {
        if (gameFlow.stageClear) return;

        audioSource.Play();

        pm.isGameOver = true;
        gameFlow.isGameOver = true;

        bloodObj.GetComponent<BloodImages>().attacked = true;
    }
}

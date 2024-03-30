using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUIManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] redCoinUI = new GameObject[3];

    [SerializeField]
    bool[] coinColorSetter = new bool[3];

    void Start()
    {
        redCoinUI = GameObject.FindGameObjectsWithTag("RedCoinUI");

        for (int i = 0; i < redCoinUI.Length; i++)
        {
            coinColorSetter[i] = false;
        }
    }

    void FixedUpdate()
    {
        coinColorSetter = SingletonObject.instance.coinColorSetter;

        for (int i = 0; i < redCoinUI.Length; i++)
        {
            GameObject coinUI = redCoinUI[i];

            Image coinImage = coinUI.GetComponent<Image>();
            coinImage.color = SetRedCoinColor(coinColorSetter[i]);
        }
    }

    public void StartInvoke()
    {
        Invoke("GetRedCoinUI", 0.05f);
    }

    void GetRedCoinUI()
    {
        for (int i = 0; i < redCoinUI.Length; i++)
        {
            GameObject coinUI = redCoinUI[i];
            Image coinImage = coinUI.GetComponent<Image>();
            coinImage.color = SetRedCoinColor(coinColorSetter[i]);
        }
    }

    Color SetRedCoinColor(bool setColor)
    {
        if (setColor) return new Color(1, 1, 1, 1);

        else return new Color(1, 1, 1, 0.1f);
    }
}

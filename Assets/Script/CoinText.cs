using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    SingletonObject singleton;

    [SerializeField]
    Text getYellowCointext, getRedCointext;
    void Start()
    {
        singleton = SingletonObject.instance;

        //getYellowCointext = GameObject.Find("GetYellowCoinText").GetComponent<Text>();
        //getRedCointext = GameObject.Find("GetRedCoinText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        getYellowCointext.text = string.Format("x {0}", singleton.getYellowCoins);
        getRedCointext.text = string.Format("x {0}", singleton.getRedCoins);
    }
}
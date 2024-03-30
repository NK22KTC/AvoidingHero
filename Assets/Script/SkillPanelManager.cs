using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanelManager : MonoBehaviour
{
    [SerializeField]
    Text totalCoinText;

    // Update is called once per frame
    void Update()
    {
        totalCoinText.text = string.Format("ÉRÉCÉìñáêî : {0}", SingletonObject.instance.totalCoins);
    }
}

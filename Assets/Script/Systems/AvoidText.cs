using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AvoidText : MonoBehaviour
{
    GameFlow gameFlow;

    [SerializeField]
    Text avoidTimesText;

    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        gameFlow = gameObject.GetComponent<GameFlow>();
        sceneName = SceneManager.GetSceneAt(0).name;
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneName == "VillageStage")
        {
            avoidTimesText.text = string.Format("îÇØÇΩâÒêî: {0}/10", gameFlow.avoidCount);
        }
        else if (sceneName == "BonusStage")
        {
            avoidTimesText.text = string.Format("îÇØÇΩâÒêî: {0}/3", gameFlow.avoidCount);
        }
        else/* if(sceneName == "SeaScene" || sceneName == "SkyScene" || sceneName == "SpaceScene")*/
        {
            avoidTimesText.text = string.Format("écÇËÇÃïbêî: {0}/20", (int)gameFlow.avoidTime);
        }
    }
}

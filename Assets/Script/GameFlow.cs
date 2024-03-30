using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using static UnityEditor.Progress;

public class GameFlow : MonoBehaviour
{
    PanelManager panelManager;

    [SerializeField]
    GameObject fadeImage;

    FadeTransform fadeTransform;

    public int avoidCount = 0;
    public float avoidTime = 20f;

    public int redCoin = 0;

    protected internal bool stageClear, isGameOver;

    string[] stages = { "VillageStage", "SeaStage", "SkyStage", "SpaceStage" };

    SingletonObject singleton;

    bool addedOne = false;  //timerで3秒ごとに避けた回数を1増やすためのフラグ

    float fadeTimer = 2f;

    bool faded = false;

    // Start is called before the first frame update
    void Start()
    {
        panelManager = GetComponent<PanelManager>();

        fadeTransform = fadeImage.GetComponent<FadeTransform>();

        singleton = SingletonObject.instance;

        stageClear = false;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(avoidCount >= 10 || avoidTime <= 0)
        {
            stageClear = true;
            isGameOver = false;
        }

        if (isGameOver) return;

        for(int i = 0; i < singleton.coinColorSetter.Length; i++)
        {
            if (!singleton.coinColorSetter[i])
            {
                singleton.goToBonus = false;
                break;
            }

            if (i == 2) singleton.goToBonus = true;
        }

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "VillageStage")
        {
            if (avoidCount == 10)
            {
                FadeIn();
                return;
            }
        }
        else if(sceneName == "BonusStage")
        {
            if (avoidCount == 3)
            {
                FadeIn();
                return;
            }
        }
        else
        {
            if (panelManager.isPaused) return;  //ポーズ判定中はカウントダウンをしない

            Timer();

            if(avoidTime <= 0)
            {
                FadeIn();
                return;
            }
        }
    }

    void Timer()
    {
        avoidTime -= Time.deltaTime;

        if(avoidTime >= 0)
        {
            int iTime = 20 - (int)avoidTime;

            if (iTime == 0) return;

            if (iTime % 3 == 0 && !addedOne)
            {
                singleton.avoidTimes += 1;
                singleton.getYellowCoins += 1;
                addedOne = true;
            }
            else if(iTime % 3 != 0)
            {
                addedOne = false;
            }
        }
    }

    void FadeIn()
    {
        if (!fadeImage.activeSelf) fadeImage.SetActive(true);

        if (fadeTimer >= 0) fadeTimer -= Time.deltaTime;

        if(fadeTimer < 1f)
        {
            fadeTransform.fadeIn = true;
        }

        if(fadeTimer < 0f)
        {
            GoNextStage();
        }
    }

    void GoNextStage()
    {
        SceneManager.LoadScene(SetNextStage(SceneManager.GetActiveScene().name));
        SceneManager.LoadScene("PlayerScene", LoadSceneMode.Additive);

        //SingletonObject.instance.StartInvoke();
    }

    string SetNextStage(string nowStage)
    {
        string stageName = "";

        if (isGameOver) return null;  //ゲームオーバー後にこの関数が実行されても次のステージに行かないようにする


        if (singleton.goToBonus)
        {
            for (int i = 0; i < singleton.coinColorSetter.Length; i++)
            {
                singleton.coinColorSetter[i] = false;
            }

            return "BonusStage";
        }

        while (true)
        {
            stageName = stages[Random.Range(0, stages.Length)];

            if (SceneManager.GetActiveScene().name != stageName) return stageName;
        }

        //return stageName;
    }
}

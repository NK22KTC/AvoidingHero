using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    SingletonObject items;

    protected internal bool isPaused = false;  // 時間が止まっているかどうかを示すフラグ(再生中はfalse,停止中はtrue)

    [SerializeField]
    GameObject PausePanel, DisplayLevelPanel, GameOverPanel, StageClearPanel, textPanel, ExplanationPanel, fadeImage, resultPanel;

    [SerializeField]
    float GameOverPanelDisplayTime;

    [SerializeField]
    Text yoketaText, getYellowCoinsText, getRedCoinsText, totalScoreText;

    GameObject player;
    PlayerMoves pm;

    GameFlow gameFlow;

    FadeTransform fadeTransform;

    bool faded = false, changeedResult = false;

    float fadeSpeed = 2f;

    void Start()
    {
        PausePanel.SetActive(false);  //シーンを開いた瞬間にそれぞれのパネルの表示をオフにする
        DisplayLevelPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        StageClearPanel.SetActive(false);
        ExplanationPanel.SetActive(true);
        fadeImage.SetActive(true);
        textPanel.SetActive(false);
        resultPanel.SetActive(false);

        player = GameObject.FindWithTag("Player");
        pm = player.GetComponent<PlayerMoves>();
        gameFlow = GetComponent<GameFlow>();
        fadeTransform = fadeImage.GetComponent<FadeTransform>();

        items = GameObject.FindWithTag("SingletonObject").GetComponent<SingletonObject>();
    }

    void Update()
    {
        //if (!faded) FadeOut();

        if (pm.isGameOver)
        {
            if (GameOverPanelDisplayTime <= 0)
            {
                if (!changeedResult)
                {
                    Result();
                    changeedResult = true;
                }
                return;
            }
            GameOver();
        }

        else if (Input.GetKeyDown(KeyCode.Space) && !ExplanationPanel.activeSelf) Pause();

        if (gameFlow.avoidCount == 10 || gameFlow.avoidTime < 0)
        {
            if (gameFlow.isGameOver) return;

            StageClearPanel.SetActive(true);
            textPanel.SetActive(false);
        }
    }

    public void Pause()
    {
        Time.timeScale = Mathf.Abs(Time.timeScale - 1f);  //タイムスケールの値を1fと0fを行き来させる
        isPaused = !isPaused;  //true,falseの逆転
        PausePanel.SetActive(isPaused);  //ポーズ画面の表示のオンオフの切り替え;
    }

    void GameOver()
    {
        GameOverPanelDisplayTime -= Time.deltaTime;

        GameOverPanel.SetActive(true);  //ゲームオーバーになったらゲームオーバーのパネルを表示する
        PausePanel.SetActive(false);
        textPanel.SetActive(false);
    }

    void Result()
    {
        items.score = items.avoidTimes + items.getYellowCoins;

        GameOverPanel.SetActive(false);

        resultPanel.SetActive(true);

        yoketaText.text = string.Format("避けた回数: {0}", items.avoidTimes);
        getYellowCoinsText.text = string.Format("獲得した黄コイン :  {0}", items.getYellowCoins);
        getRedCoinsText.text = string.Format("獲得した赤コイン :  {0}", items.getRedCoins);
        totalScoreText.text = string.Format("スコア :  {0}", items.score);
    }
}

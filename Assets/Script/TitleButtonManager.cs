using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleButtonManager : MonoBehaviour
{
    TitleSoundManager soundManager;

    [SerializeField] private GameObject titlePanel;
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private GameObject StageSelectPanel;

    [SerializeField] private Button startgameButton;
    [SerializeField] private Button returnTitleButton;

    [SerializeField] AudioSource SE_Audio;

    private bool DoDebug => Input.GetKey(KeyCode.LeftShift);

    bool gameStart = false;

    int fadeSpeed = 2;

    string selectStage = "VillageStage";

    void Start()
    {
        Init();
    }

    private void Init()
    {
        soundManager = GetComponent<TitleSoundManager>();

        startgameButton.onClick.AddListener(() =>
        {
            if (DoDebug)
            {
                ButtonSE();
                titlePanel.SetActive(false);
                StageSelectPanel.SetActive(true);
            }
            else
            {
                ButtonSE();
                OnChangeActivePanel(StageSelectPanel);
            }
        });
        returnTitleButton.onClick.AddListener(() =>
        {
            StageSelectPanel.SetActive(false);
            titlePanel.SetActive(true);
        });
    }


    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            Color color;
            color = fadePanel.GetComponent<Image>().color;

            fadePanel.SetActive(true);

            if (color.a >= 1)
            {
                SceneManager.LoadScene(selectStage);  //"VillageStage", "SeaStage", "SkyStage", "SpaceStage", "BonusStage"
                SceneManager.LoadScene("PlayerScene", LoadSceneMode.Additive);  //プレイヤーを置いているシーンを読み込む
            }
            else
            {
                color.a += Time.deltaTime * fadeSpeed;
                fadePanel.GetComponent<Image>().color = color;
            }
        }

    }

    public void StageSelect(Text stagename)
    {
        selectStage = stagename.text;

        fadePanel.SetActive(true);
        gameStart = true;
    }

    public void OnChangeActivePanel(GameObject panel)
    {
        if (DoDebug)
        {
            if (soundManager.enabled) soundManager.ChangeSound(panel);
        }
        else
        {
            gameStart = true;
            selectStage = "VillageStage";
        }
    }

    void ButtonSE()
    {
        SE_Audio.Play();
    }
}

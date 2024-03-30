using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleButtonManager : MonoBehaviour
{
    TitleSoundManager soundManager;

    [SerializeField]
    GameObject titlePanel, skillStorePanel, fadePanel, StageSelectPanel;

    [SerializeField]
    AudioSource SE_Audio;

    [SerializeField] private bool doDebug = false;

    bool gameStart = false;

    int fadeSpeed = 2;

    string selectStage = "VillageStage";

    void Start()
    {
        soundManager = GetComponent<TitleSoundManager>();
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
        ButtonSE();
        if (doDebug)
        {
            titlePanel.SetActive(!titlePanel.activeSelf);
            panel.SetActive(!panel.activeSelf);
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

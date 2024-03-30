using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExplanationManager : MonoBehaviour
{
    [SerializeField]
    GameObject textsPanel,
               gameManager,
               ExplanationImage,
               rawImage;

    [SerializeField]
    PanelManager panelManager;

    [SerializeField]
    AudioManager audioManager;

    [SerializeField]
    FadeTransform fadeTransform;

    [SerializeField]
    SpearSetting spepearSetting;

    [SerializeField]
    MeteoSetting meteoSetting;

    [SerializeField]
    DragonSetting dragonSetting;

    [SerializeField]
    Image TimerImage;

    [SerializeField]
    Sprite[] countDownSprites;

    float panelActiveTime = 5f;

    float countDownTimer = 3.99f;  //4f‚ÍindexOutOfRange‚É‚È‚é

    bool finishViewing = false;

    void Start()
    {
        textsPanel.SetActive(false);
        TimerImage.enabled = false;

        Time.timeScale = 0f;
        panelManager.isPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finishViewing)
        {
            panelActiveTime -= Time.unscaledDeltaTime;

            if(panelActiveTime < 0)
            {
                finishViewing = true;
            }
        }

        TimerImage.sprite = countDownSprites[(int)countDownTimer];

        if (finishViewing)
        {
            fadeTransform.fadeOut = false;

            ExplanationImage.SetActive(false);
            rawImage.SetActive(false);

            if (!TimerImage.enabled) TimerImage.enabled = true;

            Time.timeScale = 1f;
            countDownTimer -= Time.deltaTime;
        }

        if(countDownTimer <= 0)
        {
            gameObject.SetActive(false);
            panelManager.isPaused = false;

            textsPanel.SetActive(true);

            if(spepearSetting !=  null) spepearSetting.enabled = true;
            if (audioManager != null) audioManager.gameStarted = true;
            if (meteoSetting != null) meteoSetting.gameStarted = true;
            if (dragonSetting != null) dragonSetting.enabled = true;

        }
    }

    public void OnPressStartGameButton()
    {
        finishViewing = true;
    }
}

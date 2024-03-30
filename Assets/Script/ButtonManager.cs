using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    GameObject fadeImage;

    FadeTransform fadeTransform;

    bool faded = false,
         pressedButton = false,
         fadeingIn = false;

    enum SelectButton { Retry, Title }

    SelectButton selectButton;

    SingletonObject items;

    [SerializeField]
    AudioSource button_SE;

    void Start()
    {
        fadeTransform = fadeImage.GetComponent<FadeTransform>();

        items = SingletonObject.instance;
    }

    void Update()
    {
        if (fadeingIn && faded) LoadScene();

        if (pressedButton)
        {
            if (!fadeImage.activeSelf) fadeImage.SetActive(true);
            FadeIn();
        }

    }

    public void PlayButtonSE()
    {
        button_SE.Play();
    }

    public void OnRetrytButton()
    {
        ResetGetItems();
        pressedButton = true;

        selectButton = SelectButton.Retry;
    }

    public void OnGoTitleButton()
    {
        ResetGetItems();
        pressedButton = true;

        selectButton = SelectButton.Title;
    }

    void ResetGetItems()
    {
        int redCoinNum = Random.Range(items.getRedCoins, items.getRedCoins * 2);
        items.totalCoins += items.getYellowCoins + redCoinNum;
        items.getYellowCoins = 0;
        items.getRedCoins = 0;
        items.avoidTimes = 0;

        for (int i = 0; i < items.coinColorSetter.Length; i++)
        {
            items.coinColorSetter[i] = false;
        }
    }

    void FadeIn()
    {
        Time.timeScale = 1;
        OffGravity();

        if (!fadeingIn) 
        {
            fadeTransform.fadeIn = true;
            fadeingIn = true;
        }

        if (!fadeTransform.fadeIn)
        {
            faded = true;
        }
    }

    void OffGravity()
    {
        GameObject[] objects = FindObjectsOfType(typeof(GameObject)) as GameObject[];

        foreach (var item in objects)
        {
            if (item.GetComponent<Rigidbody2D>())
            {
                Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
                rb.velocity = Vector3.zero;
            }
        }
    }

    void LoadScene()
    {
        if(selectButton == SelectButton.Title)
        {
            SceneManager.LoadScene("Title&StoreScene");
        }
        else
        {
            SceneManager.LoadScene("VillageStage");
            SceneManager.LoadScene("PlayerScene", LoadSceneMode.Additive);
        }
    }
}

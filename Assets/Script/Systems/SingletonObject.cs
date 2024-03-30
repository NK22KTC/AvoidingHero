using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingletonObject : MonoBehaviour
{
    public static SingletonObject instance;

    public int avoidTimes = 0,
               getYellowCoins = 0,
               getRedCoins = 0,
               totalCoins = 0,
               score = 0;

    public bool[] coinColorSetter = {false, false, false};

    public bool goToBonus;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            instance.getYellowCoins = 0;
            instance.getRedCoins = 0;
            instance.avoidTimes = 0;

            Destroy(gameObject);
        }

        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title&StoreScene");
    }
}

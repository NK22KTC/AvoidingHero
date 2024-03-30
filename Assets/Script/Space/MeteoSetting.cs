using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class MeteoSetting : MonoBehaviour
{
    [SerializeField]
    GameFlow gameFlow;

    PlayerMoves playerMoves;

    [SerializeField]
    GameObject meteoPrehab, starPrehab;

    GameObject meteo, star;

    int meteoMaxLeft = -13,
        meteoMaxUp = 8;

    int starMaxLeft = -10,
        starMaxUp = 4;

    internal bool gameStarted = false;

    void Start()
    {
        playerMoves = GameObject.FindWithTag("Player").GetComponent<PlayerMoves>();

        meteo = null;
        star = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted || playerMoves.isGameOver || gameFlow.avoidTime <= 0) return;

        if (meteo == null)
        {
            SettingMeteo();
        }
        else if (meteo.transform.position.y <= -meteoMaxUp || meteo.transform.position.x >= -meteoMaxLeft)
        {
            Destroy(meteo);
            meteo = null;
        }

        if(star == null)
        {
            SettingStar();
        }
        else if(star.transform.position.x >= -starMaxLeft)
        {
            Destroy(star);
            star = null;
        }
    }

    void SettingMeteo()
    {
        int positionX, positionY;

        if(Random.Range(0, 2) == 0)  //Xç¿ïWïœçX
        {
            positionX = Random.Range(0, meteoMaxLeft);
            positionY = meteoMaxUp;
        }
        else
        {
            positionX = meteoMaxLeft;
            positionY = Random.Range(0, meteoMaxUp);
        }

        meteo = Instantiate(meteoPrehab, new Vector3(positionX, positionY, 0), meteoPrehab.transform.rotation);
    }

    void SettingStar()
    {
        int posX, posY;

        posX = starMaxLeft;
        posY = Random.Range(-starMaxUp, starMaxUp);

        star = Instantiate(starPrehab, new Vector3(posX, posY, 0), starPrehab.transform.rotation);
    }
}

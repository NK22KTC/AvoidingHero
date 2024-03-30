using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField]
    GameObject walkChara, swimChara, flyChara;

    Animator walkAnim, swimAim;

    PlayerMoves moves;

    PanelManager pause;

    protected internal bool idle = false,
                            moveRight = false, 
                            moveLeft = false,
                            dead = false;

    string sceneName;

    Vector2 oldPlayerPos;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;

        walkAnim = GetComponent<Animator>();
        swimAim = swimChara.GetComponent<Animator>();

        moves = GetComponent<PlayerMoves>();
        pause = GameObject.Find("GameManager").GetComponent<PanelManager>();

        SetCharacter();
    }

    
    // Update is called once per frame
    void Update()
    {
        if (pause.isPaused) return;

        if(walkChara.activeSelf) Walk();

        else if(swimChara.activeSelf) Swim();

        else Fly();
    }

    void SetCharacter()
    {
        if (sceneName == "VillageStage" || sceneName == "BonusStage")
        {
            walkChara.SetActive(true);
            swimChara.SetActive(false);
            flyChara.SetActive(false);

            walkAnim.enabled = true;
        }
        else if (sceneName == "SeaStage")
        {
            walkChara.SetActive(false);
            swimChara.SetActive(true);
            flyChara.SetActive(false);

            walkAnim.enabled = false;
        }
        else
        {
            walkChara.SetActive(false);
            swimChara.SetActive(false);
            flyChara.SetActive(true);

            walkAnim.enabled = false;
        }
    }

    void Walk()
    {
        moveLeft = Input.GetMouseButton(0) ? true : false;
        moveRight = Input.GetMouseButton(1) ? true : false;
        dead = moves.isGameOver ? true : false;

        //if (pause.isPaused) return;

        if (dead)
        {
            idle = false;
            moveRight = false;
            moveLeft = false;

            dead = true;
        }
        else
        {
            dead = false;

            if (moveRight & moveLeft)
            {
                moveRight = false;
                moveLeft = false;
                idle = true;
            }
            else if (moveRight)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

                moveRight = true;
                moveLeft = false;
                idle = false;
            }
            else if (moveLeft)
            {
                gameObject.transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

                moveRight = false;
                moveLeft = true;
                idle = false;
            }
            else
            {
                moveRight = false;
                moveLeft = false;
                idle = true;
            }
        }

        walkAnim.SetBool("Dash", moveRight || moveLeft);
        walkAnim.SetBool("Dead", dead);
        walkAnim.SetBool("Idle", idle);
    }

    void Swim()
    {
        Vector2 newPlayerPos = transform.position;
        float angle;

        try
        {
            Vector2 dPos = newPlayerPos - oldPlayerPos;
            angle = Mathf.Atan2(dPos.y, dPos.x) * Mathf.Rad2Deg;
        }
        catch
        {
            angle =180;
        }

        if(angle == 0) { }

        else if (Mathf.Abs(angle) < 90) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        else transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        oldPlayerPos = newPlayerPos;
    }

    void Fly()
    {
        Vector2 newPlayerPos = transform.position;
        float angle;

        try
        {
            Vector2 dPos = newPlayerPos - oldPlayerPos;
            angle = Mathf.Atan2(dPos.y, dPos.x) * Mathf.Rad2Deg;
        }
        catch
        {
            angle = 180;
        }

        if (angle == 0) { }

        else if (Mathf.Abs(angle) < 90) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        else transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        oldPlayerPos = newPlayerPos;
    }
}
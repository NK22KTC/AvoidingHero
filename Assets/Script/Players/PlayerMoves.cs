using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoves : MonoBehaviour
{
    int moveRight, moveLeft, moveUp, moveDown;
    int moveX, moveY;

    Vector3 newPos;

    float moveSpeed = 12f,
          leftEdge = -8.49f,
          rightEdge = 8.49f,
          topEdge = 4.3f,
          underEdge = -4.3f;

    protected internal bool isGameOver = false;

    private Vector3 mousePos;

    GameObject gameManager;

    PanelManager pause;
    public GameFlow gameFlow;

    SingletonObject items;

    void Start()
    {
        // ��ʂ̍����̍��W���擾 (��ʍ���)
        Vector3 screen_LeftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        // ��ʂ̉E��̍��W���擾 (��ʉE��)
        Vector3 screen_RightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));  //����܂�悭�Ȃ�

        gameManager = GameObject.Find("GameManager");

        pause = gameManager.GetComponent<PanelManager>();
        gameFlow = gameManager.GetComponent<GameFlow>();

        gameFlow.isGameOver = isGameOver;

        items = SingletonObject.instance;
    }

    // Update is called once per frame
    void Update()
    {
        gameFlow.isGameOver = isGameOver;

        if (isGameOver) return;
        if (pause.isPaused) return;

        //InputKeyboard();
        InputMouse();

        if (transform.position.x < leftEdge) transform.position = new Vector3(leftEdge, transform.position.y, transform.position.z);
        if (transform.position.x > rightEdge) transform.position = new Vector3(rightEdge, transform.position.y, transform.position.z);

        if (transform.position.y < underEdge) transform.position = new Vector3(transform.position.x, underEdge, transform.position.z);
        if (transform.position.y > topEdge) transform.position = new Vector3(transform.position.x, topEdge, transform.position.z);
    }

    void InputMouse()  //�}�E�X����
    {
        string sceneName = SceneManager.GetSceneAt(0).name;

        if (sceneName == "VillageStage" || sceneName == "BonusStage")
        {
            MouseButton();
        }
        else
        {
            MouseDrag();
        }

        void MouseDrag()
        {
            //Cursor.visible = false;  //�}�E�X�J�[�\�����\���ɂ���

            mousePos = Input.mousePosition;
            mousePos.z = 10f;
            newPos = Camera.main.ScreenToWorldPoint(mousePos);
            //newPos.y = -4f;
            newPos.z = 0f;
            transform.position = newPos;
        }

        void MouseButton()
        {
            Vector3 playerPos;
            int moveLeft = 0, moveRight = 0;

            if (Input.GetMouseButton(0)) moveLeft = -1;
            if (Input.GetMouseButton(1)) moveRight = 1;

            playerPos = transform.position;
            playerPos.x += (moveLeft + moveRight) * Time.deltaTime * moveSpeed;

            transform.position = playerPos;
        }
    }
}

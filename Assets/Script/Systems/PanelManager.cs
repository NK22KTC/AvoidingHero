using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    SingletonObject items;

    protected internal bool isPaused = false;  // ���Ԃ��~�܂��Ă��邩�ǂ����������t���O(�Đ�����false,��~����true)

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
        PausePanel.SetActive(false);  //�V�[�����J�����u�Ԃɂ��ꂼ��̃p�l���̕\�����I�t�ɂ���
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
        Time.timeScale = Mathf.Abs(Time.timeScale - 1f);  //�^�C���X�P�[���̒l��1f��0f���s����������
        isPaused = !isPaused;  //true,false�̋t�]
        PausePanel.SetActive(isPaused);  //�|�[�Y��ʂ̕\���̃I���I�t�̐؂�ւ�;
    }

    void GameOver()
    {
        GameOverPanelDisplayTime -= Time.deltaTime;

        GameOverPanel.SetActive(true);  //�Q�[���I�[�o�[�ɂȂ�����Q�[���I�[�o�[�̃p�l����\������
        PausePanel.SetActive(false);
        textPanel.SetActive(false);
    }

    void Result()
    {
        items.score = items.avoidTimes + items.getYellowCoins;

        GameOverPanel.SetActive(false);

        resultPanel.SetActive(true);

        yoketaText.text = string.Format("��������: {0}", items.avoidTimes);
        getYellowCoinsText.text = string.Format("�l���������R�C�� :  {0}", items.getYellowCoins);
        getRedCoinsText.text = string.Format("�l�������ԃR�C�� :  {0}", items.getRedCoins);
        totalScoreText.text = string.Format("�X�R�A :  {0}", items.score);
    }
}

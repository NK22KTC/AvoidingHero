using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    PlayerMoves playerMoves;

    PanelManager panelManager;

    GameFlow gameFlow;

    GameObject gameManager;

    GameObject player;
    Transform playerPos;  // �ǐՂ���v���C���[��Transform�R���|�[�l���g

    [SerializeField]
    float moveSpeed = 10f,  // �ړ����x
          maxSpeed = 15f,  // ���x�̍ő�l
          acceleration = 10f;  // �����x

    private Rigidbody2D rb;
    private Vector2 velocity;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameManager");

        panelManager = gameManager.GetComponent<PanelManager>();
        gameFlow = gameManager.GetComponent<GameFlow>();

        playerMoves = player.GetComponent<PlayerMoves>();
        playerPos = player.transform;

        rb = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
    }

    private void Update()
    {
        if (playerMoves.isGameOver) return;

        if (panelManager.isPaused) return;

        if (gameFlow.avoidTime < 0) return;

        // �v���C���[�̕���������
        Vector2 direction = playerPos.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        // �������l�����ĒǐՂ���
        Vector2 targetVelocity = direction.normalized * moveSpeed;
        velocity = Vector2.Lerp(velocity, targetVelocity, acceleration * Time.deltaTime);
        rb.velocity = velocity;

        // ���x�̍ő�l�𐧌�����
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}